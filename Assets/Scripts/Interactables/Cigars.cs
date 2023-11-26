using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Cigars : MonoBehaviour
{
    public int cigarsLeft = 4;
    public GameObject[] objectsToShow;
    public TextMeshProUGUI cigarsLeftText;
    public UnityEvent noMoreCigars;
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
            if (i == cigarsLeft)
            objectsToShow[i].SetActive(true);
            else objectsToShow[i].SetActive(false);
        }

        cigarsLeftText.text = "Fumar charuto\nQnt: " + cigarsLeft;

        if(cigarsLeft == 0 && !isFinished)
        {
            isFinished = true;
            noMoreCigars.Invoke();
        }
    }

    public void ReduceCigars()
    {
        cigarsLeft--;
    }
}
