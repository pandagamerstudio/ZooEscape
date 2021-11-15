using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviourPun
{
    public GameObject panel;
    /*private LifesScript livesScript;

    private void Start()
    {
        livesScript = FindObjectOfType<LifesScript>();
    }*/

    public void OnPause(){
        panel.SetActive(true);
    }

    public void OnResume(){
        panel.SetActive(false);
        if (this.GetComponentInParent<PlayerController>().isMobile)
            this.GetComponentInParent<PlayerController>().canvas.SetActive(true);
    }

    public void OnResetLevel(){
        if (PhotonNetwork.IsMasterClient)
        {
            this.GetComponentInParent<PlayerController>().canvasVidas.GetComponent<LifesScript>().LoseLife();
            PhotonNetwork.DestroyAll();
            PlayerPrefs.SetString ("Scene", SceneManager.GetActiveScene().name);
            PhotonNetwork.LoadLevel("Recargar");
            //photonView.RPC("LoseLife", RpcTarget.All);
        }
    }

    public void OnOptions(){

    }

    public void OnExitToLevel(){

    }

    public void OnExitToMain(){
        PhotonNetwork.LoadLevel("Menu");
    }

}
