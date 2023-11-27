using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;


public class ActivateWithClothing : MonoBehaviour
{
    PlayerStats playerStats;
    public GameObject[] forClean;
    public GameObject[] forDirty;

    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < forClean.Length; i++)
        {
            forDirty[i].SetActive(playerStats.isDirty);
            forClean[i].SetActive(!playerStats.isDirty);
        }
    }
}
