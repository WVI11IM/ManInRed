using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource soundEffectSource;
    public AudioSource musicSource;

    public float soundEffectVolume = 1.0f;
    public float musicVolume = 0.7f;

    public List<AudioData> musicList = new List<AudioData>();
    public List<AudioData> soundEffectsList = new List<AudioData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(string musicName)
    {
        AudioData music = musicList.Find(audio => audio.name == musicName);
        if (music.clip != null)
        {
            musicSource.clip = music.clip;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Method to play a sound effect by name
    public void PlaySoundEffect(string soundEffectName)
    {
        AudioData soundEffect = soundEffectsList.Find(audio => audio.name == soundEffectName);
        if (soundEffect.clip != null)
        {
            soundEffectSource.PlayOneShot(soundEffect.clip, soundEffectVolume);
        }
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }
}