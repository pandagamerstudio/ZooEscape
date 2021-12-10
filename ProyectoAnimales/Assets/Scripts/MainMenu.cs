using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class MainMenu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public string scene;

    public GameObject mainScreen, createRoomScreen, lobyScreen, lobyBrowserScreen, menuPrincipalScreen, opcionesScreen, creditosScreen, mesanjePantalla, controlesScreen, levelsScreen,
        historia1;

    public Button createRoomButton, findRoomButton;//Screen buttons

    public TextMeshProUGUI playerListText, roomInfoText;//loby text
    public Button startGameButton, buttonContinue;
    public Button[] levelButtons; 
    public RectTransform roomListContainer;
    public GameObject roomButtonPrefab;
    public TMP_InputField playerNameInput;

    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomList = new List<RoomInfo>();
    int idJugSel, actualLevel1, actualLevel2;
    public GameObject[] personajes;
    public GameObject canvasMenu;

    public GameObject[] carteles;
    public GameObject[] cartelesPos;


    void Start(){

        if (PhotonNetwork.NickName != "") playerNameInput.text = PhotonNetwork.NickName;

        if (PlayerPrefs.GetInt("livesRemaining") == 0) PlayerPrefs.SetInt("livesRemaining", 4);
        
        if (PlayerPrefs.HasKey("LevelMenu")){
            if (PlayerPrefs.GetInt("LevelMenu") == 1){
                if (PhotonNetwork.IsMasterClient){
                    if (PlayerPrefs.HasKey("actualLevel1") == false)
                    {
                        PlayerPrefs.SetInt("actualLevel1", 1);
                        actualLevel1 = 1;
                    }else{
                        actualLevel1 = PlayerPrefs.GetInt("actualLevel1");
                    }
                }else{
                    if (PlayerPrefs.HasKey("actualLevel2") == false)
                    {
                        PlayerPrefs.SetInt("actualLevel2", 1);
                        actualLevel2 = 1;
                    }else{
                        actualLevel2 = PlayerPrefs.GetInt("actualLevel2");
                    }
                }
                photonView.RPC("SetScreenLevels", RpcTarget.All);
                PlayerPrefs.SetInt("LevelMenu", 0);

                photonView.RPC("GetCharacters", RpcTarget.All);
                

                return;
            }
        }

        PlayerPrefs.DeleteKey("livesRemaining");

        if (PlayerPrefs.HasKey("music") == false)
        {
            PlayerPrefs.SetFloat("music", 0.5f);
        }
        if (PlayerPrefs.HasKey("sfx") == false)
        {
            PlayerPrefs.SetFloat("sfx", 0.5f);
        }

        if (PhotonNetwork.IsMasterClient){
            if (PlayerPrefs.HasKey("actualLevel1") == false)
            {
                PlayerPrefs.SetInt("actualLevel1", 1);
                actualLevel1 = 1;
            }else{
                actualLevel1 = PlayerPrefs.GetInt("actualLevel1");
            }
        }else{
            if (PlayerPrefs.HasKey("actualLevel2") == false)
            {
                PlayerPrefs.SetInt("actualLevel2", 1);
                actualLevel2 = 1;
            }else{
                actualLevel2 = PlayerPrefs.GetInt("actualLevel2");
            }
        }

        if (!PlayerPrefs.HasKey("Completado"))
            PlayerPrefs.SetInt("Completado", 0);

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
           // Screen.SetResolution(1280, 800, false);
            canvasMenu.transform.localScale = new Vector3(0.8f, 0.8f, canvasMenu.transform.localScale.z);
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
        menuPrincipalScreen.SetActive(false);
        creditosScreen.SetActive(false);
        opcionesScreen.SetActive(false);
        controlesScreen.SetActive(false);
        levelsScreen.SetActive(false);
        historia1.SetActive(false);

        screen.SetActive(true);

        if (screen == lobyBrowserScreen)
            UpdateLobbyBrowserUI();
    }

    [PunRPC]
    public void SetScreenLevels(){
        mainScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        lobyScreen.SetActive(false);
        lobyBrowserScreen.SetActive(false);
        menuPrincipalScreen.SetActive(false);
        creditosScreen.SetActive(false);
        opcionesScreen.SetActive(false);
        controlesScreen.SetActive(false);

        ComprobarNiveles();
        levelsScreen.SetActive(true);
    }

    [PunRPC]
    public void SetScreenHistoria()
    {
        SetScreen(historia1);
        if (!PhotonNetwork.IsMasterClient)
        {
            buttonContinue.interactable = false;
        }
    }

    [PunRPC]
    public void SetScreenLobby(){
        SetScreen(lobyScreen);

        SelectorPersonajes(idJugSel);
        UpdateLobbyUI();
        ComprobarNiveles();
    }

    [PunRPC]
    public void GetCharacters(){
        if (PhotonNetwork.IsMasterClient)
            idJugSel = PlayerPrefs.GetInt("idPersonaje1");
        else
            idJugSel = PlayerPrefs.GetInt("idPersonaje2");
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

    public void OnBackInLevelsButton(){
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.CurrentRoom.IsVisible = true;
        }
        photonView.RPC("SetScreenLobby", RpcTarget.All);
    }

    public void OnVolverButton(){
        SetScreen(menuPrincipalScreen);
    }

    public void OnVolverOpcButton(){
        SetScreen(opcionesScreen);
    }

    public void OnCreateButton(TMP_InputField roomNameInput){
        if(roomNameInput.text == ""){
            Avisar();
            return;
        }
        NetworkManager.instance.CreateRooms(roomNameInput.text);
    }

    public void OnPlayButton(){
        SetScreen(mainScreen);
    }

    public void OnCreditosButton(){
        SetScreen(creditosScreen);
    }
    public void OnOptionsButton(){
        SetScreen(opcionesScreen);
    }

    public void OnControlesButton()
    {
        SetScreen(controlesScreen);
    }



    //Lobby Screen
    public override void OnJoinedRoom(){
        SetScreen(lobyScreen);
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
        if (PhotonNetwork.IsMasterClient){
            idJugSel = 0;
            PlayerPrefs.SetInt ("idPersonaje1", idJugSel);
        } else {
            idJugSel = 1;
            PlayerPrefs.SetInt ("idPersonaje2", idJugSel);
        }

        SelectorPersonajes(idJugSel);
        ComprobarNiveles();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){

        //UpdateLobbyUI();
        //GameManager.instance.LeavePlayer();
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        SetScreen(mainScreen);
    }


    [PunRPC]
    void UpdateLobbyUI(){
        //enable or disable the start button depending on if we are the Host
        if (PhotonNetwork.PlayerList.Length == 2)
            startGameButton.interactable = PhotonNetwork.IsMasterClient;
        else
            startGameButton.interactable = false;

        playerListText.text = "";
        foreach(Player player in PhotonNetwork.PlayerList)
            playerListText.text += player.NickName + "\n\n";

        roomInfoText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void OnStartGameButton(){
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        photonView.RPC("SetScreenLevels", RpcTarget.All);
        //NetworkManager.instance.ChangeScene("Level1"); 
        //NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game");
    }

    public void OnStartLevelButton(int n){
        if(n == 0)
        {
            photonView.RPC("SetScreenHistoria", RpcTarget.All);
            return;
        }
        NetworkManager.instance.ChangeScene("Level"+n); 
    }

    public void OnPruebaButton(){
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        NetworkManager.instance.ChangeScene(scene); 
        //NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game");
    }

    public void OnLeaveLobbyButton(){
        PhotonNetwork.LeaveRoom();
        //SetScreen(mainScreen);
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

            if (roomList[i].MaxPlayers == 0) button.SetActive(false);
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

    public void SelectorPersonajes(int i){
        if(PhotonNetwork.PlayerList.Length != 2) i = 0;

        if (PhotonNetwork.PlayerList.Length == 1) carteles[1].transform.position = cartelesPos[3].transform.position;

        if (PhotonNetwork.IsMasterClient){
            if (carteles[1].transform.position == cartelesPos[i].transform.position)
                return;
        }else{
            if (carteles[0].transform.position == cartelesPos[i].transform.position)
                return;
        }

        idJugSel = i;
        foreach(GameObject p in personajes){
            p.GetComponent<Animator>().SetBool("Seleccionado", false);
        }
        Debug.Log(i);
        personajes[i].GetComponent<Animator>().SetBool("Seleccionado", true);
        photonView.RPC("PersonajeSeleccionado", RpcTarget.All, idJugSel, PhotonNetwork.IsMasterClient);

        if (PhotonNetwork.IsMasterClient){
            PlayerPrefs.SetInt ("idPersonaje1", idJugSel);
        }else{
            PlayerPrefs.SetInt ("idPersonaje2", idJugSel);
        }
    }

    [PunRPC]
    public void PersonajeSeleccionado(int i, bool master){
        if (master){
            carteles[0].transform.position = cartelesPos[i].transform.position;
        }else{
            carteles[1].transform.position = cartelesPos[i].transform.position;
        }
    }

    public void OnSelectSound (){
        GameObject.Find("AudioManager").GetComponent<AudioVolume>().playSfx("seleccionar");
    }

    public void OnSelectSoundLevel (){
        GameObject.Find("AudioManager").GetComponent<AudioVolume>().playSfx("pasoNivel");
    }
    public void ComprobarNiveles(){
        if (actualLevel1 >= actualLevel2){
            for(int i=levelButtons.Length-1; i >= actualLevel1 ; i--){
                levelButtons[i].interactable = false;
            }
        }else{
            for(int i=levelButtons.Length-1; i >= actualLevel2 ; i--){
                levelButtons[i].interactable = false;
            }
        }

        if (!PhotonNetwork.IsMasterClient)
            levelsScreen.transform.GetChild(levelsScreen.transform.childCount-1).gameObject.SetActive(true);
    }



}
