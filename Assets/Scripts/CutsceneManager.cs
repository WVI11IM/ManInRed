using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public Cutscene[] cutscenes;
    Movement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<Movement>().GetComponent<Movement>();
    }

    private void Update()
    {
        for (int i = 0; i < cutscenes.Length; i++)
        {
            if (cutscenes[i].director.state == PlayState.Playing)
            {
                TimeManager.Instance.ClockSwitch(false);
                playerMovement.CanMove(false);
                break;
            }
            else
            {
                if (!DialogueManager.Instance.isActive)
                {
                    TimeManager.Instance.ClockSwitch(true);
                    playerMovement.CanMove(true);
                }
            }
        }
    }

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