using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //This script contains both pressure and suspicion stats

    [Header("Meters")]
    public Image pressureMeter;
    public Image suspicionMeter;

    [Header("Pressure")]
    public bool onPressureZone = false;
    public float mainPressure = 0;
    public float pressureMultiplier = 0;

    [Header("Suspicion")]
    public bool onSuspicionZone = false;
    public float mainSuspicion = 0;
    public float suspicionMultiplier = 0;

    void Start()
    {
    }

    void Update()
    {
        StatsUpdate();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("pressure")) onPressureZone = true;
        if (other.CompareTag("suspicion")) onSuspicionZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("pressure")) onPressureZone = false;
        if (other.CompareTag("suspicion")) onSuspicionZone = false;
    }

    void StatsUpdate()
    {
        pressureMeter.fillAmount = mainPressure / 100;
        suspicionMeter.fillAmount = mainSuspicion / 100;

        if (!TimeManager.Instance.timerIsPaused)
        {
            if (onPressureZone)
            {
                ModifyPressure(pressureMultiplier);
            }
            if (onSuspicionZone)
            {
                mainSuspicion += suspicionMultiplier * Time.deltaTime;
            }
        }

        if (mainPressure < 0) mainPressure = 0;
        if (mainPressure > 100) mainPressure = 100;
    }

    //Continuously modifies pressure while it's being called
    void ModifyPressure(float value)
    {
        mainPressure += value * Time.deltaTime;
    }

    //Adds/Removes fixed value of pressure through half a second
    public void ModifyPressure(int value)
    {
        StartCoroutine(ModifyPressureOverTime(value, 0.5f));
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
        mainPressure = targetPressure;
    }
}