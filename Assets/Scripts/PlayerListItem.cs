using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text text;
    private Player player;
    
    public void Setup(Player player_)
    {
        player = player_;
        text.text = player_.NickName;
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        if (player == other)
        {
            Destroy(gameObject);
        }
    }
    
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
