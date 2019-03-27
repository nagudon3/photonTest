using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    private PhotonLobby lobby;
    public GameObject battleButton; //to initializes the UI button
    public GameObject cancelButton;
    public GameObject offlineButton;


    private void Awake() {
        lobby = this; //Get the component of this instance(its own)
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Player has connected to the Photon Master server.");
        battleButton.SetActive(true);
        cancelButton.SetActive(false);
        offlineButton.SetActive(false);
    }

    public void OnClickBattleButton()
    {
        Debug.Log("Battle button clicked");;
        PhotonNetwork.JoinRandomRoom(); //Join any available room in the server
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Can't connect to any room at the momment.");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Try to create room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSetting.maxPlayers };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined a room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Fail to create the room. The same room name is exist");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Try to cancel.");
        battleButton.SetActive(true);
        cancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}