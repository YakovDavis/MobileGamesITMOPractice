using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;

    private GameObject controller;
    
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.up * 10, Quaternion.identity, 0, new object[] { PV.ViewID });
        PhotonNetwork.LocalPlayer.TagObject = controller;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "YBotEnemy"), Vector3.up * 10 + Vector3.right * 10, Quaternion.identity);
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
