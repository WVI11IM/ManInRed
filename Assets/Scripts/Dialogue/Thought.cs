using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class ThoughtElement
{
    public string dialogue;
    public UnityEvent thoughtEvent;
    public float duration;
}

public class Thought : MonoBehaviour
{
    DialogueManager dialogueManager;
    public ThoughtElement thought;


    private void Awake()
    {
        dialogueManager = DialogueManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        yield return new WaitForSeconds(time);
        if (dialogueManager.isActive)
        {
            dialogueManager.isActive = false;
            dialogueManager.isThinking = false;
        }
    }

}
