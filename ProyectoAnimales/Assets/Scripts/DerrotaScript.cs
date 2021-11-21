using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DerrotaScript : MonoBehaviourPun
{
    public Button playAgain;
    public GameObject canvasMenu;
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
            playAgain.interactable = false;
        }
        if (!photonView.IsMine) return;

        CheckIfMobile();

        if (isMobile)
        {
            // Screen.SetResolution(1280, 800, false);
            canvasMenu.transform.localScale = new Vector3(0.8f, 0.8f, canvasMenu.transform.localScale.z);
        }
    }
    public void OnBackToMainMenu(){
        PlayerPrefs.SetInt("actualLevel1", 1);
        PlayerPrefs.SetInt("actualLevel2", 1);
        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel("Menu");
    }

    public void OnPlayAgain(){
        PlayerPrefs.SetInt("actualLevel1", 1);
        PlayerPrefs.SetInt("actualLevel2", 1);
        PlayerPrefs.SetInt("LevelMenu", 1);
        PhotonNetwork.LoadLevel("Menu");
    }
}
