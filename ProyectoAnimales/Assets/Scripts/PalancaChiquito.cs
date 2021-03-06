using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PalancaChiquito : MonoBehaviourPun
{
  public  bool activada = false;
    float pulsar;
    private Animator animator;
    public GameObject[] players;
    public bool playerIn, dentro;
    Collider2D collider;
    [SerializeField]
   public static bool unaVez;


    GameObject miPersonaje,personajeDelOtro;

     void Awake()
    {
        animator = this.GetComponent<Animator>();
        playerIn = false;
        dentro = false;
        collider = GetComponent<BoxCollider2D>();
        unaVez = false;
    }

    private void Start()
    {
        StartCoroutine(seleccionarPersonajes());



    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !dentro)
        {

            dentro = true;
            collider.enabled = false;
            if (activada) {

                cambiarTamano(collision.gameObject);
              /*  if (players[0].GetComponent<PlayerController>().id == collision.gameObject.GetComponent<PlayerController>().id){
                    players[0].GetComponent<PlayerController>().changeScale(2);
                    players[1].GetComponent<PlayerController>().changeScale(1);
                }else{
                    players[1].GetComponent<PlayerController>().changeScale(2);
                    players[0].GetComponent<PlayerController>().changeScale(1);
                }*/
                
                animator.SetBool("Activada", false);
                StartCoroutine(changeActivada(false));
            }
            else{
                
                /*if (players[0].GetComponent<PlayerController>().id == collision.gameObject.GetComponent<PlayerController>().id){
                    players[0].GetComponent<PlayerController>().changeScale(1);
                    players[1].GetComponent<PlayerController>().changeScale(2);
                }else{
                    players[1].GetComponent<PlayerController>().changeScale(1);
                    players[0].GetComponent<PlayerController>().changeScale(2);
                }*/
                //Caso que ambos jugadores tengan la misma escala(primera colisi???n), 
 
              
                    cambiarTamano(collision.gameObject);
           

                animator.SetBool("Activada", true);
                StartCoroutine(changeActivada(true));

            }
           
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player"))
            dentro = false;
    }

    IEnumerator changeActivada(bool b){
        yield return new WaitForSeconds(0.5f);
        activada = b;
        dentro = true;
        collider.enabled = true;
    }

    IEnumerator seleccionarPersonajes()
    {
        yield return new WaitForSeconds(1.0f);
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players[0].GetComponent<PhotonView>().IsMine)
        {
            miPersonaje = players[0];
            personajeDelOtro = players[1];
        }
        else
        {
            miPersonaje = players[1];
            personajeDelOtro = players[0];
        }
    }

    public void cambiarTamano(GameObject collision) {

        Debug.Log("Tama???o cambiado, mi tamalo acutal "+ (int)Mathf.Abs(miPersonaje.transform.localScale.x));

        if ((int)Mathf.Abs(miPersonaje.transform.localScale.x) == (int)Mathf.Abs(personajeDelOtro.transform.localScale.x))
        {
           
            if (miPersonaje.GetComponent<PhotonView>().ViewID != collision.GetComponent<PhotonView>().ViewID)
            {
                if ((int)Mathf.Abs(miPersonaje.transform.localScale.x) == 1)
                {
                    miPersonaje.GetComponent<PlayerController>().changeScale(2);

                }
                else
                {
                    miPersonaje.GetComponent<PlayerController>().changeScale(1);


                }
            }

        }
        else {
            if ((int)Mathf.Abs(miPersonaje.transform.localScale.x) == 1)
            {
                miPersonaje.GetComponent<PlayerController>().changeScale(2);

            }
            else
            {
                miPersonaje.GetComponent<PlayerController>().changeScale(1);


            }
        }


       
        Debug.Log("Mi tama???o cambiado " + (int)Mathf.Abs(miPersonaje.transform.localScale.x));
    }

}
