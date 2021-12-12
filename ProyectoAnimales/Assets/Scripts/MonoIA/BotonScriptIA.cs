using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotonScriptIA : MonoBehaviour
{
    public GameObject[] paredes;
    bool activada = false;
    float pulsar;
    private Animator animator;

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        activada = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!activada) {
                activada = true;
                activarBotones(true);
                animator.SetBool("Activada" , true);
            }      
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            activada = false;
            activarBotones(false);
            animator.SetBool("Activada" , false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            activada = true;
            activarBotones(true);
            animator.SetBool("Activada" , true);
        }
    }

    public void setParedes(GameObject[] par){
        paredes = par;
    }

    public void activarBotones(bool aux){

        if (aux)
        {
            activarParedes();
            return;
        }
        desactivarParedes();   
    }

    public void desactivarParedes(){
        foreach(GameObject p in paredes){
            p.SetActive(false);
        }
    }

    public void activarParedes(){
        foreach(GameObject p in paredes){
            p.SetActive(true);
        }
    }
}
