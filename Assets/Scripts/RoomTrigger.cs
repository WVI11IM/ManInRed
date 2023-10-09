using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class RoomTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public UnityEvent[] eventCallback;
    public Collider interactionArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eventCallback[1].Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eventCallback[0].Invoke();
        }
    }
}
