using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    
    public int numPlayers;

    //instance
    public static NetworkManager instance;

    void Awake(){
        if (instance != null && instance != this){
            gameObject.SetActive(false);
        }else{
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start(){
        //we want to connect to master server
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
        Debug.Log("You joined master server");
    }

    /*
    public override void OnJoinedLobby(){
        CreateRooms("testRoom");
    }

    public override void OnJoinedRoom(){
        Debug.Log("You joined the room" + PhotonNetwork.CurrentRoom.Name);
    }
    */

    public void CreateRooms(string roomName){
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte) numPlayers;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    public void JoinRoom(string roomName){
        PhotonNetwork.JoinRoom(roomName);
    }
    
   // [PunRPC]
    public void ChangeScene(string sceneName){
        PhotonNetwork.LoadLevel(sceneName);
    }

}
