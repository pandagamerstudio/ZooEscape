using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviourPun
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!PhotonNetwork.IsMasterClient)
            return;

        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            photonView.RPC("RecogerObjeto", RpcTarget.All);

            PhotonNetwork.Destroy(gameObject);
        }

    }

    [PunRPC]
    void RecogerObjeto()
    {
        //GetComponent<SpriteRenderer>().enabled = false;
    }
}
