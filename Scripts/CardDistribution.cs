using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardDistribution : MonoBehaviourPun
{
    #region Card Models

    public GameObject twoClubs;
    public GameObject threeClubs;
    public GameObject fourClubs;
    public GameObject fiveClubs;
    public GameObject sixClubs;
    public GameObject sevenClubs;
    public GameObject aceClubs;
    public GameObject jackClubs;
    public GameObject queenClubs;
    public GameObject kingClubs;

    public GameObject twoDiamonds;
    public GameObject threeDiamonds;
    public GameObject fourDiamonds;
    public GameObject fiveDiamonds;
    public GameObject sixDiamonds;
    public GameObject sevenDiamonds;
    public GameObject aceDiamonds;
    public GameObject jackDiamonds;
    public GameObject queenDiamonds;
    public GameObject kingDiamonds;

    public GameObject twoSpades;
    public GameObject threeSpades;
    public GameObject fourSpades;
    public GameObject fiveSpades;
    public GameObject sixSpades;
    public GameObject sevenSpades;
    public GameObject aceSpades;
    public GameObject jackSpades;
    public GameObject queenSpades;
    public GameObject kingSpades;

    public GameObject twoHearts;
    public GameObject threeHearts;
    public GameObject fourHearts;
    public GameObject fiveHearts;
    public GameObject sixHearts;
    public GameObject sevenHearts;
    public GameObject aceHearts;
    public GameObject jackHearts;
    public GameObject queenHearts;
    public GameObject kingHearts;

    #endregion

    #region Private Fields

    private List<GameObject> deck = new List<GameObject>();
    private Dictionary<int, GameObject> field = new Dictionary<int, GameObject>();

    private List<GameObject> cardsOne = new List<GameObject>();
    private List< GameObject> cardsTwo = new List<GameObject>();
    private List<GameObject> cardsThree = new List<GameObject>();
    private List<GameObject> cardsFour = new List<GameObject>();

    private List<GameObject> stashOne = new List<GameObject>();
    private List<GameObject> stashTwo = new List<GameObject>();
    private List<GameObject> stashThree = new List<GameObject>();
    private List<GameObject> stashFour = new List<GameObject>();

    private Vector3 handAnchorOne = new Vector3(0f, 1.5f, 5f);
    private Vector3 handAnchorTwo = new Vector3(5f, 1.5f, 0f);
    private Vector3 handAnchorThree = new Vector3(0f, 1.5f, -5f);
    private Vector3 handAnchorFour = new Vector3(-5f, 1.5f, 0f);

    private int shuffleTime = 0;
    private int currentPlayer = 0;
    private int round = 1;
    private string powerSuit = "None";
    private string roundSuit = "None";
    private int countdown = 100;

    private bool gameRunning = false;
    private float startTime;
    private float endTime;
    private int nGame = 0;
    private int nSession = 0;

    private string filePath;
    private bool setup = false;

    int[] uiAddr = { 2001, 3001, 4001, 5001 };

    #endregion

    #region Public Fields

    public GameObject turnUI;
    public GameObject playerUI;
    public GameObject extraUI;

    #endregion

    #region MonoBehaviour Calls

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deck.Count > 0 && PhotonNetwork.CurrentRoom.PlayerCount == 5)
        {
            if (deck.Count == 40 && countdown == 0)
            {
                ResetPlayerUI();
                UpdateTurnUI();
                //UpdateExtraUI();
            }

            else if (deck.Count == 40 && countdown > 0) { 
                countdown--;
            }

            else if(deck.Count == 0 && countdown == 0)
            {
                countdown = 100;
            }

            if (countdown == 0) {
                ShuffleCards();
            }
        }

        if (setup == false) {
            filePath = Path.Combine(Application.persistentDataPath + "/", "TestLogs.csv");
            Debug.Log(filePath);
            InitializeCSVFile(filePath);
            setup = true;
        }
    }

    #endregion

    #region Card Handlers

    static void FisherYatesShuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;

        // Start from the end and swap elements with a random one
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void ShuffleCards()
    {
        if (shuffleTime % 20 == 0)
        {
            GameObject card = deck.First();
            deck.Remove(card);
            GiveCard(card);

            if (currentPlayer == 3)
            {
                currentPlayer = 0;
            }
            else
            {
                currentPlayer++;
            }
        }
        shuffleTime++;

        if (deck.Count == 0) {
            WriteToCSV(filePath, Time.time - startTime, nSession, nGame, 0, "DistributedCards", "none");
        }
    }

    public void GenerateDeck(int nGame) {
        if (nGame == 1) //Game 1 (Espadas)
        {
            //P1: KQS, J62C, A53H, 74D
            //P2: 74S, KQC, J62H, A53D
            //P3: A53S, 74C, KQH, J62D
            //P4: J62S, A53C, 74H, KQD

            deck.Add(PhotonNetwork.Instantiate(sevenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
        }

        else if (nGame == 2)    //Game 2 (Paus)
        {
            //P1: Q62S, 75C, 7KH, KJ3D -----> pontos: 10 + 2 + 4 + 4 + 10 + 3 = 33 -----> cartas: 10
            //P2: 7K3S, A4C, J54H, Q2D -----> pontos: 11 + 2 + 10 + 4 + 3 = 30 -----> cartas: 10
            //P3: A4S, JQ3C, 632H, A4D -----> pontos: 3 + 2 + 11 + 11 = 27 -----> cartas: 10
            //P4: J5S, K62C, AQH, 765D -----> pontos: 4 + 11 + 10 + 3 + 2 = 30 -----> cartas: 10

            deck.Add(PhotonNetwork.Instantiate(kingDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity)); 
        }

        else if (nGame == 3)    //Game 3 (Ouros)
        {
            //P1: 74S, KQC, J62H, A53D
            //P2: A53S, 74C, KQH, J62D
            //P3: J62S, A53C, 74H, KQD
            //P4: KQS, J62C, A53H, 74D

            deck.Add(PhotonNetwork.Instantiate(fourSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(twoClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
        }

        else
        {
            deck.Add(PhotonNetwork.Instantiate(twoClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingClubs.name, new Vector3(0f, 0f, 0f), Quaternion.identity));

            deck.Add(PhotonNetwork.Instantiate(twoDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingDiamonds.name, new Vector3(0f, 0f, 0f), Quaternion.identity));

            deck.Add(PhotonNetwork.Instantiate(twoSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingSpades.name, new Vector3(0f, 0f, 0f), Quaternion.identity));

            deck.Add(PhotonNetwork.Instantiate(twoHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(threeHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fourHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(fiveHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sixHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(sevenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(aceHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(jackHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(queenHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
            deck.Add(PhotonNetwork.Instantiate(kingHearts.name, new Vector3(0f, 0f, 0f), Quaternion.identity));
        }

        foreach (GameObject card in deck) {
            card.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }

    public void GiveCard(GameObject card) {
        int cardOwner = currentPlayer + 1;
        card.GetComponent<CardData>().owner = cardOwner;
        if (cardOwner == 1)
        {
            cardsOne.Add(card);
            cardsOne = DisplayCards(cardsOne, handAnchorOne, cardOwner);
        }
        else if (cardOwner == 2)
        {
            cardsTwo.Add(card);
            cardsTwo = DisplayCards(cardsTwo, handAnchorTwo, cardOwner);
        }
        else if (cardOwner == 3)
        { 
            cardsThree.Add(card);
            cardsThree = DisplayCards(cardsThree, handAnchorThree, cardOwner);
        }
        else if(cardOwner == 4)
        {
            cardsFour.Add(card);
            cardsFour = DisplayCards(cardsFour, handAnchorFour, cardOwner);
        }
        
    }

    public List<GameObject> DisplayCards(List<GameObject> cards, Vector3 anchor, int owner) {
        float handX = anchor.x;
        float handY = anchor.y;
        float handZ = anchor.z;
        float rotation = 90f * (owner - 1f);
        int size = cards.Count;

        List<GameObject> newCards = new List<GameObject>();

        if (handX == 0f)
        {
            handX = (size - 1) * -0.25f;

            foreach (GameObject card in cards)
            {
                PhotonNetwork.Destroy(card);
                
                string name = card.name.Substring(0, card.name.Length - 7);
                GameObject newCard = PhotonNetwork.Instantiate(name, new Vector3(handX, handY, handZ), Quaternion.Euler(0f, rotation, 0f));
                newCard.GetComponent<CardData>().owner = owner;
                newCards.Add(newCard);

                handX = handX + 0.5f;
            }
        }
        else if (handZ == 0f)
        {
            handZ = (size - 1) * -0.25f;

            foreach (GameObject card in cards)
            {
                PhotonNetwork.Destroy(card);
                
                string name = card.name.Substring(0, card.name.Length - 7);
                GameObject newCard = PhotonNetwork.Instantiate(name, new Vector3(handX, handY, handZ), Quaternion.Euler(0f, rotation, 0f));
                newCard.GetComponent<CardData>().owner = owner;
                newCards.Add(newCard);
                
                handZ = handZ + 0.5f;
            }
        }

        return newCards;
    }

    public GameObject FindCard(int id, string name, string suit) {
        List<GameObject> hand = new List<GameObject> ();
        if (id == 1) {
            hand = cardsOne;
        }
        else if (id == 2) {
            hand = cardsTwo;
        }
        else if (id == 3) {
            hand = cardsThree;
        }
        else if (id == 4) { 
            hand = cardsFour;
        }

        foreach (GameObject card in hand) {
            if (card.GetComponent<CardData>().name == name && card.GetComponent<CardData>().suit == suit) {
                return card;
            }
        }
        
        return null;
    }

    public void RemoveCard(int id, GameObject card) {
        if (id == 1)
        {
            cardsOne.Remove(card);
            cardsOne = DisplayCards(cardsOne, handAnchorOne, id);
        }
        else if (id == 2)
        {
            cardsTwo.Remove(card);
            cardsTwo = DisplayCards(cardsTwo, handAnchorTwo, id);
        }
        else if (id == 3)
        {
            cardsThree.Remove(card);
            cardsThree = DisplayCards(cardsThree, handAnchorThree, id);
        }
        else if (id == 4)
        {
            cardsFour.Remove(card);
            cardsFour = DisplayCards(cardsFour, handAnchorFour, id);
        }
    }

    public void DestroyDeck() {
        List<GameObject>[] stashes = { stashOne, stashTwo, stashThree, stashFour };

        foreach (List<GameObject> stash in stashes) {
            foreach (GameObject card in stash)
            {
                PhotonNetwork.Destroy(card);
            }
        }
        
        stashOne.Clear();
        stashTwo.Clear();
        stashThree.Clear();
        stashFour.Clear();
    }

    #endregion

    #region Rule Enforcers

    public bool HasSuit(int id) { 
        List<GameObject> hand = new List<GameObject>();

        if (id == 1)
        {
            hand = cardsOne;
        }
        else if (id == 2)
        {
            hand = cardsTwo;
        }
        else if (id == 3) { 
            hand = cardsThree;
        }
        else if (id == 4) { 
            hand = cardsFour;
        }

        foreach (GameObject card in hand) {
            if (card.GetComponent<CardData>().suit == roundSuit) {
                return true;
            }
        }

        return false;
    }
    public void CheckEndRound() {
        if (field.Count == 4) {
            StartCoroutine(WaitAndClear());
        }
    }

    public IEnumerator WaitAndClear() {
        currentPlayer = 69;
        Debug.Log("Round End");
        int winner = CheckWinner();
        Debug.Log("Player " + winner.ToString() + " won this round");
        WriteToCSV(filePath, Time.time - startTime, nSession, nGame, winner, "RoundWin", "none");

        UpdateRoundWin(winner);
        
        yield return new WaitForSeconds(4);
        GiveCardsToWinner(winner);

        field.Clear();
        ResetPlayerUI();
        roundSuit = "None";
        currentPlayer = winner - 1;
        UpdateTurnUI();

        if (round < 10)
        {
            round++;
            UpdateExtraUI();
        }
        else
        {
            CheckGameWinner();
            round = 1;
            currentPlayer = 0;
            powerSuit = "None";
            DestroyDeck();
            gameRunning = false;
            endTime = Time.time - startTime;
            Debug.Log("Ended game at " + endTime.ToString() + " seconds of runtime");
            /*GenerateDeck();
            FisherYatesShuffle(deck);
            powerSuit = deck.Last().GetComponent<CardData>().suit;
            Debug.Log("The power suit for this game is " + powerSuit);*/
        }
    }

    public int CheckWinner() {
        int winner = 1;
        GameObject winCard = field[1];

        foreach (int id in field.Keys) {
            if (id != 1) {
                GameObject card = field[id];
                if (card.GetComponent<CardData>().suit == powerSuit)
                {
                    if (winCard.GetComponent<CardData>().suit == powerSuit && card.GetComponent<CardData>().value > winCard.GetComponent<CardData>().value) {
                        winner = id;
                        winCard = card;
                    }
                    else if (winCard.GetComponent<CardData>().suit != powerSuit) {
                        winner = id;
                        winCard = card;
                    }
                }
                else if (card.GetComponent<CardData>().suit == roundSuit)
                {
                    if (winCard.GetComponent<CardData>().suit == roundSuit && card.GetComponent<CardData>().value > winCard.GetComponent<CardData>().value) {
                        winner = id;
                        winCard = card;
                    }
                    else if (winCard.GetComponent<CardData>().suit != roundSuit && winCard.GetComponent<CardData>().suit != powerSuit) {
                        winner = id;
                        winCard = card;
                    }
                }
            }
        }
        return winner;
    }

    public void GiveCardsToWinner(int winner) {
        List<GameObject> stash = new List<GameObject>();

        foreach (GameObject card in field.Values)
        {
            PhotonNetwork.Destroy(card);
            string name = card.name.Substring(0, card.name.Length - 7);
            GameObject newCard = PhotonNetwork.Instantiate(name, new Vector3(0f, -5f, 0f), Quaternion.identity);
            stash.Add(newCard);
        }

        if (winner == 1) {
            stashOne.AddRange(stash);
        }
        else if (winner == 2)
        {
            stashTwo.AddRange(stash);
        }
        else if (winner == 3) {
            stashThree.AddRange(stash);
        }
        else if(winner == 4)
        {
            stashFour.AddRange(stash);
        }
    }

    public void CheckGameWinner() {
        int teamOne = GetStashPoints(1) + GetStashPoints(3);
        int teamTwo = GetStashPoints(2) + GetStashPoints(4);

        if (teamOne > teamTwo)
        {
            Debug.Log("The winners are players 1 and 3 with a total of " + teamOne + " points");
            WriteToCSV(filePath, Time.time - startTime, nSession, nGame, 0, "P1and3Win", teamOne.ToString());
            UpdateGameWin(1, 3);
        }
        else {
            Debug.Log("The winners are players 2 and 4 with a total of " + teamTwo + " points");
            WriteToCSV(filePath, Time.time - startTime, nSession, nGame, 0, "P2and4Win", teamTwo.ToString());
            UpdateGameWin(2, 4);
        }
    }

    public int GetStashPoints(int id) {
        int points = 0;

        List<GameObject> stash = new List<GameObject>();

        if (id == 1)
        {
            stash = stashOne;
        }
        else if (id == 2)
        {
            stash = stashTwo;
        }
        else if (id == 3)
        {
            stash = stashThree;
        }
        else if (id == 4)
        {
            stash = stashFour;
        }

        foreach (GameObject card in stash) {
            points += card.GetComponent<CardData>().value;
        }

        return points;
    }

    #endregion

    #region RPC Calls

    [PunRPC]

    public void PlayCard(int id, float playerX, float playerZ, string name, string suit) {
        Debug.Log("Received play card request from player " + id.ToString());
        Debug.Log("Current player is player " + currentPlayer.ToString());
        GameObject card = FindCard(id, name, suit);

        if (card == null) {
            return;
        }

        if (id != currentPlayer + 1) {
            return;
        }

        /*else if (card.GetComponent<CardData>().owner != id) {
            return;
        }

        else if (field.Keys.Contains(id)) {
            return;
        }*/

        GameObject newCard = card;

        if (suit == roundSuit || !HasSuit(id)) {
            if (playerX == 0)
            {
                float cardZ = card.transform.position.z;
                if (Mathf.Abs(playerZ - cardZ) == 3f)
                {
                    PhotonNetwork.Destroy(card);
                    string newName = card.name.Substring(0, card.name.Length - 7);
                    if (currentPlayer + 1 == 1)
                    {
                        newCard = PhotonNetwork.Instantiate(newName, new Vector3(0f, 1.1f, playerZ / 3), Quaternion.Euler(-90f, 0f, 0f));
                    }
                    else
                    {
                        newCard = PhotonNetwork.Instantiate(newName, new Vector3(0f, 1.1f, playerZ / 3), Quaternion.Euler(-90f, 180f, 0f));
                    }
                }
            }
            else if (playerZ == 0)
            {
                float cardX = card.transform.position.x;

                if (Mathf.Abs(playerX - cardX) == 3f)
                {
                    PhotonNetwork.Destroy(card);
                    string newName = card.name.Substring(0, card.name.Length - 7);
                    if (currentPlayer + 1 == 2)
                    {
                        newCard = PhotonNetwork.Instantiate(newName, new Vector3(playerX / 3, 1.1f, 0f), Quaternion.Euler(-90f, 90f, 0f));
                    }
                    else
                    {
                        newCard = PhotonNetwork.Instantiate(newName, new Vector3(playerX / 3, 1.1f, 0f), Quaternion.Euler(-90f, -90f, 0f));
                    }

                }
            }

            field.Add(id, newCard);
            RemoveCard(id, card);
            UpdatePlayerUI(currentPlayer);
            Debug.Log("Player " + id.ToString() + " has played " + newCard.GetComponent<CardData>().name + " of " + newCard.GetComponent<CardData>().suit);
            WriteToCSV(filePath, Time.time - startTime, nSession, nGame, id, "PlayCard", newCard.GetComponent<CardData>().name + " of " + newCard.GetComponent<CardData>().suit);

            if (roundSuit == "None")
            {
                roundSuit = newCard.GetComponent<CardData>().suit;
                Debug.Log("The suit for this round is " + roundSuit);
            }

            currentPlayer++;
            currentPlayer %= 4;
            UpdateTurnUI();

            CheckEndRound();
        }  
    }

    [PunRPC]
    public void LogExpression(int playerID, string expr) {
        //Debug.Log("Player " + playerID.ToString() + " selected " + expr);
        WriteToCSV(filePath, Time.time - startTime, nSession, nGame, playerID, "SetExpression", expr);
    }

    #endregion

    #region UI Updaters

    public void UpdateTurnUI() {
        int id = currentPlayer + 1;
        turnUI.GetComponent<TextMeshProUGUI>().text = "P" + id.ToString() + " Turn";

        PhotonView photonView;
        foreach (int pun in uiAddr) {
            photonView = PhotonView.Find(pun);
            photonView.RPC("UpdateTurnUI", RpcTarget.All, currentPlayer);
        }
    }

    public void UpdateRoundWin(int winner)
    {
        turnUI.GetComponent<TextMeshProUGUI>().text = "P" + winner.ToString() + " Round Win";

        PhotonView photonView;
        foreach (int pun in uiAddr)
        {
            photonView = PhotonView.Find(pun);
            photonView.RPC("UpdateRoundWin", RpcTarget.All, winner);
        }
    }

    public void UpdateGameWin(int one, int two)
    {
        turnUI.GetComponent<TextMeshProUGUI>().text = "P" + one.ToString() + " & P" + two.ToString() + " Game Win";

        PhotonView photonView;
        foreach (int pun in uiAddr)
        {
            photonView = PhotonView.Find(pun);
            photonView.RPC("UpdateGameWin", RpcTarget.All, one, two);
        }
    }

    public void UpdatePlayerUI(int id) {
        string card = field[id + 1].GetComponent<CardData>().name;
        string suit = field[id + 1].GetComponent<CardData>().suit;
        string playerID = "P" + (id + 1).ToString() + " -> ";
        string currentText = playerUI.GetComponent<TextMeshProUGUI>().text;

        if (suit == "Spades")
        {
            suit = "\u2660";
        }
        else if (suit == "Clubs")
        {
            suit = "\u2663";
        }
        else if (suit == "Hearts")
        {
            suit = "\u2665";
        }
        else if (suit == "Diamonds") {
            suit = "\u2666";
        }

        string updated = playerID + card + suit;
        int start = currentText.IndexOf(playerID);
        string startText = currentText.Substring(0, start);
        
        string endText = "";
        if (id != 3)
        {
            int end = currentText.IndexOf("\n", start);
            endText = currentText.Substring(end, currentText.Length - end);
        }

        playerUI.GetComponent<TextMeshProUGUI>().text = startText + updated + endText;

        PhotonView photonView;
        foreach (int pun in uiAddr)
        {
            photonView = PhotonView.Find(pun);
            photonView.RPC("UpdatePlayerUI", RpcTarget.All, startText + updated + endText);
        }
    }

    public void ResetPlayerUI() {
        playerUI.GetComponent<TextMeshProUGUI>().text = "P1 -> None\n\nP2 -> None\n\nP3 -> None\n\nP4 -> None";

        PhotonView photonView;
        foreach (int pun in uiAddr)
        {
            photonView = PhotonView.Find(pun);
            photonView.RPC("ResetPlayerUI", RpcTarget.All);
        }
    }

    public void UpdateExtraUI() {
        extraUI.GetComponent<TextMeshProUGUI>().text = "\nRound " + round.ToString() + "\n\n\nTrump: " + powerSuit;

        PhotonView photonView;
        foreach (int pun in uiAddr)
        {
            photonView = PhotonView.Find(pun);
            photonView.RPC("UpdateExtraUI", RpcTarget.All, round, powerSuit);
        }
    }

    public void ResetExtraUI() {
        extraUI.GetComponent<TextMeshProUGUI>().text = "\nRound 0\n\n\nTrump: None";

        PhotonView photonView;
        foreach (int pun in uiAddr)
        {
            photonView = PhotonView.Find(pun);
            photonView.RPC("ResetExtraUI", RpcTarget.All);
        }
    }

    #endregion

    #region Game Launcher

    [PunRPC]
    public void StartGame(int gameNumber) {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 5 || gameRunning == true) {
            return;
        }
        
        gameRunning = true;
        startTime = Time.time;
        nGame = gameNumber;
        Debug.Log("Starting game " + nGame.ToString() + " at " + startTime.ToString() + " seconds of runtime");

        //filePath = Path.Combine(Application.persistentDataPath, "TestLogs.csv");
        //InitializeCSVFile(filePath);
        DefineSession(filePath);
        WriteToCSV(filePath, startTime - startTime, nSession, nGame, 0, "StartGame", "none");

        LoadPlayerList(gameNumber);
        GenerateDeck(gameNumber);
        //FisherYatesShuffle(deck);
        powerSuit = deck.Last().GetComponent<CardData>().suit;
        Debug.Log("The power suit for this game is " + powerSuit);
        UpdateExtraUI();
    }

    public void LoadPlayerList(int nGame) {
        int id = 0;
        foreach (int pun in uiAddr)
        {
            id++;
            PhotonView photonView = PhotonView.Find(pun);
            photonView.RPC("ImportSettings", RpcTarget.All, nGame, id);
        }
    }

    #endregion

    #region Exportation

    private void InitializeCSVFile(string path)
    {
        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Timestamp:Session:Game:Player:Action:Value");
            }
        }
    }

    private void WriteToCSV(string path, float timestamp, int session, int game, int player, string action, string value) {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{timestamp}:{session}:{game}:{player}:{action}:{value}");
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to write to CSV file: {e.Message}");
        }
    }

    private void DefineSession(string path)
    {
        try
        {
            string line = File.ReadLines(path).Last();
            if (!line.Contains("Session"))
            {
                string[] newline = line.Split(':');

                if (nGame == 1)
                {
                    nSession = (int.Parse(newline[1])) + 1;
                }
                else {
                    nSession = (int.Parse(newline[1]));
                }
            }
            else
            {
                nSession = 1;
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to write to CSV file: {e.Message}");
        }
    }

    #endregion

    //            -------- CENAS QUE FALTAM --------
    //
    // 1 - timer para jogada (30 segundos?) (provavelmente não)
    // 2 - potencial QoL (sons, highlights nas cartas, etc.) (grande talvez)
    //
    //            ------------- EL FIN -------------
}
