using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Beers : MonoBehaviour
{
    public int beersLeft = 3;
    public GameObject[] objectsToShow;
    public TextMeshProUGUI beersLeftText;
    public UnityEvent noMoreBeers;
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
            if (i == beersLeft)
            objectsToShow[i].SetActive(true);
            else objectsToShow[i].SetActive(false);
        }

        beersLeftText.text = "Beber cerveja\nQnt: " + beersLeft;

        if(beersLeft == 0 && !isFinished)
        {
            isFinished = true;
            noMoreBeers.Invoke();
        }
    }

    public void ReduceBeers()
    {
        beersLeft--;
    }
}
