using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public Dialogue[] roseDialogues;
    PlayerStats playerStats;
    private bool toldAboutCuttingEverything = false;

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
}
