using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public UIController uiController;
    public bool wasCalled = false;
    [TextArea(3, 5)]
    public string tutorialText;

    public void CallTutorial()
    {
        if (!wasCalled)
        {
            uiController.TutorialBoxText(tutorialText);
            wasCalled = true;
        }
    }
}
