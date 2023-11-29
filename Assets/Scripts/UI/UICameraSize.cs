using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraSize : MonoBehaviour
{
    Camera uiCamera;

    // Start is called before the first frame update
    void Start()
    {
        uiCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        uiCamera.orthographicSize = Camera.main.orthographicSize;
    }
}
