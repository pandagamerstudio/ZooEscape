using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonScript3d : MonoBehaviour
{
    private GameObject[] paredes;
    bool activada = false;
    float pulsar;
    public Animator animator;
    public GameObject suelo;
    public int idObjetivo;

    int id;
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        activada = false;
        suelo.SetActive(false);
    }
    public void OnTriggerEnter(Collider collision)
    {


        if (collision.CompareTag("Player") || collision.CompareTag("Caja"))
        {
            if (!activada)
            {
                activada = true;
                animator.SetBool("Activada", true);
                suelo.SetActive(true);

            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {


        if ((collision.CompareTag("Player") || collision.CompareTag("Caja")))
        {
            activada = false;
            animator.SetBool("Activada", false);
            suelo.SetActive(false);

        }
    }

    public void OnTriggerStay(Collider collision)
    {

        if ((collision.CompareTag("Player") || collision.CompareTag("Caja")))
        {
            activada = true;
            animator.SetBool("Activada", true);
        }
    }

    public void setParedes(GameObject[] par)
    {
        paredes = par;
    }

    public void inicializarBoton(SpawnManagerLevel1 s, int i)
    {
        id = i;
    }


    public bool estaActivadoElSuelo()
    {
        return suelo.GetComponent<MeshRenderer>().enabled;
    }

    public void activarElementos()
    {


        suelo.SetActive(true);
        suelo.GetComponent<MeshRenderer>().enabled = false;

    }

    public void desactivarElementos()
    {

        suelo.SetActive(false);
        suelo.GetComponent<MeshRenderer>().enabled = true;
    }
}
