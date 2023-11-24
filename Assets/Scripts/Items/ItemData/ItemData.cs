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
    private float gotejar = 8;  //De quanto em quanto tempo vai cair o sangue

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

        if (inventario.HasItem(5))
        {
            Debug.Log("Tem serra suja");
            SerraLimpa();
        }
    }

    void Jornal()
    {

    }

    void SerraSuja()
    {

    }

    void Maleta()
    {

    }

    void MaletaCorpo()
    {
        if (!TimeManager.Instance.timerIsPaused && !TimeManager.Instance.skippingTime)
        {
            contaGotas += 1 * Time.deltaTime;
        }

        if (contaGotas >= gotejar)
        {
            sangue = Resources.Load<GameObject>("Sangue" + Random.Range(1, 6));
            Debug.Log("Caiu sangue");
            Instantiate(sangue, inventario.transform.position, Quaternion.identity);

            contaGotas = 0;
        }
    }

    void MaletaCorpoJornal()
    {

    }

    void SerraLimpa()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}