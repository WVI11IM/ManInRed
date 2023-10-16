using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{

    public enum States
    {
        HIDDEN,//0
        ACTIVE,//1
        FINISHED//2
    }

    [Tooltip("Text that will be displayed in the tasks list.")]
    [TextArea]
    public string taskDescription;
    [Tooltip("The task's current state.")]
    public States currentState;
    [Tooltip("The task's identification number.")]
    public int taskId;
    [Tooltip("UI Text that will be generated in the tasks list.")]
    public GameObject descriptionObject;
    [Tooltip("Vertical Layout Group that will contain all task texts.")]
    public LayoutGroup layoutGroup;

    void Start()
    {
        //Create the task description object from the prefab
        descriptionObject = Instantiate(Resources.Load("TaskDescription") as GameObject);
        descriptionObject.transform.SetParent(layoutGroup.transform); // Set the parent to the Layout Group
        descriptionObject.GetComponent<Text>().text = taskDescription; // Set the text of the description object
        descriptionObject.SetActive(false); // Initially, set it as inactive
    }

    void Update()
    {
        //Activates/Deactivates the task's UI text in the task list
        switch (currentState)
        {
            case (States.HIDDEN):
                descriptionObject.SetActive(false);
                break;
            case (States.ACTIVE):
                descriptionObject.SetActive(true);
                break;
            case (States.FINISHED):
                descriptionObject.SetActive(false);
                break;
        }
    }

    public void ChangeState(int state)
    {
        //Changes the task's state. Must be 0, 1 or 2
        switch (state)
        {
            case (0):
                currentState = States.HIDDEN;
                break;
            case (1):
                currentState = States.ACTIVE;
                break;
            case (2):
                currentState = States.FINISHED;
                break;
            default:
                Debug.Log("No state available for int " + state);
                break;

        }
    }
}
