using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviourPun
{
    public GameObject panel;
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
            PhotonNetwork.DestroyAll();
            PlayerPrefs.SetString ("Scene", SceneManager.GetActiveScene().name);
            PhotonNetwork.LoadLevel("Recargar");
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
