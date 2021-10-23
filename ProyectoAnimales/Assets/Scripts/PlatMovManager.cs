using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatMovManager : MonoBehaviour
{
    private GameObject plataforma;
    private PlatformMovement platMov;
    bool activada = false;
    float pulsar;
    private Animator animator;
    public string tipo;

     void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") || collision.CompareTag("Objects"))
        {
            if (activada) {
                plataforma.GetComponentInChildren<PlatformMovement>().activate = false;
                animator.SetBool("Activada", false);
                activada = false;
            }
            else{
                plataforma.GetComponentInChildren<PlatformMovement>().activate = true;
                animator.SetBool("Activada", true);
                activada = true;

            }
           
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if ((collision.CompareTag("Player") || collision.CompareTag("Objects")) && tipo == "Boton"){
            plataforma.GetComponentInChildren<PlatformMovement>().activate = false;
            animator.SetBool("Activada", false);
            activada = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        if ((collision.CompareTag("Player") || collision.CompareTag("Objects")) && tipo == "Boton"){
            plataforma.GetComponentInChildren<PlatformMovement>().activate = true;
            animator.SetBool("Activada", true);
            activada = true;
        }
    }

    public void setPlataforma(GameObject plat){
        plataforma = plat;
    }

}
