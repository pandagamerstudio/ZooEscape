using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public override void OnPlayerLeftRoom (Player otherPlayer){
        base.OnPlayerLeftRoom(otherPlayer);

        if (PlayerPrefs.GetInt("Completado") != 1){
            PlayerPrefs.SetInt("actualLevel1", 1);
            PlayerPrefs.SetInt("actualLevel2", 1);
        }

        GameManager.instance.LeavePlayer();
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LoadLevel("Desconexion");
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel("Menu");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        if (PlayerPrefs.GetInt("Completado") != 1){
            PlayerPrefs.SetInt("actualLevel1", 1);
            PlayerPrefs.SetInt("actualLevel2", 1);
        }

        PhotonNetwork.LoadLevel("Menu");
    }

}
