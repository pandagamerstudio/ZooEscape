using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnObject : MonoBehaviourPun
{
    public Transform[] bananaSpawn;
    public GameObject banana;

    public Transform[] cajaSpawn;
    public GameObject caja;

    public Transform[] platformSpawn;
    public GameObject platform;

    public Transform[] palancaSpawn;
    public GameObject palanca;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient){
            for(int i=0; i < bananaSpawn.Length; i++){
                PhotonNetwork.Instantiate(banana.name, bananaSpawn[i].position, Quaternion.identity);
            }
            for (int i = 0; i < cajaSpawn.Length; i++)
            {
                PhotonNetwork.Instantiate(caja.name, cajaSpawn[i].position, Quaternion.identity);
            }

            for (int i = 0; i < platformSpawn.Length; i++)
            {
                PhotonNetwork.Instantiate(platform.name, platformSpawn[i].position, Quaternion.identity);
            }

            for (int i = 0; i < palancaSpawn.Length; i++)
            {
                PhotonNetwork.Instantiate(palanca.name, palancaSpawn[i].position, Quaternion.identity);
            }
        }
    }

}
