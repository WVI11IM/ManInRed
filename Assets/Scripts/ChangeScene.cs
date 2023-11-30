using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MainEnding()
    {
        if(PlayerStats.Instance.mainSuspicion < 50)
        {
            SceneManager.LoadScene("Ending2");
        }
        else SceneManager.LoadScene("Ending3");
    }
}
