using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BotonScriptLevel5 : MonoBehaviourPun, IOnEventCallback
{
    private GameObject[] paredes;
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
                RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                PhotonNetwork.RaiseEvent(3, null, raiseEvent, SendOptions.SendReliable);
               // photonView.RPC("botonPulsado", RpcTarget.All);
            }      
        }
    }
 
    public void botonPulsado() {
        nivel.activarBotones(id, true);
        animator.SetBool("Activada", true);
    }
    public void OnTriggerExit2D(Collider2D collision){
        if (nivel == null) return;
        if ((collision.CompareTag("Player") || collision.CompareTag("Caja"))){
            activada = false;
            nivel.activarBotones(id, false);
            animator.SetBool("Activada" , false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        if (nivel == null) return;
        if ((collision.CompareTag("Player") || collision.CompareTag("Caja"))){
            activada = true;
            nivel.activarBotones(id, true);
            animator.SetBool("Activada" , true);
        }
    }

    public void setParedes(GameObject[] par){
        paredes = par;
    }

    public void inicializarBoton(SpawnManagerLevel5 s, int i){
        nivel = s;
        id = i;
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;


        if (eventCode == 3)
        {

            botonPulsado();
        }
       
    }
}
