using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviourPun
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            photonView.RPC("RecogerObjeto", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RecogerObjeto()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
