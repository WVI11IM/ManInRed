using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEye : MonoBehaviour
{
    public GameObject redWindow;
    public GameObject eyeIconCanvas;
    public bool playerIsInside;
    public CheckFloorItems checkFloorItems;

    // Start is called before the first frame update
    void Start()
    {
        redWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        eyeIconCanvas.transform.rotation = Camera.main.transform.rotation;

        if (checkFloorItems.suspiciousItemOnFloor)
        {
            redWindow.SetActive(true);
        }
        else if (playerIsInside && Inventory.Instance.suspicionLevel != 0)
        {
            if (Inventory.Instance.HasItem(1) || Inventory.Instance.HasItem(3) || Inventory.Instance.HasItem(5) || PlayerStats.Instance.isDirty || checkFloorItems.suspiciousItemOnFloor)
            {
                redWindow.SetActive(true);
            }
            else redWindow.SetActive(false);
        }
        else redWindow.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        //suspiciousItemOnFloor = false;
        //playerIsInside = false;

        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
        }

        /*
        if (other.CompareTag("ItemGrande") || other.CompareTag("sangue"))
        {
            OnFloorSuspicion onFloorSuspicion = other.GetComponent<OnFloorSuspicion>();

            if (onFloorSuspicion != null && other.enabled == true)
            {
                if (!onFloorSuspicion.cleanSaw)
                {
                    suspiciousItemOnFloor = true;
                }
            }
            else suspiciousItemOnFloor = false;
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }
}
