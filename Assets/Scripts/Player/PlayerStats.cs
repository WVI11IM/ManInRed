using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    //This script contains both pressure and suspicion stats

    [Header("Meters")]
    public Image pressureMeter;
    public Image suspicionMeter;

    [Header("Meter Animators")]
    [SerializeField] Animator pressureMeterAnimator;
    [SerializeField] Animator suspicionMeterAnimator;

    [Header("Pressure")]
    public bool onPressureZone = false;
    public float mainPressure = 0;
    public float pressureMultiplier = 0;

    [Header("Suspicion")]
    public bool onSuspicionZone = false;
    public float mainSuspicion = 0;
    public float suspicionMultiplier = 0;
    public Volume pressurePP;

    bool isPlayingPressureSound = false;
    bool isPlayingSuspicionSound = false;


    void Start()
    {
    }

    void Update()
    {
        StatsUpdate();
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("pressure")) onPressureZone = true;
        if (other.CompareTag("suspicion")) onSuspicionZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("pressure"))
        {
            onPressureZone = false;
            pressureMeterAnimator.SetInteger("modifier", 0);
            AudioManager.Instance.StopSoundEffectLoop("pressureIncrease");
            isPlayingPressureSound = false;
        }
        if (other.CompareTag("suspicion"))
        {
            onSuspicionZone = false;
            suspicionMeterAnimator.SetInteger("modifier", 0);
            AudioManager.Instance.StopSoundEffectLoop("suspicionIncrease");
            isPlayingSuspicionSound = false;
        }
    }
    */

    void StatsUpdate()
    {
        if (pressureMultiplier <= 0)
        {
            pressureMeterAnimator.SetInteger("modifier", 0);
            AudioManager.Instance.StopSoundEffectLoop("pressureIncrease");
            isPlayingPressureSound = false;
        }

        if (suspicionMultiplier <= 0)
        {
            suspicionMeterAnimator.SetInteger("modifier", 0);
            AudioManager.Instance.StopSoundEffectLoop("suspicionIncrease");
            isPlayingSuspicionSound = false;
        }

        //Being called by Update(), it updates the player's stats every frame
        pressureMeter.fillAmount = mainPressure / 100;
        suspicionMeter.fillAmount = mainSuspicion / 100;

        if (!TimeManager.Instance.timerIsPaused)
        {
            if (pressureMultiplier > 0)
            {
                ModifyPressure(pressureMultiplier);
            }
            if (suspicionMultiplier > 0)
            {
                ModifySuspicion(suspicionMultiplier);
            }
        }

        if (mainPressure < 0) mainPressure = 0;
        if (mainPressure > 60) pressurePP.weight = Mathf.Lerp(0f, 1f, (mainPressure - 60) / 40);
        if (mainPressure > 100)
        {
            mainPressure = 100;
            Debug.Log("Max Pressure!!");
        }

        if (mainSuspicion < 0) mainSuspicion = 0;
        if (mainSuspicion > 100)
        {
            mainSuspicion = 100;
            Debug.Log("Max Suspicion!!");
        }
    }

    void ModifyPressure(float value)
    {
        //Continuously modifies pressure while it's being called
        mainPressure += pressureMultiplier * Time.deltaTime;
        pressureMeterAnimator.SetInteger("modifier", (int)value);
        if (!isPlayingPressureSound)
        {
            AudioManager.Instance.PlaySoundEffectLoop("pressureIncrease");
            isPlayingPressureSound = true;
        }
    }


    public void ModifyPressure(int value)
    {
        //Adds/Removes fixed value of pressure through half a second
        StartCoroutine(ModifyPressureOverTime(value, 0.5f));
        pressureMeterAnimator.SetInteger("modifier", value);
    }

    private IEnumerator ModifyPressureOverTime(int value, float duration)
    {
        float elapsedTime = 0.0f;
        float startPressure = mainPressure;
        float targetPressure = mainPressure + value;

        while (elapsedTime < duration)
        {
            mainPressure = Mathf.RoundToInt(Mathf.Lerp(startPressure, targetPressure, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        suspicionMeterAnimator.SetInteger("modifier", 0);
        AudioManager.Instance.StopSoundEffectLoop("suspicionIncrease");
        mainPressure = targetPressure;
        isPlayingPressureSound = false;
    }

    void ModifySuspicion(float value)
    {
        //Continuously modifies suspicion while it's being called
        mainSuspicion += value * Time.deltaTime;
        suspicionMeterAnimator.SetInteger("modifier", (int)value);
        if (!isPlayingSuspicionSound)
        {
            AudioManager.Instance.PlaySoundEffectLoop("suspicionIncrease");
            isPlayingSuspicionSound = true;
        }
    }
}