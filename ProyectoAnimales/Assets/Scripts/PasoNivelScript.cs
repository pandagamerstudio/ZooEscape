using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PasoNivelScript : MonoBehaviourPun
{    
    public Button nextLevel, exitToLevelClient;

    string escenaActual;
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
            nextLevel.interactable = false;
        }

        escenaActual = PlayerPrefs.GetString("pasoEscena");

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

    public void OnNextLevel()
    {
        string nombre = "";
        int level = 0;
        switch (escenaActual)
        {
            case "Level1":
                nombre = "Level2";
                break;
            case "Level2":
                nombre = "Level3";
                break;
            case "Level3":
                nombre = "Level4";
                break;
            case "Level4":
                nombre = "Level5";
                break;
            case "Level5":
                nombre = "Level6";
                break;
            case "Level6":
                nombre = "Level7";
                break;
            case "Level7":
                nombre = "Level8";
                break;
            case "Level8":
                nombre = "Level9";
                break;
            case "Level9":
                nombre = "Level10";
                break;
            case "Level10":
                nombre = "Victoria";
                break;
            default:
                nombre = "Menu";
                break;
        }

        PhotonNetwork.LoadLevel(nombre);
    }
}
