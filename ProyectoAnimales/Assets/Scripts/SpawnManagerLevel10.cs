using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManagerLevel10 : SpawnManagerLevel1
{
    public Transform[] cajaSpawn;
    public GameObject caja;
    public Transform [] botonesSpawn;
    public GameObject boton;
    public Transform paredesSpawn;
    public GameObject paredes;
    GameObject paredesAux;

    public bool[] botonesActivados;
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.Euler(0,0,180));

        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.Euler(0,0,180));
        
        for (int i=0; i < cajaSpawn.Length; i++){
            GameObject c = PhotonNetwork.Instantiate(caja.name, cajaSpawn[i].position, Quaternion.identity);
            if (i==1)
                c.GetComponent<Rigidbody2D>().gravityScale = -1;
        }

        paredesAux = PhotonNetwork.Instantiate(paredes.name, paredesSpawn.position, Quaternion.Euler(0,0,90f));
            
        botonesActivados = new bool[botonesSpawn.Length];

        for (int i = 0; i < botonesSpawn.Length; i++){
            GameObject b;
            if (i == 1)
                b = PhotonNetwork.Instantiate(boton.name, botonesSpawn[i].position, Quaternion.Euler(0,0,180));  
            else 
                b = PhotonNetwork.Instantiate(boton.name, botonesSpawn[i].position, Quaternion.identity);  
                
            b.GetComponent<BotonScript>().inicializarBoton(this, i);
          
        }
    }

    public override void reiniciarNivel()
    {
        base.reiniciarNivel();
    }

    public override void activarBotones(int id, bool aux){
        botonesActivados[id] = aux;
        foreach (bool b in botonesActivados){
            if (!b){
                activarParedes();
                return;
            }
        }
        desactivarParedes();   
    }

    public void desactivarParedes(){
        paredesAux.GetComponent<Animator>().SetBool("Pared", true);
    }

    public void activarParedes(){
        paredesAux.GetComponent<Animator>().SetBool("Pared", false);
    }

}
