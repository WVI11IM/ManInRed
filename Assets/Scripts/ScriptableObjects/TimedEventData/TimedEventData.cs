using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Timed Event Data")]
public class TimedEventData : ScriptableObject
{
    [Range(1, 3)]
    public int day;
    [Range(0, 24)]
    public int hour;
    [Range(0, 60)]
    public int minute;
    public int eventId;
    public bool wasCalled;
}
