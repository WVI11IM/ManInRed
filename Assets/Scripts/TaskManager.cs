using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public List<Task> taskList = new List<Task>();

    // Start is called before the first frame update
    void Start()
    {
        Task[] tasks = FindObjectsOfType<Task>();
        taskList.AddRange(tasks);

        foreach (Task obj in taskList)
        {
            Debug.Log(obj.gameObject.name + " added to the list!");
        }

        UpdateTaskList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update the task list in the UI
    void UpdateTaskList()
    {
        // Loop through the task list
        foreach (Task task in taskList)
        {
            // Check if the task is in the "ACTIVE" state
            if (task.currentState == Task.States.ACTIVE)
            {
                // Handle task object creation and management in the Task script
            }
        }
    }

    public void CompleteTask(int id)
    {
        Task taskToComplete = taskList.Find(task => task.taskId == id);

        if (taskToComplete != null && taskToComplete.currentState == Task.States.ACTIVE)
        {
            taskToComplete.currentState = Task.States.FINISHED;
            Debug.Log("Task " + taskToComplete.taskId + " is now FINISHED.");
        }
        else
        {
            Debug.LogWarning("Task " + id + " not found or is not in the ACTIVE state.");
        }
    }
}
