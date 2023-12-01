using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class ProgressIds
{
    public string progressDescription;
    public int progressId;
    public bool progressIsMade = false;
}

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
    private float previousPressure = 0;
    public float pressureMultiplier = 0;

    [Header("Suspicion")]
    public bool onSuspicionZone = false;
    public float mainSuspicion = 0;
    private float previousSuspicion = 0;
    public float suspicionMultiplier = 0;
    public Volume pressurePP;

    bool isPlayingPressureSound = false;
    bool isPlayingSuspicionSound = false;

    [Header("States")]
    public bool onMinigame = false;
    public bool isDirty = false;
    public SkinnedMeshRenderer playerSkinnedMeshRenderer;
    public Material cleanMaterial;
    public Material dirtyMaterial;
    private Animator playerAnimator;

    [Header("Cutscene assets")]
    public Dialogue faintDialogue;
    public Dialogue maxSuspicionDialogue;
    public bool hasFainted = false;
    public bool wasCaught = false;

    [Header("Progress")]
    public ProgressIds[] progressIds;


    void Start()
    {
        cleanMaterial = playerSkinnedMeshRenderer.materials[1];
        dirtyMaterial = playerSkinnedMeshRenderer.materials[0];

        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        StatsUpdate();
    }

    void StatsUpdate()
    {
        float pressureDifference = mainPressure - previousPressure;
        pressureMeterAnimator.SetFloat("modifier", pressureDifference);
        if (!isPlayingPressureSound && pressureDifference > 0 && mainPressure > 0.5f)
        {
            AudioManager.Instance.PlaySoundEffectLoop("pressureIncrease");
            isPlayingPressureSound = true;
        }
        else if (isPlayingPressureSound && pressureDifference <= 0.0f)
        {
            AudioManager.Instance.StopSoundEffectLoop("pressureIncrease");
            isPlayingPressureSound = false;
        }
        previousPressure = mainPressure;

        float suspicionDifference = mainSuspicion - previousSuspicion;
        suspicionMeterAnimator.SetFloat("modifier", suspicionDifference);
        if (!isPlayingSuspicionSound && suspicionDifference > 0 && mainSuspicion > 0.5f)
        {
            AudioManager.Instance.PlaySoundEffectLoop("suspicionIncrease");
            isPlayingSuspicionSound = true;
        }
        else if (isPlayingSuspicionSound && suspicionDifference == 0)
        {
            AudioManager.Instance.StopSoundEffectLoop("suspicionIncrease");
            isPlayingSuspicionSound = false;
        }
        previousSuspicion = mainSuspicion;

        //Being called by Update(), it updates the player's stats every frame
        pressureMeter.fillAmount = mainPressure / 100;
        suspicionMeter.fillAmount = mainSuspicion / 100;

        if (!TimeManager.Instance.timerIsPaused)
        {
            ModifyPressure();
            ModifySuspicion();
        }

        if (mainPressure < 0) mainPressure = 0;
        if (mainPressure > 60) pressurePP.weight = Mathf.Lerp(0f, 1f, (mainPressure - 60) / 40);
        if (mainPressure > 100)
        {
            mainPressure = 100;
            Debug.Log("Max Pressure!!");
            if (!hasFainted && !TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
            {
                faintDialogue.StartDialogue();
                hasFainted = true;
            }

        }

        if (mainSuspicion < 0) mainSuspicion = 0;
        if (mainSuspicion > 100)
        {
            mainSuspicion = 100;
            Debug.Log("Max Suspicion!!");
            if (!wasCaught && !TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
            {
                maxSuspicionDialogue.StartDialogue();
                wasCaught = true;
            }
        }

        if (!isDirty)
        {
            playerSkinnedMeshRenderer.material = cleanMaterial;
        }
        else
        {
            playerSkinnedMeshRenderer.material = dirtyMaterial;
        }
    }

    public void ModifyPressure()
    {
        //Continuously modifies pressure by the multiplier while it's being called
        mainPressure += pressureMultiplier * Time.deltaTime;
    }
    public void ModifyPressurePerFrame(float value)
    {
        //Continuously modifies pressure by the value while it's being called
        mainPressure += value * Time.deltaTime;
    }

    public void ModifyPressure(int value)
    {
        //Adds/Removes fixed value of pressure through half a second
        StartCoroutine(ModifyPressureOverTime(value, 0.75f));
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

    public void ModifyPressureMultiplier(float valueToAdd)
    {
        pressureMultiplier += valueToAdd;
    }

    public void ModifySuspicion()
    {
        //Continuously modifies suspicion by the multiplier while it's being called
        mainSuspicion += suspicionMultiplier * Time.deltaTime;
    }

    public void ModifySuspicionPerFrame(float value)
    {
        //Continuously modifies pressure by the value while it's being called
        if(value > 0) mainSuspicion += value * Time.deltaTime;
    }
    public void ModifySuspicion(int value)
    {
        //Adds/Removes fixed value of pressure through half a second
        StartCoroutine(ModifySuspicionOverTime(value, 0.75f));
    }

    private IEnumerator ModifySuspicionOverTime(int value, float duration)
    {
        float elapsedTime = 0.0f;
        float startSuspicion = mainSuspicion;
        float targetSuspicion = mainSuspicion + value;

        while (elapsedTime < duration)
        {
            mainSuspicion = Mathf.RoundToInt(Mathf.Lerp(startSuspicion, targetSuspicion, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainSuspicion = targetSuspicion;
    }

    public void ModifySuspicionMultiplier(float valueToAdd)
    {
        suspicionMultiplier += valueToAdd;
    }

    public void ResetMultipliers()
    {
        pressureMultiplier = 0;
        suspicionMultiplier = 0;
    }

    public void BloodyClothes(bool isBloody)
    {
        isDirty = isBloody;
        if (!isBloody)
        {
            playerAnimator.SetTrigger("interacted");
        }
    }

    public bool CheckProgressId(int id)
    {
        if (progressIds[id].progressIsMade) return true;
        else return false;
    }

    public void DoProgress(int id)
    {
        for (int i = 0; i < progressIds.Length; i++)
        {
            if (progressIds[i].progressId == id)
            {
                progressIds[i].progressIsMade = true;
            }
        }
    }

    public void UndoProgress(int id)
    {
        for (int i = 0; i < progressIds.Length; i++)
        {
            if (progressIds[i].progressId == id)
            {
                progressIds[i].progressIsMade = false;
            }
        }
    }
}