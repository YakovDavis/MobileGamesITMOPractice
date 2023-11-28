using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public RoomInfo roomInfo;
    
    public void Setup(RoomInfo roomInfo_)
    {
        roomInfo = roomInfo_;
        text.text = roomInfo.Name;
    }

    public void OnClick()
    {
        NetworkController.Instance.JoinRoom(roomInfo);
    }
}
