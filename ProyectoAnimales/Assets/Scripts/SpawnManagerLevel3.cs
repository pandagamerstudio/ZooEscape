using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManagerLevel3 : SpawnManagerLevel1
{
    public Transform[] cajasSpawn;
    public GameObject cajas;

    void Start(){
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
        
        for (int i = 0; i < cajasSpawn.Length; i++)
        {
            PhotonNetwork.Instantiate(cajas.name, cajasSpawn[i].position, Quaternion.identity);
        }
    }
    public override void reiniciarNivel()
    {
        GameObject l = GameObject.FindWithTag("Llave");
        if (l == null)
        {
            PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        }
        GameObject[] cajas = GameObject.FindGameObjectsWithTag("Caja");
        for (int i = 0; i < cajas.Length; i++) {
            Destroy(cajas[i].GetComponent<FixedJoint2D>());
            cajas[i].transform.position = cajasSpawn[i].position;
        }


    }
}
