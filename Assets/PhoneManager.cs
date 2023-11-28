using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public Dialogue[] roseDialogues;
    public Dialogue[] professionalsDialogues;
    PlayerStats playerStats;
    private bool toldAboutCuttingEverything = false;
    private bool sentFridge = false;
    private bool sentWardrobe = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallRose()
    {
        if (playerStats.progressIds[10].progressIsMade)
        {
            if(playerStats.mainSuspicion > 25) roseDialogues[4].StartDialogue();
            else roseDialogues[3].StartDialogue();
        }
        else if (playerStats.progressIds[0].progressIsMade)
        {
            if (toldAboutCuttingEverything)
            {
                roseDialogues[2].StartDialogue();
            }
            else
            {
                roseDialogues[1].StartDialogue();
                toldAboutCuttingEverything = true;
            }
        }
        else
        {
            roseDialogues[0].StartDialogue();
        }
    }

    public void CallProfessionals()
    {
        if(playerStats.progressIds[3].progressIsMade && playerStats.progressIds[6].progressIsMade)
        {
            professionalsDialogues[3].StartDialogue();
            sentWardrobe = true;
            sentFridge = true;
        }
        else if (playerStats.progressIds[6].progressIsMade && !sentWardrobe)
        {
            professionalsDialogues[2].StartDialogue();
            sentWardrobe = true;
        }
        else if(playerStats.progressIds[3].progressIsMade && !sentFridge)
        {
            professionalsDialogues[1].StartDialogue();
            sentFridge = true;
        }
        else
        {
            professionalsDialogues[0].StartDialogue();
        }
    }
}
