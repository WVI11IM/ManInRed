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
    private float gotejar = 5;  //De quanto em quanto tempo vai cair o sangue

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
        if (inventario.HasItem(1))
        {
            Debug.Log("Tem serra suja");
            Serra();
        }

        if (inventario.HasItem(2))
        {
            Debug.Log("Tem Maleta");
            Maleta();
        }
    }

    void Maleta()
    {
        contaGotas += 1 * Time.deltaTime;
        if (contaGotas >= gotejar)
        {
            Debug.Log("Caiu sangue");
            sangue = Resources.Load<GameObject>("Sangue");
            Instantiate(sangue, inventario.transform.position, Quaternion.identity);

            contaGotas = 0;
        }
    }

    void Serra()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}