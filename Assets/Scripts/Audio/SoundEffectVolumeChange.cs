using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectVolumeChange : MonoBehaviour
{
    AudioSource audioSource;
    public float volume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = AudioManager.Instance.soundEffectVolume * volume;
    }
}
