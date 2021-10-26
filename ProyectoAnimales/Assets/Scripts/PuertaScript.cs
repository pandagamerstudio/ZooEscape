using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PuertaScript : MonoBehaviourPun
{
    public int playersIn;

    void Awake(){
        playersIn = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!PhotonNetwork.IsMasterClient)
            return;

        if (collision.CompareTag("Player")){
            playersIn++;

            if (playersIn == 2 && collision.gameObject.GetComponent<PlayerController>().key){
                PhotonNetwork.LoadLevel("Menu");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(!PhotonNetwork.IsMasterClient)
            return;

        if (collision.CompareTag("Player")){
            playersIn--;
        }
    }
}
