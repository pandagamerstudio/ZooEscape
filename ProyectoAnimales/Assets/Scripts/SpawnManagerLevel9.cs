using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManagerLevel9 : SpawnManagerLevel1
{

    public Transform[] paredesSpawn;
    public GameObject paredes;

    public Transform[] paredesPeques;
    public GameObject paredPeque;

    public Transform[] cajas1Spawn;
    public Transform[] cajas2Spawn;
    public GameObject sueloBox;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        for (int i = 0; i < paredesSpawn.Length; i++) {
            PhotonNetwork.Instantiate(paredes.name, paredesSpawn[i].position, Quaternion.identity);
        }

        for (int i = 0; i < paredesPeques.Length; i++) {
            PhotonNetwork.Instantiate(paredPeque.name, paredesPeques[i].position, Quaternion.identity);
        }

        for (int i = 0; i < cajas1Spawn.Length; i++)
        {
           
            GameObject c1 = PhotonNetwork.Instantiate(sueloBox.name, cajas1Spawn[i].position, Quaternion.identity);

            Debug.Log(c1.GetComponent<PhotonView>().ViewID);
            photonView.RPC("setCaja1", RpcTarget.All, c1.GetComponent<PhotonView>().ViewID);
        }

        for (int i = 0; i < cajas2Spawn.Length; i++)
        {
            GameObject c2 = PhotonNetwork.Instantiate(sueloBox.name, cajas2Spawn[i].position, Quaternion.identity);
            photonView.RPC("setCaja2", RpcTarget.All, c2.GetComponent<PhotonView>().ViewID);
        }

    }
    [PunRPC]
    void setCaja1(int c)
    {
        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        go.layer = 8;
        go.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    [PunRPC]
    void setCaja2(int c)
    {
        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        go.layer = 9;
        go.GetComponent<SpriteRenderer>().color = Color.green;
    }

}
