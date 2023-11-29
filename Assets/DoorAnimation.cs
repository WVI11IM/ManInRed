using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("isOpen", false);
        }
    }

    public void OpenSound()
    {
        AudioManager.Instance.PlaySoundEffect("openDoor");
    }

    public void CloseSound()
    {
        AudioManager.Instance.PlaySoundEffect("closeDoor");
    }
}
