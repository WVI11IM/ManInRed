using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    //This script manages all existing tasks 

    private static TaskManager _instance;

    public static TaskManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TaskManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("TaskManager");
                    _instance = singleton.AddComponent<TaskManager>();
                }
            }
            return _instance;
        }
    }

    [Tooltip("A list of all tasks in-game")]
    public List<Task> taskList = new List<Task>();

    void Start()
    {
        Task[] tasks = FindObjectsOfType<Task>();
        taskList.AddRange(tasks);

        foreach (Task obj in taskList)
        {
            Debug.Log(obj.gameObject.name + " added to the list!");
        }
    }

    void Update()
    {

    }

    //Function which changes the task's state to FINISHED. It needs the task's ID
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
