using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EscenaFinal : MonoBehaviourPun
{

    public void OnVolverMenu(){
        Debug.Log("Pulsadooooo");
        NetworkManager.instance.ChangeScene("Menu"); 
    }
}
