using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SpawnManagerLevel9 : SpawnManagerLevel1
{

    public Transform[] paredesSpawn;
    public GameObject paredes;

    public Transform[] paredesPeques;
    public GameObject paredPeque;

    public Transform[] cajas1Spawn;
    public Transform[] cajas2Spawn;
    public GameObject sueloBox;


    public Transform cajasPequenaSpwan;
    public GameObject cajasPeque;

    GameObject ultimacaja;
    bool unavez = true;

    public Sprite cajaJug1;
    public Sprite cajaJug2;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

     //   PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

       

        for (int i = 0; i < paredesSpawn.Length; i++) {
            PhotonNetwork.Instantiate(paredes.name, paredesSpawn[i].position, Quaternion.identity);
        }

        for (int i = 0; i < paredesPeques.Length; i++) {
            PhotonNetwork.Instantiate(paredPeque.name, paredesPeques[i].position, Quaternion.identity);
        }

        for (int i = 0; i < cajas1Spawn.Length; i++)
        {
           
            GameObject c1 = PhotonNetwork.Instantiate(sueloBox.name, cajas1Spawn[i].position, Quaternion.identity);

            
            photonView.RPC("setCaja1", RpcTarget.All, c1.GetComponent<PhotonView>().ViewID);
        }

        for (int i = 0; i < cajas2Spawn.Length; i++)
        {
            ultimacaja = PhotonNetwork.Instantiate(sueloBox.name, cajas2Spawn[i].position, Quaternion.identity);
            photonView.RPC("setCaja2", RpcTarget.All, ultimacaja.GetComponent<PhotonView>().ViewID);
        }


        GameObject caja = PhotonNetwork.Instantiate(cajasPeque.name, cajasPequenaSpwan.position, Quaternion.identity);

        photonView.RPC("cajasJugadores", RpcTarget.All, caja.GetComponent<PhotonView>().ViewID);

        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("pasoEscena", scene.name);

    }
    void Update()
    {

        if (PhotonNetwork.IsMasterClient && ultimacaja.transform.position.x <= -13 && unavez) {
            unavez = false;

            PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
            PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);
        }

    }
    [PunRPC]
    void setCaja1(int c)
    {
        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        go.layer = 8;
        go.GetComponent<SpriteRenderer>().sprite = cajaJug1;
    }

    [PunRPC]
    void setCaja2(int c)
    {
        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        go.layer = 9;
        go.GetComponent<SpriteRenderer>().sprite = cajaJug2;
    }

    [PunRPC]
    void cajasJugadores(int c) {

        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        foreach (Transform child in go.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(go.GetComponent<Rigidbody2D>());
        Destroy(go.GetComponent<BoxScript>());
        go.layer = 13;
    }

    [PunRPC]
    void cajasJugadores2(int c)
    {

        GameObject go = PhotonNetwork.GetPhotonView(c).gameObject;
        foreach (Transform child in go.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(go.GetComponent<Rigidbody2D>());
        Destroy(go.GetComponent<BoxScript>());
        go.layer = 13;
    }

}
