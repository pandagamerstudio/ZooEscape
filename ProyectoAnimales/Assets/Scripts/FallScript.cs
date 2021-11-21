using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FallScript : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(!PhotonNetwork.IsMasterClient)
            return;

        if (collision.CompareTag("Player")){
            collision.gameObject.transform.GetChild(3).GetComponent<PauseScript>().OnResetLevel();
        }
    }
}
