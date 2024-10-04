using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    #region Public Fields

    public GameObject playerF;
    public GameObject playerM;
    public GameObject master;
    [Space]
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    [Space]
    public GameObject gameManager;
    [Space]
    public Transform spawnPointP1;
    public Transform spawnPointP2;
    public Transform spawnPointP3;
    public Transform spawnPointP4;
    [Space]
    public GameObject ui;

    #endregion

    #region Private Fields

    private Dictionary<int, Transform> spawnPoint;
    private Dictionary<int, Quaternion> spawnCameras;

    private GameObject gameInstance;

    #endregion

    #region Pun Callbacks

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting...");

        spawnPoint = GenerateSpawns();
        spawnCameras = GenerateCameras();

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("Entered lobby");

        PhotonNetwork.JoinOrCreateRoom("test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Connected to a room");

        int playerNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log(playerNumber);

        if (playerNumber == 1)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);

            GameObject _master = PhotonNetwork.Instantiate(master.name, master.transform.position, Quaternion.identity);
            _master.GetComponent<MasterSetup>().IsLocalPlayer(playerNumber);

            GameObject _manager = PhotonNetwork.Instantiate(gameManager.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _manager.GetComponent<CardDistribution>().enabled = true;

            GameObject _ui = _master.transform.GetChild(1).gameObject;//Instantiate(ui, new Vector3(0f, 0f, 0f), Quaternion.identity);
            GameObject _turnUI = _ui.transform.GetChild(0).GetChild(3).gameObject;
            GameObject _playerUI = _ui.transform.GetChild(0).GetChild(2).gameObject;
            GameObject _extraUI = _ui.transform.GetChild(0).GetChild(5).gameObject;
            //_ui.transform.GetChild(1).gameObject.SetActive(true);

            _ui.SetActive(true);

            _manager.GetComponent<CardDistribution>().turnUI = _turnUI;
            _manager.GetComponent<CardDistribution>().playerUI = _playerUI;
            _manager.GetComponent<CardDistribution>().extraUI = _extraUI;
        }

        else if (playerNumber <= 5)
        {
            int id = playerNumber - 1;
            GameObject _player;
            if (playerNumber == 2 || playerNumber == 3)
            {
                _player = PhotonNetwork.Instantiate(playerF.name, spawnPoint[id].position, spawnCameras[id]);
            }
            else {
                _player = PhotonNetwork.Instantiate(playerM.name, spawnPoint[id].position, spawnCameras[id]);
            }
            GenerateObject(id);
            _player.GetComponent<PlayerSetup>().IsLocalPlayer(id);
            _player.transform.GetChild(1).gameObject.SetActive(true);

            //GameObject _ui = Instantiate(ui, new Vector3(0f, 0f, 0f), Quaternion.identity);
            //GameObject _turnUI = _ui.transform.GetChild(0).GetChild(3).gameObject;
            //GameObject _playerUI = _ui.transform.GetChild(0).GetChild(2).gameObject;

            //_ui.gameObject.SetActive(true);

            //_player.GetComponent<PlayerSetup>().turnUI = _turnUI;
            //_player.GetComponent<PlayerSetup>().playerUI = _playerUI;
        }
    }

    #endregion

    #region Custom Methods

    public Dictionary<int, Transform> GenerateSpawns() {
        Dictionary<int, Transform> sp = new Dictionary<int, Transform>
        {
            { 1, spawnPointP1 },
            { 2, spawnPointP2 },
            { 3, spawnPointP3 },
            { 4, spawnPointP4 }
        };

        return sp;
    }

    public Dictionary<int, Quaternion> GenerateCameras()
    {
        Dictionary<int, Quaternion> sp = new Dictionary<int, Quaternion>
        {
            { 1, Quaternion.Euler(0, 180, 0) },
            { 2, Quaternion.Euler(0, -90, 0) },
            { 3, Quaternion.Euler(0, 0, 0) },
            { 4, Quaternion.Euler(0, 90, 0) }
        };

        return sp;
    }

    public void GenerateObject(int playerNumber) {
        Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>
        {
            { 1, object1 },
            { 2, object2 },
            { 3, object3 },
            { 4, object4 }
        };

        PhotonNetwork.Instantiate(objects[playerNumber].name, objects[playerNumber].transform.position, objects[playerNumber].transform.rotation);
    }

    #endregion
}
