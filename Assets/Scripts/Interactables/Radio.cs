using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    public bool isOn = false;
    bool isPlaying = false;

    public float pressureToDecrease;
    public float suspicionToIncreaseAtMidnight;

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
            PlayerStats.Instance.ModifyPressurePerFrame(pressureToDecrease);
            if (!isPlaying)
            {
                radioText.text = "Desligar rádio";
                isPlaying = true;
            }
            if (TimeManager.Instance.hour < 6)
            {
                PlayerStats.Instance.ModifySuspicionPerFrame(suspicionToIncreaseAtMidnight);
            }
        }
        else
        {
            if (isPlaying)
            {
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
