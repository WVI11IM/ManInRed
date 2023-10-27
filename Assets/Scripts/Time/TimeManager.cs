using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //This script manages the in-game time and also controls the clock and lights

    private static TimeManager _instance;
    public static TimeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TimeManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("TimeManager");
                    _instance = singleton.AddComponent<TimeManager>();
                }
            }
            return _instance;
        }
    }

    public Image clockImage;
    [Tooltip("In-game minutes passed ever since midnight of day 1.")]
    public float timer = 0;
    [Tooltip("Frequency of in-game minutes per second.")]
    public float timeFrequency;
    [Tooltip("If true, stops the timer.")]
    public bool timerIsPaused = false;
    [Tooltip("If true, time is being skipped.")]
    public bool skippingTime = false;
    [SerializeField] Animator timeTextAnimator;

    [Header("Visual Elements")]
    public Gradient backgroundColors;
    public Light sunLight;
    public Light moonLight;
    public Text timeText;
    public Text dayText;

    [Header("Clock")]
    [Range(0, 60)]
    public int minute = 0;
    [Range(0, 24)]
    public int hour = 0;
    [Range(1, 4)]
    public int day = 1;

    public TimedEvent[] timedEvents;
    public enum SkippingTime
    {
        SMOKING,
        SLEEPING,
    }

    private void Awake()
    {
        FixTimedEvents();
    }

    void Update()
    {
        ClockUpdate();

        Clock(!timerIsPaused);

        CheckTimedEvents();
    }

    public void ClockUpdate()
    {
        //Calculates the time according to the float "timer"
        minute = (int)timer - (hour * 60) - ((day - 1) * 1440);
        hour = ((int)timer / 60) - ((day - 1) * 24);
        day = ((int)timer / 1440) + 1;

        //Displays the text of the current time
        string clockTime = string.Format("{0:D2}h{1:D2}", hour, minute);
        timeText.text = clockTime;
        dayText.text = "DAY " + day;

        //Fills the amount for the clock image
        if (hour < 12)
        {
            clockImage.fillClockwise = true;
            clockImage.fillAmount = Mathf.Lerp(0f, 1f, ((timer / 60) - ((day - 1) * 24))/ 12);
        }
        else
        {
            clockImage.fillClockwise = false;
            clockImage.fillAmount = Mathf.Lerp(1f, 0f, (((timer / 60) - ((day - 1) * 24)) - 12) / 12);
        }

        //Changes the background color
        Camera.main.backgroundColor = backgroundColors.Evaluate(timer / 1440 - (day - 1));

        //Rotates the Directional Light
        float rotationAngle = (timer / 1440) * 360.0f + 180;
        sunLight.transform.rotation = Quaternion.Euler(new Vector3(52.5f, rotationAngle, 0));
        moonLight.transform.rotation = Quaternion.Euler(new Vector3(52.5f, rotationAngle + 180, 0));

        //Updates the light intensity by checking the current hour
        if (hour == 6)
        {
            sunLight.intensity = Mathf.Lerp(0f, 1f, minute / 60f);
            moonLight.intensity = Mathf.Lerp(0.5f, 0f, minute / 60f);
        }
        else if (hour == 17)
        {
            sunLight.intensity = Mathf.Lerp(1f, 0f, minute / 60f);
            moonLight.intensity = Mathf.Lerp(0f, 0.5f, minute / 60f);
        }
        else if (6 < hour && hour < 17)
        {
            sunLight.intensity = 1;
            moonLight.intensity = 0.5f;
        }
        else //(hour < 6 || hour > 17) 
        {
            sunLight.intensity = 0;
            moonLight.intensity = 0.5f;
        }

    }
    void Clock(bool isWorking)
    {
        //Makes time flow only if "isWorking" is true
        if (isWorking)
        {
            timer += timeFrequency * Time.deltaTime;
        }
        else return;
    }

    public void ClockSwitch(bool isRunning)
    {
        //Public function to pause/resume time. Mostly used for UnityEvents in the unity inspector
        if (isRunning) timerIsPaused = false;
        else timerIsPaused = true;
    }

    void FixTimedEvents()
    {
        //Only used when starting the game after some in-game time has already passed. This function considers a timed event to have been called if the in-game timer went over it
        foreach (var data in timedEvents)
        {
            if (data.day <= day &&
                data.hour <= hour &&
                data.minute <= minute &&
                !data.wasCalled)
            {
                data.wasCalled = true;
            }
            else data.wasCalled = false;
        }
    }

    void CheckTimedEvents()
    {
        //Invokes the timed event at the right time
        foreach(var data in timedEvents)
        {
            if (data.day <= day &&
                data.hour <= hour &&
                data.minute <= minute &&
                !data.wasCalled)
            {
                data.eventCallback.Invoke();
                data.wasCalled = true;
            }
        }
    }

    public void SkipTimeForSmoking()
    {
        SkipTime(SkippingTime.SMOKING);
    }

    public void SkipTimeForSleeping()
    {
        SkipTime(SkippingTime.SLEEPING);
    }

    void SkipTime(SkippingTime action)
    {
        if (!skippingTime)
        {
            timerIsPaused = true;
            switch (action)
            {
                case (SkippingTime.SMOKING):
                    StartCoroutine(SkipMinutes(60, 0.5f));
                    break;
                case (SkippingTime.SLEEPING):
                    StartCoroutine(SkipMinutes(360, 5));
                    break;
                default:
                    break;
            }
        }
        else Debug.Log("Time is already being skipped!");
    }

    private IEnumerator SkipMinutes(int minutes, float duration)
    {
        float elapsedTime = 0.0f;
        float startTime = timer;
        float targetTime = timer + minutes;
        while (elapsedTime < duration)
        {
            timeTextAnimator.SetBool("isSkippingTime", true);
            skippingTime = true;
            timer = Mathf.RoundToInt(Mathf.Lerp(startTime, targetTime, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        skippingTime = false;
        timerIsPaused = false;
        timeTextAnimator.SetBool("isSkippingTime", false);
        timer = targetTime;
    }
}