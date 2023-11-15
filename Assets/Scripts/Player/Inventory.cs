using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public List<GameObject> smallInventoryItems = new List<GameObject>();
    public Transform itemContent;   //gameobject/transform da posição que o slot vai fazer instantiate
    public Image inventoryItem; //prefab do slot

    public List<GameObject> bigInventoryItem = new List<GameObject>();
    public Transform inventoryItemBig;
    public Image bigInventory;

    public bool ItemP = false, ItemG = false;

    private Interactable interagivel;

    // Start is called before the first frame update
    void Start()
    {
        interagivel = FindObjectOfType<Interactable>();
    }

    //Teste para ver se remove da lista e do inventário da UI... /funciona
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) || bigInventoryItem.Count > 1)
        {
            bigInventoryItem[0].transform.position = gameObject.transform.position;
            bigInventoryItem[0].SetActive(true);
            bigInventoryItem.Remove(bigInventoryItem[0]);
        }
    }

    public void AddItem(GameObject item)
    {
        ListItems(item);
    }

    public void RemoveItem(GameObject item)
    {
        smallInventoryItems.Remove(item);
    }

    public void ListItems(GameObject item)
    {
        if (ItemP)
        {
            smallInventoryItems.Add(item);

            foreach (var ItemData in smallInventoryItems)
            {
                Image img = Instantiate(inventoryItem, itemContent);
                var itemIcon = img.transform.GetComponent<Image>();

                //itemIcon.sprite = item.GetComponent<ItemData>().sprite;
            }
        }
        else if (ItemG)
        {
            bigInventoryItem.Add(item);
            Image img = Instantiate(bigInventory, inventoryItemBig);

            //img.sprite = item.GetComponent<ItemData>().sprite;
        }       
    }

    public bool HasItem(int id)
    {
        // Iterate through the items in the inventory
        foreach (GameObject item in smallInventoryItems)
        {
            // Check if the item's itemID matches the specified itemID
            if (item.GetComponent<ItemData>().id == id)
            {
                return true; // The item was found in the inventory
            }
        }

        return false; // The item was not found in the inventory
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ItemPequeno"))
        {
            ItemP = true;
            ItemG = false;
        }
        else if (other.CompareTag("ItemGrande"))
        {           
            ItemP = false;
            ItemG = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ItemPequeno") || other.CompareTag("ItemGrande"))
        {
            ItemP = false;
            ItemG = false;
        }
    }
}