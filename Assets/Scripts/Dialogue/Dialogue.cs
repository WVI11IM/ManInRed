using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class DialogueElements
{
    public string characterName;
    public string dialogue;
    public UnityEvent duringDialogue;
}

public class Dialogue : MonoBehaviour
{
    DialogueManager dialogueManager;
    Movement playerMovement;

    public UnityEvent beforeDialogue;
    public DialogueElements[] dialogueElements;
    public UnityEvent afterDialogue;

    bool isActive = false;

    public int dialogueIndex = 0;

    private void Awake()
    {
        dialogueManager = DialogueManager.Instance;
        playerMovement = FindObjectOfType<Movement>().GetComponent<Movement>();
    }

    void Start()
    {

    }

    void Update()
    {
        Debug.Log(dialogueIndex);

        if (dialogueIndex > dialogueElements.Length)
        {
            FinishDialogue();
        }
        else if (dialogueManager.isActive && isActive && !dialogueManager.isThinking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                NextDialogue();
            }
        }
    }

    public void StartDialogue()
    {
        if (!dialogueManager.isActive  && !dialogueManager.isThinking)
        {
            playerMovement.CanMove(false);
            TimeManager.Instance.ClockSwitch(false);
            beforeDialogue.Invoke();
            isActive = true;
            dialogueManager.isActive = true;
            dialogueIndex = 0;
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        if (dialogueIndex < dialogueElements.Length)
        {
            dialogueManager.characterName.text = dialogueElements[dialogueIndex].characterName;
            dialogueManager.dialogueText.text = dialogueElements[dialogueIndex].dialogue;
            dialogueElements[dialogueIndex].duringDialogue.Invoke();
            dialogueIndex++;
        }
        else FinishDialogue();
    }

    void FinishDialogue()
    {
        if (dialogueManager.isActive)
        {
            dialogueManager.isActive = false;
            isActive = false;
            dialogueIndex = 0;
            afterDialogue.Invoke();
            TimeManager.Instance.ClockSwitch(true);
            playerMovement.CanMove(true);
        }
    }
}
