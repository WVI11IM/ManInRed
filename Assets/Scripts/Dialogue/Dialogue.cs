using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class DialogueElements
{
    [Tooltip("Name of the character that is speaking.")]
    public string characterName;
    [Tooltip("Text that will be displayed in the dialogue box.")]
    [TextArea]
    public string dialogue;
    [Tooltip("Events that will be invoked.")]
    public UnityEvent duringDialogue;
}

public class Dialogue : MonoBehaviour
{
    DialogueManager dialogueManager;
    Movement playerMovement;

    public bool startImmediatelyWithActivation = false;
    public UnityEvent beforeDialogue;
    public DialogueElements[] dialogueElements;
    public UnityEvent afterDialogue;

    bool isActive = false;

    private bool _canContinue = false;

    public int dialogueIndex = 0;

    private void Awake()
    {
        dialogueManager = DialogueManager.Instance;
        playerMovement = FindObjectOfType<Movement>().GetComponent<Movement>();
    }

    void Start()
    {
        if(startImmediatelyWithActivation) StartDialogue();
    }

    void Update()
    {
        if (dialogueIndex > dialogueElements.Length)
        {
            FinishDialogue();
        }
        else if (dialogueManager.isActive && isActive && !dialogueManager.isThinking && _canContinue)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                NextDialogue();
            }
        }
    }

    public void StartDialogue()
    {
        //dialogueManager.StopThoughtCountdown();
        playerMovement.CanMove(false);
        TimeManager.Instance.ClockSwitch(false);
        beforeDialogue.Invoke();
        isActive = true;
        dialogueManager.isThinking = false;
        dialogueManager.thoughtBox.SetActive(false);
        dialogueManager.isActive = true;
        dialogueIndex = 0;
        NextDialogue();
    }

    void NextDialogue()
    {
        if (dialogueIndex < dialogueElements.Length)
        {
            dialogueManager.characterName.text = dialogueElements[dialogueIndex].characterName;
            switch(dialogueManager.characterName.text)
            {
                case "Thorwald":
                    dialogueManager.boxImage.color = dialogueManager.boxColors[0];
                    dialogueManager.dialogueTarget = dialogueManager.thorwaldLocation;
                    break;
                case "Rose":
                    dialogueManager.boxImage.color = dialogueManager.boxColors[1];
                    dialogueManager.dialogueTarget = dialogueManager.roseLocation;
                    break;
                case "Atendente":
                    dialogueManager.boxImage.color = dialogueManager.boxColors[2];
                    dialogueManager.dialogueTarget = dialogueManager.roseLocation;
                    break;
                case "Polícia":
                    dialogueManager.boxImage.color = dialogueManager.boxColors[3];
                    dialogueManager.dialogueTarget = dialogueManager.defaultLocation;
                    break;
                case "Policial":
                    dialogueManager.boxImage.color = dialogueManager.boxColors[3];
                    dialogueManager.dialogueTarget = dialogueManager.policeLocation;
                    break;
                default:
                    dialogueManager.dialogueTarget = dialogueManager.defaultLocation;
                    break;
            }
            StartCoroutine(DisplayLine(dialogueElements[dialogueIndex].dialogue));
            dialogueElements[dialogueIndex].duringDialogue.Invoke();
        }
        else FinishDialogue();
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueManager.dialogueText.text = "";
        _canContinue = false;
        foreach (char letter in line.ToCharArray())
        {
            //PlayDialogueSound(messageText.maxVisibleCharacters);
            dialogueManager.dialogueText.maxVisibleCharacters++;
            dialogueManager.dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        _canContinue = true;
        dialogueIndex++;
    }

    void FinishDialogue()
    {
        if (dialogueManager.isActive)
        {
            dialogueManager.isActive = false;
            isActive = false;
            dialogueIndex = 0;
            dialogueManager.boxImage.color = dialogueManager.boxColors[0];
            dialogueManager.dialogueTarget = dialogueManager.thorwaldLocation;
            afterDialogue.Invoke();
            TimeManager.Instance.ClockSwitch(true);
            playerMovement.CanMove(true);
        }
    }
}
