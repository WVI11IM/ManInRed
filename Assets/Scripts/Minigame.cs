using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Minigame : MonoBehaviour
{
    public Image progressMeter;
    public float progressPercentage;
    public TextMeshProUGUI progressText;

    public float progressSpeed;
    bool minigameIsActive = false;

    public UnityEvent whenFinished;
    public UnityEvent whenLosing;

    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MinigameUpdate();
    }

    void MinigameUpdate()
    {
        progressMeter.fillAmount = progressPercentage / 100;
        progressText.text = Mathf.FloorToInt(progressPercentage) + "%";

        if (minigameIsActive && playerStats.mainPressure < 100 && playerStats.mainSuspicion < 100)
        {
            progressPercentage += progressSpeed * Time.deltaTime;
        }

        if (progressPercentage >= 100)
        {
            whenFinished.Invoke();
            Debug.Log("Minigame Finished!");
        }

        if (playerStats.mainPressure >= 100)
        {
            whenLosing.Invoke();
            Debug.Log("Minigame Failed!");
        }
    }

    public void ActivateMinigame(bool isActive)
    {
        minigameIsActive = isActive;
        PlayerStats.Instance.onMinigame = isActive;
    }
}
