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
                GameObject.Find("AudioManager").GetComponent<AudioVolume>().playSfx("pasoNivel");
                Scene scene = SceneManager.GetActiveScene();
                string nombre = "";
                int level = 0;
                switch(scene.name){
                    case "Level1":
                        nombre = "Level2";
                        level = 2;
                        break;
                    case "Level2":
                        nombre = "Level3";
                        level = 3;
                        break;
                    case "Level3": 
                        nombre = "Level4";
                        level = 4;
                        break;
                    case "Level4": 
                        nombre = "Level5";
                        level = 5;
                        break;
                    case "Level5": 
                        nombre = "Level6";
                        level = 6;
                        break;
                    case "Level6":
                        nombre = "Level7";
                        level = 7;
                        break;
                    case "Level7":
                        nombre = "Level8";
                        level = 8;
                        break;
                    case "Level8":
                        nombre = "Level9";
                        level = 9;
                        break;
                    case "Level9":
                        nombre = "Level10";
                        level = 10;
                        break;
                    case "Level10":
                        nombre = "Final";
                        level = 11;
                        break;
                    default:
                        nombre = "Menu";
                        break;
                }

                if (level > PlayerPrefs.GetInt("actualLevel1")){
                    PlayerPrefs.SetInt("actualLevel1", level);
                }
                if (level > PlayerPrefs.GetInt("actualLevel2")){
                    PlayerPrefs.SetInt("actualLevel2", level);
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
