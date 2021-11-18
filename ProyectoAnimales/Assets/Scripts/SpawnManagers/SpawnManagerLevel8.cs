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
    public Transform [] botonesSpawn;
    public GameObject [] boton;
    public Transform [] paredesSpawn;
    public GameObject [] paredes;
    GameObject[] paredesAux;
    public Transform [] palancaSpawn;
    public GameObject palanca;

    public bool botonesActivados;
    bool [] botonesNivel8;
    bool [] paredesBool;
    
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;


        paredesBool = new bool [2];
        paredesBool[0] = false;
        paredesBool[1] = false;

        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
        
        for(int i=0; i<cajaSpawn.Length; i++){
            PhotonNetwork.Instantiate(caja.name, cajaSpawn[i].position, Quaternion.identity);
        }

        paredesAux = new GameObject [paredesSpawn.Length];

        for (int i = 0; i < paredesSpawn.Length; i++){
            if (i == 2 || i == 3){
                paredesAux[i] = PhotonNetwork.Instantiate(paredes[1].name, paredesSpawn[i].position, Quaternion.identity);  
            } else if (i == 4){
                paredesAux[i] = PhotonNetwork.Instantiate(paredes[2].name, paredesSpawn[i].position, Quaternion.identity);  
            } else {
                paredesAux[i] = PhotonNetwork.Instantiate(paredes[0].name, paredesSpawn[i].position, Quaternion.identity);  
            }
            if (i == 1 || i == 4)
                paredesAux[i].transform.rotation = Quaternion.Euler(0,0,90f);
            paredesAux[i].transform.localScale = paredesSpawn[i].localScale;
        }
        
        botonesActivados = false;
        botonesNivel8 = new bool [botonesSpawn.Length - 1];
        for (int i = 0; i < botonesNivel8.Length; i++){
            botonesNivel8[i] = false;
        }

        for(int i=0; i<botonesSpawn.Length; i++){
            GameObject b;   
            int idAux = 0;    
            if (i == 1){
                b = PhotonNetwork.Instantiate(boton[1].name, botonesSpawn[i].position, Quaternion.identity);  
                idAux = 1;
            } else if (i == 2){
                b = PhotonNetwork.Instantiate(boton[1].name, botonesSpawn[i].position, Quaternion.identity);  
                idAux = 2;
            } else if (i == 3){
                b = PhotonNetwork.Instantiate(boton[2].name, botonesSpawn[i].position, Quaternion.identity);  
                idAux = 3;
            } else {
                b = PhotonNetwork.Instantiate(boton[0].name, botonesSpawn[i].position, Quaternion.identity);  
            }
            b.GetComponent<BotonScript>().inicializarBoton(this, idAux);
        }


        for(int i=0; i<palancaSpawn.Length; i++){
            PhotonNetwork.Instantiate(palanca.name, palancaSpawn[i].position, Quaternion.identity);
        }

        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("pasoEscena", scene.name);
    }

    public override void reiniciarNivel()
    {
        base.reiniciarNivel();
    }

    public override void activarBotones(int id, bool aux){
        if (!PhotonNetwork.IsMasterClient)
            return;

        switch(id){
            case 0:
            case 3:
                if(aux)
                    desactivarParedes(id);
                else 
                    activarParedes(id);  
                break;
            case 1:
            case 2:
                paredesBool[id-1] = aux;
                if (paredesBool[0] && paredesBool[1]){
                    desactivarParedes(id);
                } else{
                    activarParedes(id);
                }
                break;
            default: 
                break;
        }        
    }

    public void desactivarParedes(int id){
        switch(id){
            case 0:
                paredesAux[0].GetComponent<Animator>().SetBool("Pared", true);
                paredesAux[1].GetComponent<Animator>().SetBool("Pared", true);
                break;
            case 1:
            case 2: 
                paredesAux[2].GetComponent<Animator>().SetBool("Pared", true);
                paredesAux[3].GetComponent<Animator>().SetBool("Pared", true);
                break;
            case 3:
                paredesAux[4].GetComponent<Animator>().SetBool("Pared", true);
                break;
            default:
                break;
        }
    }

    public void activarParedes(int id){
        switch(id){
            case 0:
                paredesAux[0].GetComponent<Animator>().SetBool("Pared", false);
                paredesAux[1].GetComponent<Animator>().SetBool("Pared", false);
                break;
            case 1:
            case 2: 
                paredesAux[2].GetComponent<Animator>().SetBool("Pared", false);
                paredesAux[3].GetComponent<Animator>().SetBool("Pared", false);
                break;
            case 3:
                paredesAux[4].GetComponent<Animator>().SetBool("Pared", false);
                break;
            default:
                break;
        }
    }
}
