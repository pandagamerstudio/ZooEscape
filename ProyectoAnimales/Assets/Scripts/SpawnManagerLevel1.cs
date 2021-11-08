using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManagerLevel1 : MonoBehaviourPun
{
    public Transform keySpawn;
    public GameObject key;
    public Transform puertaSpawn;
    public GameObject puerta;

    void Start() {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);
        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
//        PhotonNetwork.Instantiate(caja.name, keySpawn.position, Quaternion.identity);

        //g = gm.GetComponent<GameManager>();

    }

    public virtual void reiniciarNivel() {
        GameObject l = GameObject.FindWithTag("Llave");
        if (l == null) {
            PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        }
      
      
    }

    
}
