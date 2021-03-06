using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviourPun
{
    public GameObject panel, panelOptions, panelControls,canvasMenu;
    public Button exitToLevelClient, resetLevelClient;


    bool isMobile;

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern bool IsMobile();
#endif
    void CheckIfMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif
    }

    public void Start()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            exitToLevelClient.interactable = false;
            resetLevelClient.interactable = false;
        }

        if (!photonView.IsMine) return;

        CheckIfMobile();

        if (isMobile)
        {
            // Screen.SetResolution(1280, 800, false);
            canvasMenu.transform.localScale = new Vector3(0.8f, 0.8f, canvasMenu.transform.localScale.z);
        }
    }
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
                PhotonNetwork.LoadLevel("Derrota");
            }
            else
            {
                PhotonNetwork.DestroyAll();
                PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
                PhotonNetwork.LoadLevel("Recargar");
            }
            
        }
    }

    public void OnOptions()
    {
        panel.SetActive(false);
        panelOptions.SetActive(true);

    }

    public void OnExitOptions()
    {
        panelOptions.SetActive(false);
        panel.SetActive(true);

    }

    public void OnControls()
    {
        panelOptions.SetActive(false);
        panelControls.SetActive(true);

    }

    public void OnExitControls()
    {
        panelControls.SetActive(false);
        panelOptions.SetActive(true);

    }

    public void OnExitToLevel(){
        photonView.RPC("OnLoseLife", RpcTarget.All);
        if (this.GetComponentInParent<PlayerController>().canvasVidas.GetComponent<LifesScript>().livesRemaining == 0)
        {
            PhotonNetwork.LoadLevel("Derrota");
        } else {
            photonView.RPC("OnLevelMenu", RpcTarget.All);
            PhotonNetwork.LoadLevel("Menu");
        }
        
    }

    [PunRPC]
    public void OnLevelMenu(){
        PlayerPrefs.SetInt("LevelMenu", 1);
    }

    public void OnExitToMain(){
        PhotonNetwork.LeaveRoom();
    }
}
