using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MasterSetup : MonoBehaviourPun
{
    public GameObject cam;
    public int id;

    public void IsLocalPlayer(int playerNumber)
    {
        cam.SetActive(true);
        id = playerNumber;
    }

    public void StartGame(int gameNumber) {
        PhotonView photonView = PhotonView.Find(1002);
        photonView.RPC("StartGame", RpcTarget.All, gameNumber);
    }
}
