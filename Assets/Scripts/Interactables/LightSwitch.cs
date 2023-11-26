using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject[] roomLight;
    public GameObject darkVolume;
    public bool isOn = true;

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LightUpdate();
    }

    void LightUpdate()
    {
        for (int i = 0; i < roomLight.Length; i++)
        {
            if (isOn)
            {
                roomLight[i].SetActive(true);
                darkVolume.SetActive(false);
            }
            else
            {
                roomLight[i].SetActive(false);
                darkVolume.SetActive(true);
            }
        }
    }

    public void Switch()
    {
        playerAnimator.SetTrigger("interacted");
        if (!isOn)
        {
            isOn = true;
            AudioManager.Instance.PlaySoundEffect("lightOn");
        }
        else
        {
            isOn = false;
            AudioManager.Instance.PlaySoundEffect("lightOff");
        }
    }

    public void ChangeState(bool isActive)
    {
        isOn = isActive;
    }
}
