using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class ThoughtElement
{
    [Tooltip("Text that will be displayed in the thought box.")]
    [TextArea]
    public string dialogue;
    [Tooltip("Events that will be invoked.")]
    public UnityEvent thoughtEvent;
    [Tooltip("Duration of the thought in seconds.")]
    public float duration;
}

public class Thought : MonoBehaviour
{
    DialogueManager dialogueManager;
    [Tooltip("Write the thought here.")]
    public ThoughtElement thought;

    private void Awake()
    {
        dialogueManager = DialogueManager.Instance;
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void StartThought()
    {
        if (!dialogueManager.isActive && !dialogueManager.isThinking)
        {
            thought.thoughtEvent.Invoke();
            dialogueManager.isActive = true;
            dialogueManager.isThinking = true;
            dialogueManager.thoughtText.text = thought.dialogue;
        }
        StartCoroutine(ThoughtTime(thought.duration));
    }

    IEnumerator ThoughtTime(float time)
    {
        if (dialogueManager.isActive)
        {
            yield return new WaitForSeconds(time);
            dialogueManager.isActive = false;
            dialogueManager.isThinking = false;
        }
    }

}
