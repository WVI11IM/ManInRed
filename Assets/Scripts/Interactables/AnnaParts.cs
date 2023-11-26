using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class AnnaParts : MonoBehaviour
{
    public int partsLeft = 4;
    public GameObject[] objectsToShow;
    public TextMeshProUGUI partsLeftText;
    public UnityEvent gotAllParts;
    bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objectsToShow.Length; i++)
        {
            if (i == partsLeft)
            objectsToShow[i].SetActive(true);
            else objectsToShow[i].SetActive(false);
        }

        partsLeftText.text = "Partes restando: " + partsLeft;

        if(partsLeft == 0 && !isFinished)
        {
            isFinished = true;
            gotAllParts.Invoke();
        }
    }

    public void ReduceParts()
    {
        partsLeft--;
    }
}
