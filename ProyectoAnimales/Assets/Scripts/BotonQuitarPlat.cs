using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BotonQuitarPlat : MonoBehaviourPun
{
    private GameObject[] plataformas;
    bool activada = false;
    float pulsar;
    private Animator animator;
    private SpawnManagerLevel5 nivel;

    int id;

     void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (nivel == null) return;
        if (collision.CompareTag("Player") || collision.CompareTag("Caja"))
        {
            if (!activada) {
                activada = true;
                nivel.activarPlat();
                animator.SetBool("Activada" , true);
            }      
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if (nivel == null) return;
        if ((collision.CompareTag("Player") || collision.CompareTag("Caja"))){
            activada = false;
            nivel.desactivarPlat();
            animator.SetBool("Activada" , false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        if (nivel == null) return;
        if ((collision.CompareTag("Player") || collision.CompareTag("Caja"))){
            activada = true;
            nivel.activarPlat();
            animator.SetBool("Activada" , true);
        }
    }

    public void setParedes(GameObject[] plat){
        plataformas = plat;
    }

    public void inicializarBoton(SpawnManagerLevel5 s){
        nivel = s;
    }

}
