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
    bool isShowingIcon;

    public UnityEvent eventCallback;

    private void Start()
    {
        canvasIcon.SetActive(false);
    }
    private void Update()
    {
        //only activates the interaction icon if the item is interactable and if isShowingIcon is true
        if(isInteractable && isShowingIcon) canvasIcon.SetActive(true);
        else canvasIcon.SetActive(false);

        //calls the events if player inputs E, the item is interactable and it is showing an interaction icon
        if (isInteractable && isShowingIcon && Input.GetKeyDown(KeyCode.E))
        {
            eventCallback.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
            isShowingIcon = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
            isShowingIcon = false;
        }
    }

    public void ShowIcon(bool isVisible)
    {
        //Shows or hides the interaction icon
        isShowingIcon = isVisible;
    }
}
