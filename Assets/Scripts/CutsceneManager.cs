using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public Cutscene[] cutscenes;

    public void PlayCutscene(int id)
    {
        for(int i = 0; i < cutscenes.Length; i++)
        {
            if (cutscenes[i].id == id)
            {
                if (!cutscenes[i].alreadyPlayed)
                {
                    Debug.Log("Playing cutscene " + id);
                    cutscenes[i].alreadyPlayed = true;
                    cutscenes[i].director.Play();
                }
            }
        }

    }
}