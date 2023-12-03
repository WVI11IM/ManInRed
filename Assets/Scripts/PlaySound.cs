using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCutsceneSoundEffect(string soundName)
    {
        AudioManager.Instance.PlaySoundEffect(soundName);
    }

    public void PlayCutsceneAmbientSound(string soundName)
    {
        AudioManager.Instance.PlayAmbientFromStart(soundName);
    }
}
