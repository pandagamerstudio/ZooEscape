using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PuertaScript : MonoBehaviourPun
{
    public int playersIn;

    void Awake(){
        playersIn = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!PhotonNetwork.IsMasterClient)
            return;

        if (collision.CompareTag("Player")){
            playersIn++;

            GameObject l = GameObject.FindWithTag("Llave");
            if (playersIn == 2 && (l == null)){
                Scene scene = SceneManager.GetActiveScene();
                string nombre = "";
                switch(scene.name){
                    case "Level1":
                        nombre = "Level2";
                        break;
                    case "Level2":
                    nombre = "Level3";
                        break;
                    case "Level3": 
                    default:
                        nombre = "Menu";
                        break;
                }
                PhotonNetwork.LoadLevel(nombre);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(!PhotonNetwork.IsMasterClient)
            return;

        if (collision.CompareTag("Player")){
            playersIn--;
        }
    }
}
