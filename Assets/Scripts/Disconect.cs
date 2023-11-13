using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Disconect : MonoBehaviour
{
    //Leave the room.
    public void DisconectButton(){
        PhotonNetwork.LeaveRoom();
    }
}
