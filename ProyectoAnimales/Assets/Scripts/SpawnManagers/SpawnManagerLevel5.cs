using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class SpawnManagerLevel5 : SpawnManagerLevel1
{
    public Transform cajaSpawn;
    public GameObject caja;
    public Transform [] botonesSpawn;
    public GameObject boton;
    public Transform botonEspecialSpawn;
    public GameObject botonEspecial;
    public Transform [] paredesSpawn;
    public Transform [] paredesSpawn2;
    public GameObject paredes;
    GameObject[] paredesAux;
    GameObject[] paredesAux2;
    public Transform[] platMovilesSpawn;
    public GameObject platMovil;
    GameObject[] platAux;

    public bool[] botonesActivados;
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.Instantiate(key.name, keySpawn.position, Quaternion.identity);

        PhotonNetwork.Instantiate(puerta.name, puertaSpawn.position, Quaternion.identity);
        
        PhotonNetwork.Instantiate(caja.name, cajaSpawn.position, Quaternion.identity);

        GameObject botonEs = PhotonNetwork.Instantiate(botonEspecial.name, botonEspecialSpawn.position, Quaternion.identity);



        botonEs.GetComponent<BotonQuitarPlat>().inicializarBoton(this);


        paredesAux = new GameObject [paredesSpawn.Length];

        for (int i = 0; i < paredesSpawn.Length; i++){
            paredesAux[i] = PhotonNetwork.Instantiate(paredes.name, paredesSpawn[i].position, Quaternion.identity);
            if(i == paredesSpawn.Length - 1){
                paredesAux[i].transform.rotation = Quaternion.Euler(0,0,90f);
            }
        }

        paredesAux2 = new GameObject [paredesSpawn2.Length];
        for (int i = 0; i < paredesSpawn2.Length; i++){
            paredesAux2[i] = PhotonNetwork.Instantiate(paredes.name, paredesSpawn2[i].position, Quaternion.identity);
            paredesAux2[i].GetComponent<Animator>().SetBool("Pared", true);
        }
        
        botonesActivados = new bool[botonesSpawn.Length];

        for (int i = 0; i < botonesSpawn.Length; i++){
          GameObject b = PhotonNetwork.Instantiate(boton.name, botonesSpawn[i].position, Quaternion.identity);  
          b.GetComponent<BotonScriptLevel5>().inicializarBoton(this, i);
        }

        platAux = new GameObject [platMovilesSpawn.Length];

        for (int i = 0; i < platMovilesSpawn.Length; i++){
          platAux[i] = PhotonNetwork.Instantiate(platMovil.name, platMovilesSpawn[i].position, Quaternion.identity);  
        }

        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("pasoEscena", scene.name);

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
            //p.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            //p.GetComponent<BoxCollider2D>().enabled = false;
            //paredesAux[0].transform.position = new Vector3(19.67f, -0.93f, 0);
            //paredesAux[1].transform.position = new Vector3(26.29f, -0.93f, 0);
        foreach(GameObject p in paredesAux){
            p.GetComponent<Animator>().SetBool("Pared", true);
        }

        foreach(GameObject p in paredesAux2){
            p.GetComponent<Animator>().SetBool("Pared", false);
        }
    }

    public void activarParedes(){
            //p.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            //p.GetComponent<BoxCollider2D>().enabled = true;
            //paredesAux[0].transform.position = new Vector3(19.67f, 7.56f, 0);
            //paredesAux[1].transform.position = new Vector3(26.29f, 7.56f, 0);
            //paredesAux[2].transform.position = new Vector3(23f, 10.78f, 0);

        foreach(GameObject p in paredesAux){
            p.GetComponent<Animator>().SetBool("Pared", false);
        }

        foreach(GameObject p in paredesAux2){
            p.GetComponent<Animator>().SetBool("Pared", true);
        }
    }

    public void desactivarPlat(){
        foreach(GameObject p in platAux){
            //p.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            //p.GetComponent<BoxCollider2D>().enabled = false;
            p.GetComponent<Animator>().SetBool("Activada", true);
        }
    }

    public void activarPlat(){
        foreach(GameObject p in platAux){
            //p.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            //p.GetComponent<BoxCollider2D>().enabled = true;
            p.GetComponent<Animator>().SetBool("Activada", false);
        }
    }
}
