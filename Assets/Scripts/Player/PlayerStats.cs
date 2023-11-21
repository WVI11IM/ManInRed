using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    //This script contains both pressure and suspicion stats

    private static PlayerStats _instance;
    public static PlayerStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerStats>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("PlayerStats");
                    _instance = singleton.AddComponent<PlayerStats>();
                }
            }
            return _instance;
        }
    }

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

    [Header("States")]
    public bool onMinigame = false;


    void Start()
    {
    }

    void Update()
    {
        StatsUpdate();
    }

    void StatsUpdate()
    {
        if (pressureMultiplier <= 0)
        {
            pressureMeterAnimator.SetInteger("modifier", 0);
            if (isPlayingPressureSound)
            {
                AudioManager.Instance.StopSoundEffectLoop("pressureIncrease");
                isPlayingPressureSound = false;
            }
            pressureMultiplier = 0;
        }

        if (suspicionMultiplier <= 0)
        {
            suspicionMeterAnimator.SetInteger("modifier", 0);
            if (isPlayingSuspicionSound)
            {
                AudioManager.Instance.StopSoundEffectLoop("suspicionIncrease");
                isPlayingSuspicionSound = false;
            }
            suspicionMultiplier = 0;
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

        if (value > 0)
        {
            AudioManager.Instance.PlaySoundEffectLoop("pressureIncrease");
        }

        while (elapsedTime < duration)
        {
            mainPressure = Mathf.RoundToInt(Mathf.Lerp(startPressure, targetPressure, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pressureMeterAnimator.SetInteger("modifier", 0);
        mainPressure = targetPressure;
        AudioManager.Instance.StopSoundEffectLoop("PressureIncrease");
    }

    public void ModifyPressureMultiplier(float valueToAdd)
    {
        pressureMultiplier += valueToAdd;
    }

    void ModifySuspicion(float value)
    {
        //Continuously modifies suspicion while it's being called
        mainSuspicion += value * Time.deltaTime;
        suspicionMeterAnimator.SetInteger("modifier", (int)value);
        if (!isPlayingSuspicionSound)
        {
            AudioManager.Instance.PlaySoundEffectLoop("suspicionIncrease");
        }
    }
    public void ModifySuspicion(int value)
    {
        //Adds/Removes fixed value of pressure through half a second
        StartCoroutine(ModifySuspicionOverTime(value, 0.5f));
        suspicionMeterAnimator.SetInteger("modifier", value);
    }

    private IEnumerator ModifySuspicionOverTime(int value, float duration)
    {
        float elapsedTime = 0.0f;
        float startSuspicion = mainSuspicion;
        float targetSuspicion = mainSuspicion + value;

        if (value > 0)
        {
            AudioManager.Instance.PlaySoundEffectLoop("suspicionIncrease");
        }

        while (elapsedTime < duration)
        {
            mainPressure = Mathf.RoundToInt(Mathf.Lerp(startSuspicion, targetSuspicion, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        suspicionMeterAnimator.SetInteger("modifier", 0);
        mainSuspicion = targetSuspicion;
        AudioManager.Instance.StopSoundEffectLoop("suspicionIncrease");
    }

    public void ModifySuspicionMultiplier(float valueToAdd)
    {
        suspicionMultiplier += valueToAdd;
    }
}