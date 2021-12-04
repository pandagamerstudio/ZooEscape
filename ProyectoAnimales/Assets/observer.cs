using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject agente;
    public int mievento;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("hijoPolar"))
        {
            agente.GetComponent<movimiento>().actualizarObjetivo(this.gameObject);
        }
    }

    public void escuchando(int evento)
    {
        if (mievento == evento)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);

        }
    }
}
