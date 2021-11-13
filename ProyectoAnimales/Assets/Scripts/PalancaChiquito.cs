using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PalancaChiquito : MonoBehaviourPun
{
    bool activada = false;
    float pulsar;
    private Animator animator;
    public GameObject[] players;
    bool playerIn, dentro;

     void Awake()
    {
        animator = this.GetComponent<Animator>();
        playerIn = false;
        dentro = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !dentro)
        {
            if (!playerIn){
                players = GameObject.FindGameObjectsWithTag("Player");
                playerIn = true;
            }
            dentro = true;
            if (activada) {
                if (players[0].GetComponent<PlayerController>().id == collision.gameObject.GetComponent<PlayerController>().id){
                    players[0].GetComponent<PlayerController>().changeScale(2);
                    players[1].GetComponent<PlayerController>().changeScale(1);
                }else{
                    players[1].GetComponent<PlayerController>().changeScale(2);
                    players[0].GetComponent<PlayerController>().changeScale(1);
                }
                
                animator.SetBool("Activada", false);
                StartCoroutine(changeActivada(false));
            }
            else{
                if (players[0].GetComponent<PlayerController>().id == collision.gameObject.GetComponent<PlayerController>().id){
                    players[0].GetComponent<PlayerController>().changeScale(1);
                    players[1].GetComponent<PlayerController>().changeScale(2);
                }else{
                    players[1].GetComponent<PlayerController>().changeScale(1);
                    players[0].GetComponent<PlayerController>().changeScale(2);
                }

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
    }

}
