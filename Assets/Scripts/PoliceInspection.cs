using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceInspection : MonoBehaviour
{
    public bool policeCame = false;
    public bool finishedAllTasks = false;
    public int inspectionIndex = 0;

    public Dialogue skipToEnd;

    [Header("Objects to check for")]
    public GameObject[] bodyInBathtub;
    public GameObject annaStuff;
    public CheckFloorItems[] checkFloorItems;

    [Header("Dialogues")]
    public Dialogue suspiciousItemOnHandDialogue;
    public Dialogue dirtyClothesDialogue;
    public Dialogue lowSuspicionLowPressureDialogue;
    public Dialogue lowSuspicionHighPressureDialogue;
    public Dialogue highSuspicionLowPressureDialogue;
    public Dialogue highSuspicionHighPressureDialogue;

    public Dialogue hasBodyInBathtubDialogue;
    public Dialogue hasBodyInFridgeNoRopeDialogue;
    public Dialogue hasBodyInWardrobeNoRopeDialogue;
    public Dialogue hasBloodInDialogue;
    public Dialogue hasBloodySawDialogue;
    public Dialogue hasSuitcaseBodyDialogue;

    public Dialogue hasBloodOutDialogue;
    public Dialogue hasBodyInFridgeWithRopeDialogue;
    public Dialogue hasBodyInWardrobeWithRopeDialogue;
    public Dialogue tookOutFridgeDialogue;
    public Dialogue tookOutWardrobeDialogue;
    public Dialogue hasAnnaStuffDialogue;
    public Dialogue arrestDialogue;
    public Dialogue leaveDialogue;

    bool hasBodyInBathtubCheck = false;
    bool hasBloodInCheck = false;
    bool hasBloodySawCheck = false;
    bool hasSuitcaseBodyCheck = false;
    bool hasBloodOutCheck = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!policeCame && TimeManager.Instance.timer >= 4320)
        {
            CutsceneManager.Instance.PlayCutscene(7);
            policeCame = true;
        }

        if(!finishedAllTasks && AreAllTasksFinished() && TimeManager.Instance.timer < 4320)
        {
            skipToEnd.StartDialogue();
            finishedAllTasks = true;
        }
    }

    public void PoliceAtDoor()
    {
        if (PlayerStats.Instance.isDirty)
        {
            dirtyClothesDialogue.StartDialogue();
        }
        else if(Inventory.Instance.HasItem(1)|| Inventory.Instance.HasItem(3)|| Inventory.Instance.HasItem(4))
        {
            suspiciousItemOnHandDialogue.StartDialogue();
        }
        else if (PlayerStats.Instance.mainPressure >= 60)
        {
            if (PlayerStats.Instance.mainSuspicion >= 50)
            {
                highSuspicionHighPressureDialogue.StartDialogue();
            }
            else lowSuspicionHighPressureDialogue.StartDialogue();
        }
        else
        {
            if (PlayerStats.Instance.mainSuspicion > 50)
            {
                highSuspicionLowPressureDialogue.StartDialogue();
            }
            else lowSuspicionLowPressureDialogue.StartDialogue();

        }
    }

    public bool AreAllTasksFinished()
    {
        foreach (Task task in TaskManager.Instance.taskList)
        {
            if (task.currentState != Task.States.FINISHED)
            {
                if (task.currentState != Task.States.FINISHED)
                {
                    return false; // At least one task is not finished
                }
            }
        }
        return true; // All tasks are finished
    }

    //

    public void HasBodyInBathtub()
    {
        for(int i = 0; i < bodyInBathtub.Length; i++)
        {
            if(bodyInBathtub[i].activeSelf == true && !hasBodyInBathtubCheck)
            {
                hasBodyInBathtubCheck = true;
                break;
            }
        }
        if (hasBodyInBathtubCheck)
        {
            hasBodyInBathtubDialogue.StartDialogue();
        }
        else HasBodyInFridgeNoRope();
    }

    public void HasBodyInFridgeNoRope()
    {
        if (PlayerStats.Instance.progressIds[2].progressIsMade && !PlayerStats.Instance.progressIds[3].progressIsMade)
        {
            hasBodyInFridgeNoRopeDialogue.StartDialogue();
        }
        else HasBodyInWardrobeNoRope();
    }

    public void HasBodyInWardrobeNoRope()
    {
        if (PlayerStats.Instance.progressIds[5].progressIsMade && !PlayerStats.Instance.progressIds[6].progressIsMade)
        {
            hasBodyInWardrobeNoRopeDialogue.StartDialogue();
        }
        else HasBloodIn();
    }

    public void HasBloodIn()
    {
        for (int i = 0; i < checkFloorItems.Length; i++)
        {
            if (i <= 3)
            {
                if (checkFloorItems[i].hasBlood && !hasBloodInCheck)
                {
                    hasBloodInCheck = true;
                    break;
                }
            }
        }
        if (hasBloodInCheck)
        {
            hasBloodInDialogue.StartDialogue();
        }
        else HasBloodySaw();
    }

    public void HasBloodySaw()
    {
        for (int i = 0; i < checkFloorItems.Length; i++)
        {
            if (checkFloorItems[i].hasDirtySaw && !hasBloodySawCheck)
            {
                hasBloodySawCheck = true;
                break;
            }
        }
        if (hasBloodySawCheck)
        {
            hasBloodySawDialogue.StartDialogue();
        }
        else HasSuitcaseBody();
    }

    public void HasSuitcaseBody()
    {
        for (int i = 0; i < checkFloorItems.Length; i++)
        {
            if (checkFloorItems[i].hasSuitcasePart || checkFloorItems[i].hasSuitcasePartNewspaper && !hasSuitcaseBodyCheck)
            {
                hasSuitcaseBodyCheck = true;
                break;
            }
        }
        if (hasSuitcaseBodyCheck)
        {
            hasSuitcaseBodyDialogue.StartDialogue();
        }
        else HasBloodOut();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    public void HasBloodOut()
    {
        for (int i = 0; i < checkFloorItems.Length; i++)
        {
            if (i > 3)
            {
                if (checkFloorItems[i].hasBlood && !hasBloodOutCheck)
                {
                    hasBloodOutCheck = true;
                    break;
                }
            }
        }
        if (hasBloodOutCheck)
        {
            hasBloodOutDialogue.StartDialogue();
        }
        else HasBodyInFridgeWithRope();
    }

    public void HasBodyInFridgeWithRope()
    {
        if (PlayerStats.Instance.progressIds[3].progressIsMade && !PlayerStats.Instance.progressIds[4].progressIsMade)
        {
            hasBodyInFridgeWithRopeDialogue.StartDialogue();
        }
        else HasBodyInWardrobeWithRope();
    }

    public void HasBodyInWardrobeWithRope()
    {
        if (PlayerStats.Instance.progressIds[6].progressIsMade && !PlayerStats.Instance.progressIds[7].progressIsMade)
        {
            hasBodyInWardrobeWithRopeDialogue.StartDialogue();
        }
        else TookOutFridge();
    }

    public void TookOutFridge()
    {
        if (PlayerStats.Instance.progressIds[4].progressIsMade)
        {
            tookOutFridgeDialogue.StartDialogue();
        }
        else TookOutWardrobe();
    }

    public void TookOutWardrobe()
    {
        if (PlayerStats.Instance.progressIds[7].progressIsMade)
        {
            tookOutWardrobeDialogue.StartDialogue();
        }
        else HasAnnaStuff();
    }

    public void HasAnnaStuff()
    {
        if (annaStuff.activeSelf)
        {
            hasAnnaStuffDialogue.StartDialogue();
        }
        else LastSuspicionCheck();
    }

    public void LastSuspicionCheck()
    {
        if (PlayerStats.Instance.mainSuspicion >= 100)
        {
            arrestDialogue.StartDialogue();
        }
        else leaveDialogue.StartDialogue();
}
}
