using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("Inventory");
                    _instance = singleton.AddComponent<Inventory>();
                }
            }
            return _instance;
        }
    }

    public List<ItemData> inventoryItems = new List<ItemData>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemData item)
    {
        inventoryItems.Add(item);
    }

    public void RemoveItem(ItemData item)
    {
        inventoryItems.Remove(item);
    }

    public bool HasItem(int id)
    {
        // Iterate through the items in the inventory
        foreach (ItemData item in inventoryItems)
        {
            // Check if the item's itemID matches the specified itemID
            if (item.id == id)
            {
                return true; // The item was found in the inventory
            }
        }

        return false; // The item was not found in the inventory
    }
}
