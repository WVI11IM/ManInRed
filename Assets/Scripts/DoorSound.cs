using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public void OpenSound()
    {
        AudioManager.Instance.PlaySoundEffect("openDoor");
    }

    public void CloseSound()
    {
        AudioManager.Instance.PlaySoundEffect("closeDoor");
    }
}
