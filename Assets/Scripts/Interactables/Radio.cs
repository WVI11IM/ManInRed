using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    public bool isOn = false;
    bool isPlaying = false;
    bool isSuspicious = false;

    public TextMeshProUGUI radioText;

    public Animator playerAnimator;

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
                radioText.text = "Desligar rádio";
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
                radioText.text = "Rádio";
                isPlaying = false;
            }
        }
    }

    public void RadioSwitch()
    {
        isOn = !isOn;
        if(isOn) AudioManager.Instance.PlaySoundEffectLoop("radio");
        else AudioManager.Instance.StopSoundEffectLoop("radio");

        playerAnimator.SetTrigger("interacted");
    }

    public void RadioSwitch(bool isActive)
    {
        if (isOn)
        {
            AudioManager.Instance.StopSoundEffectLoop("radio");
        }

        isOn = isActive;
        if (isActive) AudioManager.Instance.PlaySoundEffectLoop("radio");
    }
}
