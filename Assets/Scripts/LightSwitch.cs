using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light roomLight;
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
        if (isOn) roomLight.enabled = true;
        else roomLight.enabled = false;
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
