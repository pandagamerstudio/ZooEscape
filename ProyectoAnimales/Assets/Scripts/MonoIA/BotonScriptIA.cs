using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotonScriptIA : MonoBehaviour
{
    public GameObject[] paredes;
    bool activada = false;
    float pulsar;
    private Animator animator;
    private SpawnManagerLevel1 nivel;

    int id;
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        activada = false;
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

    public void inicializarBoton(SpawnManagerLevel1 s, int i){
        nivel = s;
        id = i;
    }

    public void activarBotones(int id, bool aux){
        botonesActivados[id] = aux;
        foreach (bool b in botonesActivados){
            if (!b){
                activarParedes();
                return;
            }
        }
        desactivarParedes();   
    }

    public void desactivarParedes(){
        foreach(GameObject p in paredes){
            p.GetComponent<Animator>().SetBool("Pared", true);
        }
    }

    public void activarParedes(){
        foreach(GameObject p in paredes){
            p.GetComponent<Animator>().SetBool("Pared", false);
        }
    }
}
