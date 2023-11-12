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

    [Tooltip("Where the box focuses on.")]
    public GameObject dialogueTarget;
    [Tooltip("Canvas that contains the dialogue and thought boxes.")]
    public Canvas canvas;

    [Header("Locations")]
    [Tooltip("Character positions for the dialogue.")]
    public GameObject thorwaldLocation;
    public GameObject roseLocation;
    public GameObject defaultLocation;

    [Header("Dialogue")]
    [Tooltip("Box image for the dialogue.")]
    public GameObject dialogueBox;
    public Image boxImage;
    [Tooltip("All colors or the character dialogue box.")]
    public Color[] boxColors;
    [Tooltip("Name of the character that is speaking.")]
    public TextMeshProUGUI characterName;
    [Tooltip("Text for the active dialogue element.")]
    public TextMeshProUGUI dialogueText;
    [Tooltip("All the GameObjects that must be hidden during dialogues")]
    public GameObject[] objectsToHide;

    [Header("Thought")]
    [Tooltip("Box image for the thought.")]
    public GameObject thoughtBox;
    [Tooltip("Text for the active thought element.")]
    public TextMeshProUGUI thoughtText;
    private Coroutine thoughtCoroutine;
    private bool coroutineIsRunning;

    public bool isActive = false;
    public bool isThinking = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        thoughtBox.SetActive(false);
        isActive = false;
        isThinking = false;
        coroutineIsRunning = false;
}

    void Update()
    {
        if (isActive) 
        {
            canvas.enabled = true;
            canvas.transform.position = new Vector3(dialogueTarget.transform.position.x, dialogueTarget.transform.position.y + 1.2f, dialogueTarget.transform.position.z);
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

    public void StartThoughtCountdown(float duration)
    {
        if (!coroutineIsRunning) thoughtCoroutine = StartCoroutine(ThoughtTime(duration));
        else Debug.Log("Character is already thinking!");
    }

    public void StopThoughtCountdown()
    {
        if (thoughtCoroutine != null)
        {
            StopCoroutine(thoughtCoroutine);
            isActive = false;
            isThinking = false;
        }
    }

    IEnumerator ThoughtTime(float time)
    {
        coroutineIsRunning = true;
        if (isActive)
        {
            yield return new WaitForSeconds(time);
            isActive = false;
            isThinking = false;
        }
        coroutineIsRunning = false;
    }
}
