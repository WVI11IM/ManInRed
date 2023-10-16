using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TimeSkip
{
    //Class created for TimeManager

    public string description;
    public UnityEvent eventCallback;

    public float timeToSkip;
    public float durationOfSkip;
}
