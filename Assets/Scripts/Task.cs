using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public enum States
    {
        HIDDEN,
        ACTIVE,
        FINISHED
    }

    [TextArea]
    public string taskDescription;
    public States currentState;
    public int taskId;
    public GameObject descriptionObject;
    public LayoutGroup layoutGroup; // Reference to the Layout Group

    void Start()
    {
        // Create the task description object from the prefab
        descriptionObject = Instantiate(Resources.Load("TaskDescription") as GameObject);
        descriptionObject.transform.SetParent(layoutGroup.transform); // Set the parent to the Layout Group
        descriptionObject.GetComponent<Text>().text = taskDescription; // Set the text of the description object
        descriptionObject.SetActive(false); // Initially, set it as inactive

    }

    void Update()
    {
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
        }
    }
}
