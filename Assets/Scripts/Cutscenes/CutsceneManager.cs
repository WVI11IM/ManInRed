using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    private static CutsceneManager _instance;
    public static CutsceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CutsceneManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("CutsceneManager");
                    _instance = singleton.AddComponent<CutsceneManager>();
                }
            }
            return _instance;
        }
    }

    public Cutscene[] cutscenes;
    [HideInInspector] public Movement playerMovement;
    public GameObject phoneCanvas;
    public PoliceInspection policeInspection;

    [Tooltip("All the GameObjects that must be hidden during dialogues and cutscenes")]
    public GameObject[] objectsToHide;


    private void Start()
    {
        playerMovement = FindObjectOfType<Movement>().GetComponent<Movement>();
    }

    private void Update()
    {
        for (int i = 0; i < cutscenes.Length; i++)
        {
            if (cutscenes[i].director.state == PlayState.Playing || DialogueManager.Instance.isActive && !DialogueManager.Instance.isThinking)
            {
                ActivateObjects(false);
            }
            else
            {
                ActivateObjects(true);
            }

            if (cutscenes[i].director.state == PlayState.Playing)
            {
                TimeManager.Instance.ClockSwitch(false);
                playerMovement.CanMove(false);
                break;
            }
            else if (!DialogueManager.Instance.isActive && !phoneCanvas.activeSelf)
            {
                TimeManager.Instance.ClockSwitch(true);
                playerMovement.CanMove(true);
            }
            else
            {
                if (DialogueManager.Instance.isThinking)
                {
                    TimeManager.Instance.ClockSwitch(true);
                    playerMovement.CanMove(true);
                }
                else TimeManager.Instance.ClockSwitch(false);
                playerMovement.CanMove(false);
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

    public void ActivateObjects(bool areActive)
    {
        for (int i = 0; i < objectsToHide.Length; i++)
        {
            objectsToHide[i].SetActive(areActive);
        }
    }
}