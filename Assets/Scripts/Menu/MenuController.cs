using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
  [Header("Volume Setting")]
  [SerializeField] private TMP_Text volumeTextValue = null;
  [SerializeField] private Slider volumeSlider = null;
  [SerializeField] private float defaultVolume = 1.0f;

  [Header("Graphics Setting")]
  [SerializeField] private TMP_Text brightnessTextValue = null;
  [SerializeField] private Slider brightnessSlider = null;
  [SerializeField] private float defaultBrightness= 1;

  [Space(10)]
  [SerializeField] private TMP_Dropdown qualityDropdown;
  [SerializeField] private Toggle fullScreenToggle;

  private int _qualityLevel;
  private bool _isFullScreen;
  private float _brightnessLevel;

  [Header("Toggle Settings")]
  [SerializeField] private Toggle invertYToggle = null;

  [Header("Confirmation")]
  [SerializeField] private GameObject comfirmationPrompt = null;

  [Header("Levels To Load")]
  public string _newGameLevel;
  public string  levelToLoad;
  [SerializeField] private GameObject noSavedGameDialog = null;

  [Header("Resolution Dropdowns")]
  public TMP_Dropdown resolutionDropdown;
  private Resolution[] resolutions;



    private void Start()
    {
        // Define your list of supported resolutions.
        List<string> options = new List<string>
    {
        "1280x720",
        "1366x768",
        "1600x900",
        "1920x1080"
        // Add other supported resolutions as needed
    };

        // Get the current screen resolution.
        Resolution currentScreenResolution = Screen.currentResolution;
        string currentResolutionString = currentScreenResolution.width + "x" + currentScreenResolution.height;

        // Check if the current resolution matches any of the options.
        int bestResolutionIndex = options.IndexOf(currentResolutionString);

        if (bestResolutionIndex == -1)
        {
            // If the current resolution is not in the list, set a default best resolution.
            bestResolutionIndex = 0; // For example, select the first resolution in the list.
        }

        // Set the best resolution in the dropdown.
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = bestResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }



    public void NewGameDialogYes()
  {
     SceneManager.LoadScene(_newGameLevel);
     
  }

  public void LoadGameDialogYes()
  {
     if(PlayerPrefs.HasKey("SavedLevel"))
     {
        levelToLoad = PlayerPrefs.GetString("SavedLevel");
        SceneManager.LoadScene(levelToLoad);
     }
     else 
     {
      noSavedGameDialog.SetActive(true);
     }
  }

  public void ExitButton()
  {
    Application.Quit();
  }

  public void SetVolume(float volume)
  {
     AudioListener.volume = volume;
     volumeTextValue.text = volume.ToString("0.0");
  }

  public void VolumeApply()
  {
    PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    StartCoroutine(ConfirmationBox());
  }
   
  public void SetBrightness(float brightness)
  {
    _brightnessLevel = brightness;
    brightnessTextValue.text = brightness.ToString("0.0");
  }

  public void SetFullScreen(bool isFullScreen)
  {
    _isFullScreen = isFullScreen;
  }
  
  public void SetQuality(int qualityIndex)
  {
     _qualityLevel = qualityIndex;
  }

  public void GraphicsApply()
  {
     PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

     PlayerPrefs.SetFloat("masterQuality", _qualityLevel);
     QualitySettings.SetQualityLevel(_qualityLevel);

     PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
     Screen.fullScreen = _isFullScreen;

     StartCoroutine(ConfirmationBox());
  }

  public void ResetButton(string MenuType)
  {
     
     if(MenuType == "Graphics")
     {
       
       brightnessSlider.value = defaultBrightness;
       brightnessTextValue.text = defaultBrightness.ToString("0.0");

       qualityDropdown.value = 1;
       QualitySettings.SetQualityLevel(1);

       fullScreenToggle.isOn = false;
       Screen.fullScreen = false;

       Resolution currentResolution = Screen.currentResolution;
       Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
       resolutionDropdown.value = resolutions.Length;
       GraphicsApply();
     }

     if(MenuType == "Audio")
    {
        AudioListener.volume = defaultVolume;
        volumeSlider.value = defaultVolume;
        volumeTextValue.text = defaultVolume.ToString("0.0");
        VolumeApply();
    }
  }


  public IEnumerator ConfirmationBox()
  {
     comfirmationPrompt.SetActive(true);
     yield return new WaitForSeconds(2);
     comfirmationPrompt.SetActive(false);
  }
}

