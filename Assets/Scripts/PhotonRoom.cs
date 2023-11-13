using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
   //Room info
   public static PhotonRoom room;

   public int currentScene;
   public int multiplayScene;

    public Text txtInfo;

    int numConnected;

   private void Awake(){
       //set up singleton
       if(PhotonRoom.room == null){
           PhotonRoom.room=this;
       }
       else{
           if(PhotonRoom.room!=this){
               Destroy(PhotonRoom.room.gameObject);
               PhotonRoom.room=this;
           }
       }
       DontDestroyOnLoad(this.gameObject);
   }

   public override void OnJoinedRoom(){
       //sets player data when we join the room
       base.OnJoinedRoom();
       Debug.Log("We are in a room");
       numConnected= PhotonNetwork.PlayerList.Length;
       string msgJoinRoom="Connected to a Room.  Room Name: "+PhotonNetwork.CurrentRoom.Name+ " Number of Players connected: "+numConnected;
       Debug.Log(msgJoinRoom);
       txtInfo.text=string.Format(msgJoinRoom, PhotonNetwork.CurrentRoom.Name,numConnected);
       Debug.Log(string.Format(msgJoinRoom, PhotonNetwork.CurrentRoom.Name,numConnected));

       StartGame();
   }


   void StartGame(){
       //loads the multiplayer scene for all players
       string msgMatchFound="Match found, starting a game (VS)";
       txtInfo.text=msgMatchFound;
       Debug.Log(msgMatchFound);
       PhotonNetwork.LoadLevel(multiplayScene);
    }
   
}

