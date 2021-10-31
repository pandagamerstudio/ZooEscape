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
    public Transform jug1;
    public Transform jug2;
    public GameObject gm;
    GameManager g;
  

    void Start() {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);
        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
        //g = gm.GetComponent<GameManager>();
      
    }

    public void reiniciarNivel() {
        GameObject l = GameObject.FindWithTag("Llave");
        if (l == null) {
            PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        }
      
      
    }

    
}
