using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
   public static PhotonLobby lobby;
   RoomInfo[] rooms;
   public GameObject btnJoin;
   public GameObject btnLeave;
    public Text txtInfo;

     public Text txtNumPlayers;

   private void Awake(){
       lobby=this;//Create the singleton, lives withing the scene
   }

   void Start(){
       PhotonNetwork.ConnectUsingSettings(); // connects to master photon server
   }

    //How many players are connected right now?
   void Update(){
       int numPlayers=PhotonNetwork.CountOfPlayers;
       txtNumPlayers.text="Number of Players: "+numPlayers.ToString()+"/20"; //20 because we are on the free tier
   }

   //Set up envrionment once we are connected to master
   public override void OnConnectedToMaster(){
       string message="Connected to master";
       Debug.Log(message);
       txtInfo.text=message;       
       PhotonNetwork.AutomaticallySyncScene=true;
       btnJoin.SetActive(true);
   }

   public void OnJoinButtonClicked(){
       Debug.Log("Battle button was click");
       btnJoin.SetActive(false);
       btnLeave.SetActive(true);
       PhotonNetwork.JoinRandomRoom();
   }

    //If we can't join a room, create a new room.
   public override void OnJoinRandomFailed(short returnCode, string message){
       string mes="Failed to join the room";
       Debug.Log(mes);
       txtInfo.text=mes; 
       CreateRoom();
   }
   
   //Create a room, or see if there is one available (that part is handled by Photon, it will create a new one if there is no room available)
   void CreateRoom(){
       string mes="Trying to create a room";
       Debug.Log(mes);
       txtInfo.text=mes; 
       int randomRoomName = Random.Range(0,10000);
       RoomOptions roomOps = new RoomOptions(){IsVisible=true,IsOpen=true,MaxPlayers=20}; //all of our players in the same room, if we want multiple rooms, make MaxPlayers to a small number
       PhotonNetwork.CreateRoom("Room"+randomRoomName,roomOps);
   }

   //If we fail to create a room, try again.
   public override void OnCreateRoomFailed(short returnCode, string message){
       string mes="Create Room Failed";
       Debug.Log(mes);
       txtInfo.text=mes; 
       CreateRoom();
   }

   //Disconect from the room
   public void OnCancelButtonClick(){
       btnLeave.SetActive(false);
       btnJoin.SetActive(true);
       PhotonNetwork.LeaveRoom();
   }

}
