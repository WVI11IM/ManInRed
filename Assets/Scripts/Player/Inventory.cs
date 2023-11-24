using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class OnHandItem
{
    public string name;
    public int id;
    public GameObject onHandItemToActivate;
}

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

    public OnHandItem[] onHandItems;

    public bool ItemP = false, ItemG = false;

    private GameObject serraLimpa;

    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject itemToDrop = bigInventoryItem[0];
            itemToDrop.transform.position = gameObject.transform.position;
            Instantiate(bigInventoryItem[0], gameObject.transform.position, Quaternion.identity);
            bigInventoryItem[0].SetActive(true);
            bigInventoryItem.Remove(bigInventoryItem[0]);

            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }
        }
        else if (bigInventoryItem.Count > 1)
        {
            GameObject itemToDrop = bigInventoryItem[0];
            itemToDrop.transform.position = gameObject.transform.position;
            Instantiate(bigInventoryItem[0], gameObject.transform.position, Quaternion.identity);
            bigInventoryItem[0].SetActive(true);
            bigInventoryItem.Remove(bigInventoryItem[0]);
        }

        for (int i = 0; i < onHandItems.Length; i++)
        {
            onHandItems[i].onHandItemToActivate.SetActive(false);
        }

        if (bigInventoryItem.Count >= 1)
        {
            ItemData itemData = bigInventoryItem[0].GetComponent<ItemData>();

            for (int i = 0; i < onHandItems.Length; i++)
            {
                if (onHandItems[i].id == itemData.id)
                    onHandItems[i].onHandItemToActivate.SetActive(true);
            }
        }
    }

    public void AddItem(GameObject item)
    {
        ListItems(item);
        AudioManager.Instance.PlaySoundEffect("takeItem");
        playerAnimator.SetTrigger("interacted");
    }

    public void RemoveItem(GameObject item)
    {
        smallInventoryItems.Remove(item);
    }

    public void ListItems(GameObject item)
    {
        if (ItemP)
        {
            foreach (Transform child in itemContent)
            {
                Destroy(child.gameObject);
            }

            smallInventoryItems.Add(item);

            foreach (var ItemData in smallInventoryItems)
            {
                Image img = Instantiate(inventoryItem, itemContent);
                var itemIcon = img.transform.GetComponent<Image>();

                itemIcon.sprite = item.GetComponent<ItemData>().sprite;
            }
        }
        else if (ItemG)
        {
            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }

            bigInventoryItem.Add(item);
            Image img = Instantiate(bigInventory, inventoryItemBig);

            img.sprite = item.GetComponent<ItemData>().sprite;
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

        foreach (GameObject item in bigInventoryItem)
        {
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

    //Chamado nos eventos do gameobject da pia da cozinha, usado somente uma vez
    public void Serra()
    {
        if (HasItem(1))
        {
            Debug.Log("Serra agora está limpa");
            serraLimpa = Resources.Load<GameObject>("CleanSaw");
            Instantiate(serraLimpa, gameObject.transform.position, Quaternion.identity);
            bigInventoryItem.Remove(bigInventoryItem[0]);

            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }
        }
    }
}