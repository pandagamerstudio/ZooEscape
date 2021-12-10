using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonScript3d : MonoBehaviour
{
    public GameObject[] paredes;
    bool activada = false;
    float pulsar;
    public Animator animator;
    public GameObject suelo;
    public int idObjetivo;



    int cols = 0;


    public GameObject controladorBotones;
    int id;
    void Awake()
    {

    
        animator = this.GetComponent<Animator>();
        activada = false;
        if(suelo!=null)
        suelo.SetActive(false);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (controladorBotones != null) {
            if (collision.CompareTag("Player") || collision.CompareTag("Caja") || collision.CompareTag("cajaEspecial")) {
               
                if (collision.CompareTag("cajaEspecial"))
                {
                    Debug.Log("colision caja especial");
                    paredes[0].SetActive(true);
                    paredes[1].SetActive(true);
                    controladorBotones.GetComponent<controladorBotones>().sumEspecial();
                   StartCoroutine(cajaEspecial());
                }
                else {
                    cols++;
                    if (!activada) { 
                   

                        controladorBotones.GetComponent<controladorBotones>().sumar();
                        activada = true;
                        animator.SetBool("Activada", true);
                    }
                }
              
                }
        }

        else if (collision.CompareTag("Player") || collision.CompareTag("Caja") || collision.CompareTag("cajaEspecial"))
        {

            if (collision.CompareTag("cajaEspecial"))
            {
                suelo.SetActive(true);
                suelo.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(cajaEspecial2());

            }
            else {
                if (!activada)
                {
                    activada = true;
                    animator.SetBool("Activada", true);
                    suelo.SetActive(true);

                }
            }
                
        }
    }

    public void OnTriggerExit(Collider collision)
    {


        if (controladorBotones != null)
        {
            if ((collision.CompareTag("Player") || collision.CompareTag("Caja") ))
            {
                cols--;
                if (cols == 0) {
                    Debug.Log("salio");

                    controladorBotones.GetComponent<controladorBotones>().restar();
                    activada = false;

                    animator.SetBool("Activada", false);
                }
                

            }
        }

        else  if ((collision.CompareTag("Player") || collision.CompareTag("Caja")))
        {
            activada = false;
            animator.SetBool("Activada", false);
            suelo.SetActive(false);

        }
    }

    IEnumerator cajaEspecial() {
        yield return new WaitForSeconds(0.5f);
        paredes[0].SetActive(false);
                paredes[1].SetActive(false);
        controladorBotones.GetComponent<controladorBotones>().restar();
    }
    IEnumerator cajaEspecial2()
    {
        yield return new WaitForSeconds(0.09f);
        suelo.GetComponent<MeshRenderer>().enabled = true;

        suelo.SetActive(false);
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
