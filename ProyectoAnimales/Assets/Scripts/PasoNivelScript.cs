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
    public void Start()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            exitToLevelClient.interactable = false;
            nextLevel.interactable = false;
        }
    }
    public void OnBackToLevelMenu(){
        PlayerPrefs.SetInt("LevelMenu", 1);
        PhotonNetwork.LoadLevel("Menu");
    }
}
