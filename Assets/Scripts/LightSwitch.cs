using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject[] roomLight;
    public bool isOn = true;

    // Start is called before the first frame update
    void Start()
    {
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
            if (isOn) roomLight[i].SetActive(true);
            else roomLight[i].SetActive(false);
        }
    }

    public void Switch()
    {
        if (!isOn)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
    }

    public void ChangeState(bool isActive)
    {
        isOn = isActive;
    }
}
