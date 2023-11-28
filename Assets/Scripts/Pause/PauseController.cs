using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseController : MonoBehaviour
{
  [Header("Volume Setting EF")]
  [SerializeField] private TMP_Text volumeTextValue = null;
  [SerializeField] private Slider volumeSlider = null;
  [SerializeField] private float defaultVolume = 0.5f;

  [Header("Volume Setting AM")]
  [SerializeField] private TMP_Text volumeTextValueAM = null;
  [SerializeField] private Slider volumeSliderAM = null;
  [SerializeField] private float defaultVolumeAM = 0.5f;

  [Header("Confirmation")]
  [SerializeField] private GameObject comfirmationPrompt = null;

  public void SetVolume1(float volume)
  {
   // AudioListener.volume = volume;
    volumeTextValue.text = volume.ToString("0.0");
    AudioManager.Instance.soundEffectVolume = volume;
    
  }

   public void SetVolume2(float volume)
  {
   // AudioListener.volume = volume;
    volumeTextValueAM.text = volume.ToString("0.0");
    AudioManager.Instance.ambientVolume = volume;
    
  }

  public void VolumeApply()
  {
    PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    StartCoroutine(ConfirmationBox());
  }


  public void ResetButton(string MenuType)
  {

    if (MenuType == "Audio")
    {
      AudioListener.volume = defaultVolume;
      volumeSlider.value = defaultVolume;
      volumeTextValue.text = defaultVolume.ToString("0.0");

      volumeSliderAM.value = defaultVolumeAM;
      SetVolume2(defaultVolumeAM);


      VolumeApply();
    }
  }

  public IEnumerator ConfirmationBox()
  {
    comfirmationPrompt.SetActive(true);
    yield return new WaitForSecondsRealtime(1);
    comfirmationPrompt.SetActive(false);
  }
}
