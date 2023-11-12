using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    [HideInInspector] public PlayableDirector director;
    public int id;
    public bool alreadyPlayed = false;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyPlayed)
        {
            director.Play();
            alreadyPlayed = true;
        }
    }

    public void PlayCutscene()
    {
        if (!alreadyPlayed)
        {
            Debug.Log("Playing cutscene " + id);
            alreadyPlayed = true;
            director.Play();
        }
    }
}
