using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class MainMenu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    
    public GameObject mainScreen, createRoomScreen, lobyScreen, lobyBrowserScreen, mesanjePantalla;

    public Button createRoomButton, findRoomButton;//Screen buttons
    public TextMeshProUGUI playerListText, roomInfoText;//loby text
    public Button startGameButton;//loby button
    public RectTransform roomListContainer;
    public GameObject roomButtonPrefab;
    public TMP_InputField playerNameInput;

    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomList = new List<RoomInfo>();


    void Start(){

        createRoomButton.interactable = false;
        findRoomButton.interactable = false;

        Cursor.lockState = CursorLockMode.None;

        if (PhotonNetwork.InRoom){
            //go to lobby

            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }

        if (!photonView.IsMine)
            return;

        /*
        if (SystemInfo.deviceType == DeviceType.Desktop){
            user.ActivateControlScheme("Keyboard&Mouse");
        } else if (SystemInfo.deviceType == DeviceType.Handheld){
            user.ActivateControlScheme("Movil");   
        }*/
        CheckIfMobile();

        if (isMobile){
            if(playerNameInput){
                TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default,false,false,true);
            }
        }    
    }

    bool isMobile;

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern bool IsMobile();
#endif
        void CheckIfMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif
    }

    void SetScreen(GameObject screen){
        mainScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        lobyScreen.SetActive(false);
        lobyBrowserScreen.SetActive(false);

        screen.SetActive(true);

        if (screen == lobyBrowserScreen)
            UpdateLobbyBrowserUI();
    }

    public void OnPlayerNameValueChanged(){
        PhotonNetwork.NickName = playerNameInput.text;
    }

    public override void OnConnectedToMaster(){
        createRoomButton.interactable = true;
        findRoomButton.interactable = true;
    }

    public void OnCreateRoomButton(){
        if(PhotonNetwork.NickName == ""){
            Avisar();
            return;
        }
        SetScreen(createRoomScreen);
    }

    public void OnFindRoomButton(){
        if(PhotonNetwork.NickName == ""){
            Avisar();
            return;
        }
        SetScreen(lobyBrowserScreen);
    }

    public void OnBackButton(){
        SetScreen(mainScreen);
    }

    public void OnCreateButton(TMP_InputField roomNameInput){
        if(roomNameInput.text == ""){
            Avisar();
            return;
        }
        NetworkManager.instance.CreateRooms(roomNameInput.text);
    }

    //Lobby Screen
    public override void OnJoinedRoom(){
        SetScreen(lobyScreen);

        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        UpdateLobbyUI();
    }

    [PunRPC]
    void UpdateLobbyUI(){
        //enable or disable the start button depending on if we are the Host
        startGameButton.interactable = PhotonNetwork.IsMasterClient;

        playerListText.text = "";
        foreach(Player player in PhotonNetwork.PlayerList)
            playerListText.text += player.NickName + "\n";

        roomInfoText.text = "<b>Room Name</b>\n" + PhotonNetwork.CurrentRoom.Name;
    }

    public void OnStartGameButton(){
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        NetworkManager.instance.ChangeScene("Game"); 
        //  NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game");
    }

    public void OnLeaveLobbyButton(){
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen);
    }


    //Lobby Browser
    GameObject CreateRoomButton(){
        GameObject buttonObj = Instantiate(roomButtonPrefab, roomListContainer.transform);
        roomButtons.Add(buttonObj);
        return buttonObj;
    }

    void UpdateLobbyBrowserUI(){
        foreach (GameObject button in roomButtons)
            button.SetActive(false);

        for(int i=0; i < roomList.Count; ++i){
            GameObject button = i >= roomButtons.Count ? CreateRoomButton() : roomButtons[i];
            button.SetActive(true);
            button.transform.Find("RoomNameText").GetComponent<TextMeshProUGUI>().text = roomList[i].Name;
            button.transform.Find("PlayerCountText").GetComponent<TextMeshProUGUI>().text = roomList[i].PlayerCount + "/" + roomList[i].MaxPlayers;
            
            Button buttonComp = button.GetComponent<Button>();
            string roomNameComp = roomList[i].Name;

            buttonComp.onClick.RemoveAllListeners();
            buttonComp.onClick.AddListener(() => { OnJoinRoomButton(roomNameComp); });
        }
    }

    public void OnJoinRoomButton(string roomName){
        NetworkManager.instance.JoinRoom(roomName);
    }

    public void OnRefreshButton(){
        UpdateLobbyBrowserUI();
    }

    public override void OnRoomListUpdate(List<RoomInfo> allRooms){
        roomList = allRooms;
    }

    public void Avisar(){
        StartCoroutine(AvisarCo()); 
    }


    IEnumerator AvisarCo(){
        mesanjePantalla.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        mesanjePantalla.SetActive(false);
    }



}
