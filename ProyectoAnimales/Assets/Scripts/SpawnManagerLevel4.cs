using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManagerLevel4 : SpawnManagerLevel1
{
    public Transform cajaSpawn;
    public GameObject caja;
    public Transform [] botonesSpawn;
    public GameObject boton;
    public Transform [] paredesSpawn;
    public GameObject paredes;
    GameObject[] paredesAux;

    public bool[] botonesActivados;
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
        
        PhotonNetwork.Instantiate(caja.name, cajaSpawn.position, Quaternion.identity);

        paredesAux = new GameObject [paredesSpawn.Length];

        for (int i = 0; i < paredesSpawn.Length; i++){
            paredesAux[i] = PhotonNetwork.Instantiate(paredes.name, paredesSpawn[i].position, Quaternion.identity);
            if(i == paredesSpawn.Length - 1){
                paredesAux[i].transform.rotation = Quaternion.Euler(0,0,90f);
            }
        }
        
        botonesActivados = new bool[botonesSpawn.Length];

        for (int i = 0; i < botonesSpawn.Length; i++){
          GameObject b = PhotonNetwork.Instantiate(boton.name, botonesSpawn[i].position, Quaternion.identity);  
          b.GetComponent<BotonScript>().inicializarBoton(this, i);
        }
    }

    public override void reiniciarNivel()
    {
        base.reiniciarNivel();
    }

    public void activarBotones(int id, bool aux){
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
        foreach(GameObject p in paredesAux){
            p.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            p.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void activarParedes(){
        foreach(GameObject p in paredesAux){
            p.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            p.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
