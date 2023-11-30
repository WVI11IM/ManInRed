using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public int id;
    public string displayName;
    public Sprite sprite;

    private Inventory inventario;

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
    6 Balde
    7 Corda
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
            PlayerStats.Instance.ModifyPressurePerFrame(0.25f);
        }
    }

    void Maleta()
    {

    }

    void MaletaCorpoJornal()
    {
        if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            PlayerStats.Instance.ModifyPressurePerFrame(0.5f);
        }
    }
}