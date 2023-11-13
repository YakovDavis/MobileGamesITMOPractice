using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
   private PhotonView PV;
   public Text txtNumber;
   int globalNumber;

    void Start(){
        PV= GetComponent<PhotonView>();
    }

    void Update(){
        txtNumber.text="Number to Increase: "+globalNumber;
    }

    public void IncreaseNumber(){
        PV.RPC("RPC_IncreaseNumber",RpcTarget.AllViaServer);
    }

    [PunRPC]
    void RPC_IncreaseNumber(){
        globalNumber++;
    }
}
