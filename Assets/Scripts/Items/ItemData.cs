using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public int id;
    public string displayName;
    public Sprite sprite;

    private Inventory inventario;

    private GameObject sangue;
    private float contaGotas;   //Contador
    private float gotejar = 6;  //De quanto em quanto tempo vai cair o sangue
    private bool areBloodPuddlesInstantiated = false;

    private void Start()
    {
        inventario = Inventory.Instance;
    }

    /*
    Nome dos itens e IDs
    1 Serra Suja
    2 Maleta Vazia
    3 Maleta + Corpo
    4 Maleta + Jornal + Corpo
    5 Serra Limpa
    */

    private void Update()
    {
        if (inventario.HasItem(0))
        {
            Debug.Log("Tem jornal");
            Jornal();
        }

        if (inventario.HasItem(1))
        {
            Debug.Log("Tem serra suja");
            SerraSuja();
        }

        if (inventario.HasItem(2))
        {
            Debug.Log("Tem Maleta");
            Maleta();
        }

        if (inventario.HasItem(3))
        {
            Debug.Log("Tem Maleta com Parte");
            MaletaCorpo();
        }

        if (inventario.HasItem(4))
        {
            Debug.Log("Tem Maleta com Parte Embrulhada");
            MaletaCorpoJornal();
        }
    }

    void Jornal()
    {

    }

    void SerraSuja()
    {
        if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            PlayerStats.Instance.ModifyPressurePerFrame(0.2f);
        }
    }

    void Maleta()
    {

    }

    void MaletaCorpo()
    {
        if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            contaGotas += Time.deltaTime;
        }

        if (contaGotas >= gotejar)
        {
            // Instantiate two sangue objects only once
            if (!areBloodPuddlesInstantiated)
            {
                for (int i = 0; i < 2; i++)
                {
                    sangue = Resources.Load<GameObject>("Sangue" + Random.Range(1, 6));
                    Debug.Log("Caiu sangue");
                    Instantiate(sangue, inventario.transform.position, Quaternion.identity);
                }

                // Set the flag to prevent multiple instantiations in the same frame
                areBloodPuddlesInstantiated = true;
            }

            contaGotas = 0;
        }
        else
        {
            areBloodPuddlesInstantiated = false;
        }

        if(!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            PlayerStats.Instance.ModifyPressurePerFrame(0.2f);
        }
    }

    void MaletaCorpoJornal()
    {
        if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            PlayerStats.Instance.ModifyPressurePerFrame(0.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}