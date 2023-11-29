using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsToHide : MonoBehaviour
{
    public Task[] hidePartsTasks;
    public AnnaParts annaParts;
    public int partsToHide;

    // Start is called before the first frame update
    void Start()
    {
        partsToHide = annaParts.partsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        switch (partsToHide)
        {
            case 4:
                break;
            case 3:
                hidePartsTasks[0].ChangeState(2);
                TaskManager.Instance.CompleteTask(6);
                break;
            case 2:
                hidePartsTasks[0].ChangeState(2);
                hidePartsTasks[1].ChangeState(2);
                TaskManager.Instance.CompleteTask(7);
                break;
            case 1:
                hidePartsTasks[0].ChangeState(2);
                hidePartsTasks[1].ChangeState(2);
                hidePartsTasks[2].ChangeState(2);
                TaskManager.Instance.CompleteTask(8);
                break;
            case 0:
                PlayerStats.Instance.progressIds[10].progressIsMade = true;
                hidePartsTasks[0].ChangeState(2);
                hidePartsTasks[1].ChangeState(2);
                hidePartsTasks[2].ChangeState(2);
                hidePartsTasks[3].ChangeState(2);
                TaskManager.Instance.CompleteTask(9);
                break;
        }
    }

    public void PartWasDisposed()
    {
        partsToHide--;
    }
}
