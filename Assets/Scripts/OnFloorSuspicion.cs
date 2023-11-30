using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFloorSuspicion : MonoBehaviour
{
    public bool blood, dirtySaw, suitcasePart, cleanSaw;

    public int suspicionLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            RaiseSuspicion();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("suspicion0")) suspicionLevel = 0;
        else if (other.CompareTag("suspicion1")) suspicionLevel = 1;
        else if (other.CompareTag("suspicion2")) suspicionLevel = 2;
        else if (other.CompareTag("suspicion3")) suspicionLevel = 3;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("suspicion0")) suspicionLevel = 0;
        else if (other.CompareTag("suspicion1")) suspicionLevel = 1;
        else if (other.CompareTag("suspicion2")) suspicionLevel = 2;
        else if (other.CompareTag("suspicion3")) suspicionLevel = 3;
    }

    public void RaiseSuspicion()
    {
        if (blood) PlayerStats.Instance.ModifySuspicionPerFrame(0.15f * suspicionLevel * PeriodVariation());
        else if (dirtySaw) PlayerStats.Instance.ModifySuspicionPerFrame(1.5f * suspicionLevel * PeriodVariation());
        else if (suitcasePart) PlayerStats.Instance.ModifySuspicionPerFrame(0.5f * suspicionLevel * PeriodVariation());
        else if(cleanSaw) PlayerStats.Instance.ModifySuspicionPerFrame(0.5f * (suspicionLevel - 1) * PeriodVariation());
    }

    public float PeriodVariation()
    {
        if ((TimeManager.Instance.hour >= 6 && TimeManager.Instance.hour < 18))
        {
            return 0.5f;
        }
        else return 1f;
    }
}
