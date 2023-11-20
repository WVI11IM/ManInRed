using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Interactable : MonoBehaviour
{
    public GameObject iconCanvas;
    public ItemData itemData;
    public bool isInteractable;
    bool isInteracting = false;
    bool isShowingIcon;
    public bool isClosest;
    public UnityEvent eventCallback;

    private PlayerInteractionManager playerInteractionManager;
    private Inventory inventory;

    private void Start()
    {
        playerInteractionManager = FindObjectOfType<PlayerInteractionManager>().GetComponent<PlayerInteractionManager>();
        inventory = Inventory.Instance;
        iconCanvas.SetActive(false);
    }
    private void Update()
    {
        iconCanvas.transform.rotation = Camera.main.transform.rotation;

        if (isInteracting)
        {
            iconCanvas.SetActive(false);
        }
        else if (isInteractable && isShowingIcon && isClosest)
        {
            iconCanvas.SetActive(true);

            //calls the events if player inputs E, the item is interactable and it is showing an interaction icon
            if (Input.GetKeyDown(KeyCode.E))
            {
                eventCallback.Invoke();
            }
        }
        else iconCanvas.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteractionManager.UpdateClosestInteractable(this);
            if (isClosest)
            {
                isInteractable = true;
                isShowingIcon = true;
            }
            else
            {
                isInteractable = false;
                isShowingIcon = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
            isShowingIcon = false;
            isClosest = false;
        }
    }

    public void CanInteract(bool isVisible)
    {
        //Shows or hides the interaction icon
        isInteracting = !isVisible;
        isShowingIcon = isVisible;
    }

    public void AddItem(GameObject item)
    {
        inventory.AddItem(item);
    }

    public void RemoveItem(GameObject item)
    {
        inventory.RemoveItem(item);
    }
}
