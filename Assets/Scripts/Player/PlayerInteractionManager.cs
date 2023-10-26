using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public Interactable closestInteractable;

    void Update()
    {

    }

    public void UpdateClosestInteractable(Interactable interactable)
    {
        if (closestInteractable == null)
        {
            closestInteractable = interactable;
            interactable.isClosest = true;
        }
        else
        {
            float distanceToNew = Vector3.Distance(transform.position, interactable.transform.position);
            float distanceToCurrent = Vector3.Distance(transform.position, closestInteractable.transform.position);
            if (distanceToNew <= distanceToCurrent)
            {
                closestInteractable.isClosest = false;
                interactable.isClosest = true;
                closestInteractable = interactable;
            }
        }
    }

    public void CheckAndClearClosestInteractable(Interactable interactable)
    {
        if (closestInteractable == interactable)
        {
            closestInteractable = null;
            interactable.isClosest = false;
        }
    }
}