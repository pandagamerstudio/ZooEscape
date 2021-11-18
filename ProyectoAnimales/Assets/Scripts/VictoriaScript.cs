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
    public void Start()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            exitToLevelClient.interactable = false;
        }
    }
    public void OnBackToLevelMenu(){
        PlayerPrefs.SetInt("LevelMenu", 1);
        PhotonNetwork.LoadLevel("Menu");
    }

    public void OnBackToMainMenu(){
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Menu");
    }
}
