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

    [PunRPC]
    public void OnLoseLife()
    {
        this.GetComponentInParent<PlayerController>().canvasVidas.GetComponent<LifesScript>().LoseLife();
        GameObject.Find("AudioManager").GetComponent<AudioVolume>().playSfx("derrota");
    }
    
    public void OnResetLevel(){
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("OnLoseLife", RpcTarget.All);

            if (this.GetComponentInParent<PlayerController>().canvasVidas.GetComponent<LifesScript>().livesRemaining == 0)
            {
                GameManager.instance.LeavePlayer();
                //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel("Menu");
            }
            else
            {
                PhotonNetwork.DestroyAll();
                PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
                PhotonNetwork.LoadLevel("Recargar");
            }
            
        }
    }

    public void OnOptions(){

    }

    public void OnExitToLevel(){
        PlayerPrefs.SetInt("LevelMenu", 1);
        PhotonNetwork.LoadLevel("Menu");
    }

    public void OnExitToMain(){
        GameManager.instance.LeavePlayer();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }

}
