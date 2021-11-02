using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class EscenaFinal : MonoBehaviourPun
{
    public void OnVolverMenu()
    {
        if (!photonView.IsMine){
            return;
        }
        Debug.Log("Pulsadooooo");
        SceneManager.LoadScene("Menu");
    }
}