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

    //public bool ItemP = false, ItemG = false;

    //Variaveis para o sanguue
    private GameObject sangue;
    private float contaGotas;   //Contador
    private float gotejar = 7;  //De quanto em quanto tempo vai cair o sangue

    //private GameObject serraLimpa;
    public GameObject[] allItems;

    Animator playerAnimator;

    private bool podeLimpar;
    private GameObject paraLimpar;

    public int suspicionLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            GameObject itemToDrop = bigInventoryItem[0];
            itemToDrop.transform.position = gameObject.transform.position;
            Instantiate(bigInventoryItem[0], gameObject.transform.position, Quaternion.identity);
            bigInventoryItem[0].SetActive(true);
            bigInventoryItem.Remove(bigInventoryItem[0]);

            AudioManager.Instance.PlaySoundEffect("dropItem");

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

            AudioManager.Instance.PlaySoundEffect("dropItem");
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

        LimparSangue();
        MaletaCorpo();

        if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            RaiseSuspicion();
            Debug.Log("Current suspicionLevel: " + suspicionLevel);
        }
    }

    public void AddItem(GameObject item)
    {
        ListItems(item);
        AudioManager.Instance.PlaySoundEffect("takeItem");
        playerAnimator.SetTrigger("interacted");
        Debug.Log("Added item " + item);
    }

    public void RemoveItem(GameObject item)
    {
        ItemData itemData = item.GetComponent<ItemData>();
        int itemId = itemData.id;

        if (itemId == 0 || itemId == 7)
        {
            foreach (Transform child in itemContent)
            {
                Destroy(child.gameObject);
            }

            smallInventoryItems.Remove(item);

            foreach (var ItemData in smallInventoryItems)
            {
                Image img = Instantiate(inventoryItem, itemContent);
                var itemIcon = img.transform.GetComponent<Image>();
                var itemImage = ItemData.GetComponent<ItemData>().sprite;
                itemIcon.sprite = itemImage;
            }
        }
        else
        {
            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }

            bigInventoryItem.Remove(item);
            Image img = Instantiate(bigInventory, inventoryItemBig);

            img.sprite = item.GetComponent<ItemData>().sprite;
        }
    }

    public void ListItems(GameObject item)
    {
        ItemData itemData = item.GetComponent<ItemData>();
        int itemId = itemData.id;

        if(itemId == 0 || itemId == 7)
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
                var itemImage = ItemData.GetComponent<ItemData>().sprite;
                itemIcon.sprite = itemImage;
            }
        }

        else
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("suspicion0")) suspicionLevel = 0;
        else if (other.CompareTag("suspicion1")) suspicionLevel = 1;
        else if (other.CompareTag("suspicion2")) suspicionLevel = 2;
        else if (other.CompareTag("suspicion3")) suspicionLevel = 3;
    }

    private void OnTriggerStay(Collider other)
    {
        /*
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
        */

        if (other.CompareTag("sangue"))
        {
            podeLimpar = true;
            paraLimpar = other.gameObject;
        }

        if (other.CompareTag("suspicion0")) suspicionLevel = 0;
        else if (other.CompareTag("suspicion1")) suspicionLevel = 1;
        else if (other.CompareTag("suspicion2")) suspicionLevel = 2;
        else if (other.CompareTag("suspicion3")) suspicionLevel = 3;
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.CompareTag("ItemPequeno") || other.CompareTag("ItemGrande"))
        {
            ItemP = false;
            ItemG = false;
        }
        */

        if (other.CompareTag("sangue"))
        {
            podeLimpar = false;
            paraLimpar = null;
        }
    }

    public void RaiseSuspicion()
    {
        //Segurando serra ensanguentada
        if (HasItem(1))
        {
            PlayerStats.Instance.ModifySuspicionPerFrame(1f * suspicionLevel * PeriodVariation());
        }
        //Segurando maleta com sangue
        else if (HasItem(3))
        {
            PlayerStats.Instance.ModifySuspicionPerFrame(0.3f * suspicionLevel * PeriodVariation());
        }
        //Segurando serra limpa
        else if (HasItem(5))
        {
            PlayerStats.Instance.ModifySuspicionPerFrame(0.3f * suspicionLevel * PeriodVariation());
        }

        //Roupa com sangue
        if (PlayerStats.Instance.isDirty)
        {
            PlayerStats.Instance.ModifySuspicionPerFrame(0.5f * suspicionLevel * PeriodVariation());
        }
    }

    public float PeriodVariation()
    {
        if ((TimeManager.Instance.hour >= 6 && TimeManager.Instance.hour < 18))
        {
            return 0.5f;
        }
        else return 1f;
    }

    public void WindowIcons()
    {

    }

    public void LimparSerra()
    {
        if (HasItem(1))
        {
            bigInventoryItem.Remove(bigInventoryItem[0]);
            playerAnimator.SetTrigger("interacted");
            AudioManager.Instance.PlaySoundEffect("sink");
            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }
            AddItem(allItems[5]);

        }
    }

    public void GuardarParteMaleta()
    {
        if (HasItem(2) && HasItem(0))
        {
            bigInventoryItem.Remove(bigInventoryItem[0]);
            playerAnimator.SetTrigger("interacted");

            AudioManager.Instance.PlaySoundEffect("hideInSuitcase");
            AudioManager.Instance.PlaySoundEffect("wrapNewspaper");
            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }
            AddItem(allItems[4]);
            RemoveItem(allItems[0]);
        }
        else if (HasItem(2))
        {
            bigInventoryItem.Remove(bigInventoryItem[0]);
            playerAnimator.SetTrigger("interacted");
            AudioManager.Instance.PlaySoundEffect("hideInSuitcase");
            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }
            AddItem(allItems[3]);
            PlayerStats.Instance.isDirty = true;
        }
    }

    public void EsconderEmMovel()
    {
        if (HasItem(3))
        {
            bigInventoryItem.Remove(bigInventoryItem[0]);
            playerAnimator.SetTrigger("interacted");
            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }
            AddItem(allItems[2]);
        }
        else if (HasItem(4))
        {
            bigInventoryItem.Remove(bigInventoryItem[0]);
            playerAnimator.SetTrigger("interacted");
            foreach (Transform child in inventoryItemBig)
            {
                Destroy(child.gameObject);
            }
            AddItem(allItems[2]);
        }
    }

    public void AmarrarCorda()
    {
        if (HasItem(7))
        {
            playerAnimator.SetTrigger("interacted");
            RemoveItem(allItems[7]);
        }
    }

    void LimparSangue()
    {
        if (HasItem(6))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (podeLimpar)
                {
                    playerAnimator.SetTrigger("interacted");
                    Destroy(paraLimpar);
                    AudioManager.Instance.PlaySoundEffect("cleaningBlood");
                    podeLimpar = false;
                }
            }
        }
    }

    void MaletaCorpo()
    {
        if (HasItem(3))
        {
            if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
            {
                contaGotas += Time.deltaTime;
            }

            if (contaGotas >= gotejar)
            {
                sangue = Resources.Load<GameObject>("Sangue" + Random.Range(1, 6));
                Debug.Log("Caiu sangue");
                Instantiate(sangue, gameObject.transform.position, Quaternion.identity);

                contaGotas = 0;
            }

            if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
            {
                PlayerStats.Instance.ModifyPressurePerFrame(0.5f);
            }
        }
        
    }

}