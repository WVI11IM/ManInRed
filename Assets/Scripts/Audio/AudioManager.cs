using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource ambientSource;
    float currentAmbientSoundVolume;

    public float soundEffectVolume = 1.0f;
    public float ambientVolume = 0.7f;

    public List<AudioData> ambientList = new List<AudioData>();
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

        AudioListener.volume = 1.0f;
    }

    private void Update()
    {
        ambientSource.volume = ambientVolume * currentAmbientSoundVolume;
    }

    public void PlayAmbient(string ambientName)
    {
        AudioData ambient = ambientList.Find(audio => audio.name == ambientName);
        if (ambient.clip != null)
        {
            ambientSource.clip = ambient.clip;
            currentAmbientSoundVolume = ambient.volume;
            ambientSource.volume = ambientVolume * ambient.volume;

            float randomTime = Random.Range(0f, ambientSource.clip.length);
            ambientSource.time = randomTime;
            ambientSource.loop = true;
            ambientSource.Play();
        }
    }

    public void StopAmbient()
    {
        ambientSource.Stop();
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        AudioData soundEffect = soundEffectsList.Find(audio => audio.name == soundEffectName);
        if (soundEffect.clip != null)
        {
            GameObject soundGameObject = new GameObject("SoundEffect[" + soundEffectName + "]");
            AudioSource source = soundGameObject.AddComponent<AudioSource>();
            SoundEffectVolumeChange volumeChange = soundGameObject.AddComponent<SoundEffectVolumeChange>();

            if (soundEffect.sourcePosition3D != null)
            {
                source.spatialBlend = 1.0f;
                source.minDistance = soundEffect.minDist3D;
                source.maxDistance = soundEffect.maxDist3D;
                source.transform.position = soundEffect.sourcePosition3D.position;
            }
            else
            {
                source.spatialBlend = 0.0f;
            }
            source.clip = soundEffect.clip;
            volumeChange.volume = soundEffect.volume;
            source.volume = soundEffectVolume * volumeChange.volume;

            source.Play();

            Destroy(soundGameObject, source.clip.length);
        }
    }

    public void PlaySoundEffectLoop(string soundEffectName)
    {
        AudioData soundEffect = soundEffectsList.Find(audio => audio.name == soundEffectName);
        if (soundEffect.clip != null)
        {
            GameObject soundGameObject = new GameObject("SoundEffect[" + soundEffectName + "]");
            AudioSource source = soundGameObject.AddComponent<AudioSource>();
            SoundEffectVolumeChange volumeChange = soundGameObject.AddComponent<SoundEffectVolumeChange>();

            if (soundEffect.sourcePosition3D != null)
            {
                source.spatialBlend = 1.0f;
                source.minDistance = soundEffect.minDist3D;
                source.maxDistance = soundEffect.maxDist3D;
                source.transform.position = soundEffect.sourcePosition3D.position;
            }
            else
            {
                source.spatialBlend = 0.0f;
            }
            source.clip = soundEffect.clip;
            volumeChange.volume = soundEffect.volume;
            source.volume = soundEffectVolume * volumeChange.volume;

            float randomTime = Random.Range(0f, source.clip.length);
            source.time = randomTime;

            source.loop = true;
            source.Play();
        }
    }

    public void StopSoundEffectLoop(string soundEffectName)
    {
        AudioData soundEffect = soundEffectsList.Find(audio => audio.name == soundEffectName);
        if (soundEffect.clip != null)
        {
            GameObject soundGameObject = GameObject.Find("SoundEffect[" + soundEffectName + "]");
            AudioSource source = soundGameObject.GetComponent<AudioSource>();
            SoundEffectVolumeChange volumeChange = soundGameObject.GetComponent<SoundEffectVolumeChange>();

            Destroy(soundGameObject);
        }
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
    }

    public void SetAmbientVolume(float volume)
    {
        ambientVolume = volume;
    }
}