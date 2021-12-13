using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PuertaScriptIA : MonoBehaviour
{
    public int playersIn;

    void Awake(){
        playersIn = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Player")){
            playersIn++;
            GameObject l = GameObject.FindWithTag("Llave");
            if (playersIn == 1 && (l == null)){

                GameObject.Find("AudioManager").GetComponent<AudioVolume>().playSfx("pasoNivel");
                
                /*Scene scene = SceneManager.GetActiveScene();
                string nombre = "";
                int level = 0;
                switch(scene.name){
                    case "Level1":
                        level = 2;
                        break;
                    case "Level2":
                        level = 3;
                        break;
                    case "Level3": 
                        level = 4;
                        break;
                    case "Level4": 
                        level = 5;
                        break;
                    case "Level5": 
                        level = 6;
                        break;
                    case "Level6":
                        level = 7;
                        break;
                    case "Level7":
                        level = 8;
                        break;
                    case "Level8":
                        level = 9;
                        break;
                    case "Level9":
                        level = 10;
                        break;
                    case "Level10":
                        level = 11;
                        break;
                    default:
                        break;
                }
*/
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            playersIn--;
        }
    }
}
