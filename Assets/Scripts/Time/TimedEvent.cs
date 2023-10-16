using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TimedEvent
{
    //Class created for TimeManager

    public UnityEvent eventCallback;

    [Range(1, 4)]
    public int day;
    [Range(0, 24)]
    public int hour;
    [Range(0, 60)]
    public int minute;

    public bool wasCalled;
}
