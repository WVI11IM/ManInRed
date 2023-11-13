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

    private void Start()
    {
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onExit.Invoke();
        }
    }
}