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
                
                SceneManager.LoadScene("MenuIAs");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            playersIn--;
        }
    }
}
