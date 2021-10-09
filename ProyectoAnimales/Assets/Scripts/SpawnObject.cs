using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnObject : MonoBehaviourPun
{
    public Transform[] bananaSpawn;
    public GameObject banana;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient){
            for(int i=0; i < bananaSpawn.Length; i++){
                PhotonNetwork.Instantiate(banana.name, bananaSpawn[i].position, Quaternion.identity);
            }
        }
    }

}
