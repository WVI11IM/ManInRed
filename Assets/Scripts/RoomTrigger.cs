using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class RoomTrigger : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onStay;
    public UnityEvent onExit;
    public LayerMask roomLayers;
    public bool playerIsInside = false;

    private BoxCollider triggerCollider;

    private void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Set the culling mask of the camera to the layers of the current room
        Camera.main.cullingMask = roomLayers;
        if (other.CompareTag("Player"))
        {
            onStay.Invoke();
            playerIsInside = true;
        }
        else playerIsInside = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onExit.Invoke();
            playerIsInside = false;
        }
    }

    public void RaiseCollider(float liftAmount)
    {
        StartCoroutine(LiftOffset(liftAmount));
    }
    private IEnumerator LiftOffset(float liftAmount)
    {
        float elapsedTime = 0f;
        Vector3 originalCenter = triggerCollider.center;

        while (elapsedTime < 0.01f)
        {
            // Calculate the new center with an elevated Y value
            Vector3 newCenter = originalCenter + new Vector3(0f, liftAmount * (elapsedTime / 0.01f), 0f);

            // Apply the new center to the collider
            triggerCollider.center = newCenter;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure that the collider has the final lifted position
        triggerCollider.center = originalCenter + new Vector3(0f, liftAmount, 0f);
    }
}