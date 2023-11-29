using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundTrigger : MonoBehaviour
{
    //public bool isDay = true;
    public bool isOutdoors = false;
    public bool playerOnTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (TimeManager.Instance.hour >= 6 && TimeManager.Instance.hour < 18)
        {
            isDay = true;
        }
        else isDay = false;
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTrigger = true;
            PlayAmbient();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerOnTrigger = false;
    }

    public void PlayAmbient()
    {
        if (playerOnTrigger)
        {
            AudioManager.Instance.StopAmbient();

            if (TimeManager.Instance.hour >= 6 && TimeManager.Instance.hour < 18)
            {
                if(isOutdoors) AudioManager.Instance.PlayAmbient("outDay");
                else AudioManager.Instance.PlayAmbient("inDay");
            }
            else
            {
                if (isOutdoors) AudioManager.Instance.PlayAmbient("outNight");
                else AudioManager.Instance.PlayAmbient("inNight");
            }
            /*
            switch (isDay, isOutdoors)
            {
                case (true, true):
                    AudioManager.Instance.PlayAmbient("outDay");
                    break;
                case (true, false):
                    AudioManager.Instance.PlayAmbient("inDay");
                    break;
                case (false, true):
                    AudioManager.Instance.PlayAmbient("outNight");
                    break;
                case (false, false):
                    AudioManager.Instance.PlayAmbient("inNight");
                    break;
            }
            */
        }
    }
}
