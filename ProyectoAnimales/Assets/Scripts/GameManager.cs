using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;
using Cinemachine;
using UnityEngine.InputSystem;
//
public class GameManager : MonoBehaviourPun
{
    
    [Header("Players")]
    public string PlayerPrefabPath;
    public GameObject playerPrefab;

    public PlayerController[] players;
    public List<GameObject> playersGO;

    public Transform[] spawnPoints;
    public float respawnTime;

    private int playersInGame;

    //public GameObject rope;

    public static GameManager instance;

    public CinemachineVirtualCamera camara;

    void Awake(){
        instance = this;
        PlayerPrefabPath = this.playerPrefab.name;

        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        playersGO = new List<GameObject>();

        photonView.RPC("ImInGame", RpcTarget.All);
    }

    [PunRPC]
    void ImInGame(){
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length){
            SpawnPlayer();
        }
    }

    void SpawnPlayer(){
        Vector3 playerPos;
        if (PhotonNetwork.IsMasterClient){
            playerPos = spawnPoints[0].position;
        }else{
            playerPos = spawnPoints[1].position;
        }

        GameObject playerObject = PhotonNetwork.Instantiate(PlayerPrefabPath, playerPos, Quaternion.identity);
        camara.Follow = playerObject.transform;

        playerObject.GetComponent<PhotonView>().RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    public void LeavePlayer(){
        playersInGame--;
    }

}
