using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;


public class ActivateWithConditions : MonoBehaviour
{
    PlayerStats playerStats;
    Inventory inventory;
    Interactable interactable;

    [Header("Should the interactable activate or deactivate if conditions are met?")]
    public bool activateOrDeactivateInteractable = true;

    public int[] requiredItemIds;
    public int[] requiredProgressIds;
    /*
    [Header("Progress requirements")]
    public bool cortouCorpo, escondeuEmLixo, escondeuEmGeladeira, escondeuEmArmario, escondeuEmCanteiro, escondeuTudo;
    */

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
        playerStats = PlayerStats.Instance;
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        ConditionsMeet();
    }

    public void ConditionsMeet()
    {
        if (CheckItems() && CheckProgress())
        {
            interactable.enabled = activateOrDeactivateInteractable;
        }
        else
        {
            interactable.enabled = !activateOrDeactivateInteractable;
        }
        Debug.Log("Interactable of " + gameObject.name + " is now " + interactable.enabled);
    }

    public bool CheckItems()
    {
        if (requiredItemIds.Length > 0)
        {
            for (int i = 0; i < requiredItemIds.Length; i++)
            {
                if (requiredItemIds[i] == 3 || requiredItemIds[i] == 4)
                {
                    if (!inventory.HasItem(3) && !inventory.HasItem(4))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!inventory.HasItem(requiredItemIds[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        else
        {
            return true;
        }

    }

    public bool CheckProgress()
    {
        if (requiredProgressIds.Length > 0)
        {
            for (int i = 0; i < requiredProgressIds.Length; i++)
            {
                if (!playerStats.CheckProgressId(requiredProgressIds[i]))
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return true;
        }
    }
}
