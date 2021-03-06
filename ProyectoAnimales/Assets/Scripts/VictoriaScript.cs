using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoriaScript : MonoBehaviourPun
{
    public Button exitToLevelClient;
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
            exitToLevelClient.interactable = false;
        }
        if (!photonView.IsMine) return;

        CheckIfMobile();

        if (isMobile)
        {
            // Screen.SetResolution(1280, 800, false);
            canvasMenu.transform.localScale = new Vector3(0.8f, 0.8f, canvasMenu.transform.localScale.z);
        }
    }
    public void OnBackToLevelMenu(){
        PlayerPrefs.SetInt("LevelMenu", 1);
        PhotonNetwork.LoadLevel("Menu");
    }

    public void OnBackToMainMenu(){
        PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene("Menu");
    }


}
