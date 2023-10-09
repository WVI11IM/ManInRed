using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Interactable : MonoBehaviour
{
    public GameObject canvasIcon;
    public ItemData itemData;
    bool isInteractable;

    public UnityEvent eventCallback;

    private void Start()
    {
        canvasIcon.SetActive(false);
    }
    private void Update()
    {
        if(isInteractable) canvasIcon.SetActive(true);
        else canvasIcon.SetActive(false);

        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            eventCallback.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
        }
    }

    public void SkipTime(float amountToAdd)
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        timeManager.timer += amountToAdd;
    }
}
