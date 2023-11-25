using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public bool isOn = false;
    bool isPlaying = false;
    bool isSuspicious = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (!isPlaying)
            {
                PlayerStats.Instance.ModifyPressureMultiplier(-1);
                if (TimeManager.Instance.hour < 6)
                {
                    PlayerStats.Instance.ModifySuspicionMultiplier(1);
                    isSuspicious = true;
                }
                isPlaying = true;
            }
            
        }
        else
        {
            if (isPlaying)
            {
                PlayerStats.Instance.ModifyPressureMultiplier(1);
                if (isSuspicious)
                {
                    PlayerStats.Instance.ModifySuspicionMultiplier(-1);
                    isSuspicious = false;
                }
                isPlaying = false;
            }
        }
    }

    public void RadioSwitch()
    {
        isOn = !isOn;
        if(isOn) AudioManager.Instance.PlaySoundEffectLoop("radio");
        else AudioManager.Instance.StopSoundEffectLoop("radio");
    }

    public void RadioSwitch(bool isActive)
    {
        isOn = isActive;
        if (isActive) AudioManager.Instance.PlaySoundEffectLoop("radio");
        else AudioManager.Instance.StopSoundEffectLoop("radio");
    }
}
