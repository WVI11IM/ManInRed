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
            Vector3 newCenter = originalCenter + new Vector3(0f, liftAmount * (elapsedTime / 0.01f), 0f);
            triggerCollider.center = newCenter;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        triggerCollider.center = originalCenter + new Vector3(0f, liftAmount, 0f);
    }
}