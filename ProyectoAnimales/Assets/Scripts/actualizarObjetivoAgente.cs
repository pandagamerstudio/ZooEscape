using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actualizarObjetivoAgente : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject agente;
    bool unaVez;
   public int objetivo;


    void Start()
    {
        unaVez = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {


        if ((collision.gameObject.tag.Equals("Player")|| collision.CompareTag("Caja")) &&unaVez)
        {
          
            agente.GetComponent<movimiento>().actualizarObjetivo(this.gameObject);
            unaVez = false;
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player") && (this.name.Equals("Boton")|| this.name.Equals("Boton2")))
        {
            agente.GetComponent<movimiento>().desActualizarObjetivo(objetivo);
            unaVez = true;
        }
    }

}
