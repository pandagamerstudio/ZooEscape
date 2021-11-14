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
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }
    
}
