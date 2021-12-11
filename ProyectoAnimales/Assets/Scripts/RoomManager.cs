using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public override void OnPlayerLeftRoom (Player otherPlayer){
        base.OnPlayerLeftRoom(otherPlayer);

        GameManager.instance.LeavePlayer();
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LoadLevel("Desconexion");
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel("Menu");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        PhotonNetwork.LoadLevel("Menu");
    }

}
