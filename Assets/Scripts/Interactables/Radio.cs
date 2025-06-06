using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    public bool isOn = false;
    bool isPlaying = false;
    bool playerIsInApartment;

    public GameObject particles;

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
        if (isOn && !TimeManager.Instance.timerIsPaused)
        {
            if (playerIsInApartment)
            {
                PlayerStats.Instance.ModifyPressurePerFrame(pressureToDecrease);
            }
            
            if (!isPlaying)
            {
                radioText.text = "Desligar r�dio";
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
                radioText.text = "R�dio";
                isPlaying = false;
            }
        }

        if (isOn) particles.SetActive(true);
        else particles.SetActive(false);
    }

    public void RadioSwitch()
    {
        isOn = !isOn;
        if (isOn)
        {
            AudioManager.Instance.PlaySoundEffectLoop("radio");
            AudioManager.Instance.PlaySoundEffect("radioOn");
        }
        else
        {
            AudioManager.Instance.StopSoundEffectLoop("radio");
            AudioManager.Instance.PlaySoundEffect("radioOff");
        }

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

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInApartment = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInApartment = false;
        }
    }
}
