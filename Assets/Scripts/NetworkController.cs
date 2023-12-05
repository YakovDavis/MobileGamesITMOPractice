using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Sentry;
public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController Instance;
    
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TMP_Text numPlayersText;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private Transform roomListContainer;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform playerListContainer;
    [SerializeField] private GameObject playerListItemPrefab;
    [SerializeField] private GameObject startGameButton;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Exception e = new Exception("Test exception");
        SentrySdk.CaptureException(e);
        Debug.Log("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Now connected to the " + PhotonNetwork.CloudRegion + " server");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("MainMenu");
        Debug.Log("Joined lobby");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInput.text);
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = nickNameInput.text;
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MenuManager.Instance.OpenMenu("RoomMenu");

        foreach (Transform child in playerListContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Instantiate(playerListItemPrefab, playerListContainer).GetComponent<PlayerListItem>().Setup(player);
        }
        
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMaster)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room creation failed: " + message;
        MenuManager.Instance.OpenMenu("ErrorMenu");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("TitleMenu");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform t in roomListContainer)
        {
            Destroy(t.gameObject);
        }
        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContainer).GetComponent<RoomListItem>().Setup(room);
        }
    }

    public void RefreshPlayerList()
    {
        foreach (Transform child in playerListContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Instantiate(playerListItemPrefab, playerListContainer).GetComponent<PlayerListItem>().Setup(player);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RefreshPlayerList();
    }
    
    public override void OnPlayerLeftRoom(Player leftPlayer)
    {
        RefreshPlayerList();
    }

    public void Update()
    {
        int numPlayers=PhotonNetwork.CountOfPlayers;
        numPlayersText.text="Number of Players: " + numPlayers.ToString() + "/20"; //20 because we are on the free tier
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}

