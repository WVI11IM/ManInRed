using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public Animator taskList, clock, tutorialBox, meters, inventory, black;

    public bool taskListActive, clockActive, tutorialBoxActive, metersActive, inventoryActive, blackActive = false;

    public TextMeshProUGUI tutorialBoxText;

    private Coroutine tutorialBoxCoroutine;

    private void Update()
    {
        clock.SetBool("isActive", clockActive);
        taskList.SetBool("isActive", taskListActive);
        meters.SetBool("isActive", metersActive);
        inventory.SetBool("isActive", inventoryActive);
        tutorialBox.SetBool("isActive", tutorialBoxActive);
        black.SetBool("isActive", blackActive);
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
    public void Black(bool isActive)
    {
        blackActive = isActive;
    }

    void TutorialBox(bool isActive)
    {
        tutorialBoxActive = isActive;
    }

    public void TutorialBoxText(string text)
    {
        if (!tutorialBoxActive)
        {
            TutorialBox(true);
            tutorialBoxText.text = text;

            if (tutorialBoxCoroutine != null)
            {
                StopCoroutine(tutorialBoxCoroutine);
            }

            tutorialBoxCoroutine = StartCoroutine(TutorialBoxCountdown());
            AudioManager.Instance.PlaySoundEffect("tutorial");
        }
    }

    IEnumerator TutorialBoxCountdown()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(6);
            TutorialBox(false);
            yield return null;
        }
    }
}
