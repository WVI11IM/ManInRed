using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("DialogueManager");
                    _instance = singleton.AddComponent<DialogueManager>();
                }
            }
            return _instance;
        }
    }

    public GameObject dialogueTarget;
    public Canvas canvas;

    public GameObject dialogueBox;
    public GameObject thoughtBox;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI thoughtText;

    public bool isActive = false;
    public bool isThinking = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        thoughtBox.SetActive(false);
        isActive = false;
        isThinking = false;
}

    void Update()
    {
        if (isActive) 
        {
            canvas.enabled = true;
            canvas.transform.position = new Vector3(dialogueTarget.transform.position.x, 3, dialogueTarget.transform.position.z);
            canvas.transform.rotation = Camera.main.transform.rotation;

            if (isThinking) 
            {
                dialogueBox.SetActive(false);
                thoughtBox.SetActive(true);
            }
            else
            {
                thoughtBox.SetActive(false);
                dialogueBox.SetActive(true);
            }
        }
        else canvas.enabled = false;
    }
}
