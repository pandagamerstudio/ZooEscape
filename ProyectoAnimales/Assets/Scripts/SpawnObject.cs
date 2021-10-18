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

    public Transform[] colorBoxSpawn;
    public GameObject colorBox;

    public Transform[] cajas1Spawn;
    public Transform[] cajas2Spawn;
    public GameObject sueloBox;

    public Transform cuerdaSpawn;
    public GameObject cuerda;

    public Transform[] minasSpawn;
    public GameObject mina;

    public GameObject[] players;

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
                GameObject p = PhotonNetwork.Instantiate(platform.name, platformSpawn[i].position, Quaternion.identity);
                GameObject pa = PhotonNetwork.Instantiate(palanca.name, palancaSpawn[i].position, Quaternion.identity);
                photonView.RPC("instantiatePlat", RpcTarget.All, p.transform.GetChild(0).GetComponent<PhotonView>().ViewID, pa.GetComponent<PhotonView>().ViewID);
            }

            for (int i=0; i < colorBoxSpawn.Length; i++){
                GameObject b = PhotonNetwork.Instantiate(colorBox.name, colorBoxSpawn[i].position, Quaternion.identity);
                if (i%2 == 0){
                    photonView.RPC("setColorBoxId", RpcTarget.All, 1, b.GetComponent<PhotonView>().ViewID);
                }else{
                    photonView.RPC("setColorBoxId", RpcTarget.All, 2, b.GetComponent<PhotonView>().ViewID);
                }
            }

            for(int i = 0; i < cajas1Spawn.Length; i++){
                GameObject c1 = PhotonNetwork.Instantiate(sueloBox.name, cajas1Spawn[i].position, Quaternion.identity);
                photonView.RPC("setCaja1", RpcTarget.All, c1.GetComponent<PhotonView>().ViewID);
            }

            for(int i = 0; i < cajas2Spawn.Length; i++){
                GameObject c2 = PhotonNetwork.Instantiate(sueloBox.name, cajas2Spawn[i].position, Quaternion.identity);
                photonView.RPC("setCaja2", RpcTarget.All, c2.GetComponent<PhotonView>().ViewID);
            }

            GameObject rope = PhotonNetwork.Instantiate(cuerda.name, cuerdaSpawn.position, Quaternion.identity);

            for (int i = 0; i < minasSpawn.Length; i++)
            {
                GameObject m = PhotonNetwork.Instantiate(mina.name, minasSpawn[i].position, Quaternion.identity);
            }



        }
    }

    [PunRPC]
    public void instantiatePlat(int a, int b){
        
        GameObject goA = PhotonNetwork.GetPhotonView(a).gameObject;
        GameObject goB = PhotonNetwork.GetPhotonView(b).gameObject;
        goB.GetComponent<PlatMovManager>().setPlataforma(goA);
    }

    [PunRPC]
    public void setColorBoxId(int id, int g){
        GameObject goG = PhotonNetwork.GetPhotonView(g).gameObject;
        goG.GetComponent<boxColorScript>().setIdJug(id);
    }

    [PunRPC]
    void setCaja1(int c){
        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        go.layer = 8;
        go.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    [PunRPC]
    void setCaja2(int c){
        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        go.layer = 9;
        go.GetComponent<SpriteRenderer>().color = Color.green;
    }

    //public void createCuerda(){
    //    Vector3 pos = (GameManager.instance.spawnPoints[1].position - GameManager.instance.spawnPoints[0].position) / 2 + GameManager.instance.spawnPoints[0].position;
    //    GameObject rope = PhotonNetwork.Instantiate(cuerda.name, pos, Quaternion.identity);

    //    photonView.RPC("UnirCuerda", RpcTarget.All);
    //}

    //[PunRPC]
    //public void UnirCuerda(){
    //    players = GameObject.FindGameObjectsWithTag("Player");

    //    //cuerda.transform.GetChild(0).GetComponent<FixedJoint2D>().connectedBody = players[0].GetComponent<Rigidbody2D>();
    //    cuerda.GetComponent<FixedJoint2D>().connectedBody = players[0].GetComponent<PlayerController>().rb2D;

    //    //players[1].GetComponent<FixedJoint2D>().connectedBody = cuerda.transform.GetChild(cuerda.transform.childCount-1).GetComponent<Rigidbody2D>();
    //}

}
