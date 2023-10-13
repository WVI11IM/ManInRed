using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTaskWithConditions : MonoBehaviour
{
    //Link this script to a task which depends on other task's completion in order to activate by itself

    Task task;

    [Tooltip("Write down all the taskIds of the required tasks.")]
    public int[] requiredTaskIds;

    private void Awake()
    {
        task = GetComponent<Task>();
    }

    private void Update()
    {
        if (ShouldActivateTask())
        {
            task.currentState = Task.States.ACTIVE;
        }
    }

    // Check if all the required tasks are finished and returns true if they're finished
    private bool ShouldActivateTask()
    {
        foreach (int requiredTaskId in requiredTaskIds)
        {
            Task requiredTask = TaskManager.Instance.taskList.Find(task => task.taskId == requiredTaskId);

            if (requiredTask == null || requiredTask.currentState != Task.States.FINISHED)
            {
                return false;
            }
        }
        return true;
    }
}
