using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Animator taskList, clock, tutorialBox, meters, inventory;

    public bool taskListActive, clockActive, tutorialBoxActive, metersActive, inventoryActive = false;

    private void Update()
    {
        clock.SetBool("isActive", clockActive);
        taskList.SetBool("isActive", taskListActive);
        meters.SetBool("isActive", metersActive);
        inventory.SetBool("isActive", inventoryActive);
    }

    public void AllUI(bool isActive)
    {
        TaskList(isActive);
        Clock(isActive);
        Meters(isActive);
        Inventory(isActive);
    }

    public void TaskList(bool isActive)
    {
        taskListActive = isActive;
    }
    public void Clock(bool isActive)
    {
        clockActive = isActive;
    }
    public void Meters(bool isActive)
    {
        metersActive = isActive;
    }
    public void Inventory(bool isActive)
    {
        inventoryActive = isActive;
    }
}
