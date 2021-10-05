using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    
    [Header("Players")]
    public string PlayerPrefabPath;
    public GameObject playerPrefab;

    public PlayerController[] players;

    public Transform[] spawnPoints;
    public float respawnTime;

    private int playersInGame;

    public static GameManager instance;

    void Awake(){
        instance = this;
        PlayerPrefabPath = this.playerPrefab.name;
    }

    void Start(){
        players = new PlayerController[PhotonNetwork.PlayerList.Length];

        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void ImInGame(){
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length){
            SpawnPlayer();
        }
    }

    void SpawnPlayer(){
        GameObject playerObject = PhotonNetwork.Instantiate(PlayerPrefabPath, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

        playerObject.GetComponent<PhotonView>().RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
    
}
