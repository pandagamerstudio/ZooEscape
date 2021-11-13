using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class SpawnManagerLevel8 : SpawnManagerLevel1
{
    public Transform[] cajaSpawn;
    public GameObject caja;
    public Transform botonesSpawn;
    public GameObject boton;
    public Transform [] paredesSpawn;
    public GameObject paredes;
    GameObject[] paredesAux;
    public Transform palancaSpawn;
    public GameObject palanca;

    public bool botonesActivados;
    
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;


        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
        
        for(int i=0; i<cajaSpawn.Length; i++){
            PhotonNetwork.Instantiate(caja.name, cajaSpawn[i].position, Quaternion.identity);
        }

        paredesAux = new GameObject [paredesSpawn.Length];

        for (int i = 0; i < paredesSpawn.Length; i++){
            paredesAux[i] = PhotonNetwork.Instantiate(paredes.name, paredesSpawn[i].position, Quaternion.identity);
            if (i == 1)
                paredesAux[i].transform.rotation = Quaternion.Euler(0,0,90f);
        }
        
        botonesActivados = false;

        GameObject b = PhotonNetwork.Instantiate(boton.name, botonesSpawn.position, Quaternion.identity);  
        b.GetComponent<BotonScript>().inicializarBoton(this, 0);

        PhotonNetwork.Instantiate(palanca.name, palancaSpawn.position, Quaternion.identity);
        
    }

    public override void reiniciarNivel()
    {
        base.reiniciarNivel();
    }

    public override void activarBotones(int id, bool aux){
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        botonesActivados = aux;
        if (!botonesActivados){

            activarParedes();
            return;
        }
        
        desactivarParedes();
    }

    public void desactivarParedes(){
        foreach(GameObject p in paredesAux){
            //p.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            //p.GetComponent<BoxCollider2D>().enabled = false;
            //p.transform.position = new Vector3(500f,500f,0);
            p.GetComponent<Animator>().SetBool("Pared", true);
        }
    }

    public void activarParedes(){
        /*
        for (int i=0; i< paredesSpawn.Length;i++){
            paredesAux[i].transform.position = paredesSpawn[i].position;
        }
        */

        foreach(GameObject p in paredesAux){
            p.GetComponent<Animator>().SetBool("Pared", false);
        }
    }
}
