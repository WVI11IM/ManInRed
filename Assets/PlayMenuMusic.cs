using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.StopAmbient();
        AudioManager.Instance.PlayAmbientFromStart("menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMenuMusic()
    {
        AudioManager.Instance.StopAmbient();
    }
}
