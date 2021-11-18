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
    public void Start()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            playAgain.interactable = false;
        }
    }
    public void OnBackToMainMenu(){
        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel("Menu");
    }

    public void OnPlayAgain(){

    }
}
