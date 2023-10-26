using UnityEngine;

[System.Serializable]
public class AudioData
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;

    public Transform sourcePosition3D;
    public float minDist3D;
    public float maxDist3D;
}