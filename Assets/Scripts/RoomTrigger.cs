using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class RoomTrigger : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public LayerMask roomLayers;

    private LayerMask originalCullingMask;

    private void Start()
    {
        // Store the original culling mask of the camera
        originalCullingMask = Camera.main.cullingMask;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onExit.Invoke();
        }
    }
}