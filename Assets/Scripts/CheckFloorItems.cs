using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFloorItems : MonoBehaviour
{
    public bool hasBlood = false;
    public bool hasDirtySaw = false;
    public bool hasSuitcasePart = false;
    public bool hasSuitcasePartNewspaper = false;
    public bool suspiciousItemOnFloor = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBlood || hasDirtySaw || hasSuitcasePart || hasSuitcasePartNewspaper)
        {
            suspiciousItemOnFloor = true;
        }
        else suspiciousItemOnFloor = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ItemGrande") || other.CompareTag("sangue"))
        {
            OnFloorSuspicion onFloorSuspicion = other.GetComponent<OnFloorSuspicion>();

            if (onFloorSuspicion != null && other.enabled == true)
            {
                // Reset all flags
                hasBlood = false;
                hasDirtySaw = false;
                hasSuitcasePart = false;
                hasSuitcasePartNewspaper = false;

                if (onFloorSuspicion.blood)
                {
                    hasBlood = true;
                }
                if (onFloorSuspicion.suitcasePart)
                {
                    hasSuitcasePart = true;
                }
                if (onFloorSuspicion.suitcasePartNewspaper)
                {
                    hasSuitcasePartNewspaper = true;
                }
                if (onFloorSuspicion.dirtySaw)
                {
                    hasDirtySaw = true;
                }

                // Check if there are any suspicious items on the floor
                suspiciousItemOnFloor = hasBlood || hasDirtySaw || hasSuitcasePart || hasSuitcasePartNewspaper;
            }
        }
    }
}
