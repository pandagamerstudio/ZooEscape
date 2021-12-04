using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actualizarObjetivoAgente : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject agente;
    bool unaVez;
    void Start()
    {
        unaVez = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")&&unaVez)
        {
            agente.GetComponent<movimiento>().actualizarObjetivo();
            unaVez = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && this.name.Equals("Boton"))
        {
            agente.GetComponent<movimiento>().desActualizarObjetivo();
            unaVez = true;
        }
    }

}
