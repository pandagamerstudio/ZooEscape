using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnObject : MonoBehaviourPun
{
    public Transform[] bananaSpawn;
    public GameObject banana;

    private void Awake()
    {
        photonView.RPC("spawnBanana", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void spawnBanana()
    {
        for (int i =0; i< bananaSpawn.Length;i++)
        {
            GameObject playerObject = PhotonNetwork.Instantiate(banana.name, bananaSpawn[i].position, Quaternion.identity);
        }
        
    }

}
