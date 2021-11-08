using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BotonScript : MonoBehaviourPun
{
    private GameObject[] paredes;
    bool activada = false;
    float pulsar;
    private Animator animator;
    private SpawnManagerLevel4 nivel;

    int id;
     void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") || collision.CompareTag("Caja"))
        {
            if (!activada) {
                activada = true;
                nivel.activarBotones(id, true);
                animator.SetBool("Activada" , true);
            }      
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if ((collision.CompareTag("Player") || collision.CompareTag("Caja"))){
            activada = false;
            nivel.activarBotones(id, false);
            animator.SetBool("Activada" , false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        if ((collision.CompareTag("Player") || collision.CompareTag("Caja"))){
            activada = true;
            nivel.activarBotones(id, true);
            animator.SetBool("Activada" , true);
        }
    }

    public void setParedes(GameObject[] par){
        paredes = par;
    }

    public void inicializarBoton(SpawnManagerLevel4 s, int i){
        nivel = s;
        id = i;
    }
}
