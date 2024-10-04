using Photon.Pun;
using TMPro;
using UnityEngine;

public class ListControl : MonoBehaviourPun
{
    public GameObject controller;
    public GameObject list;
    public GameObject text;

    private int game = 0;
    private int playerID = 0;
    private bool activated = false;

    private string natural = "I am playing a trump card\r\n\r\nI am playing an ace\r\n\r\nI am playing a seven\r\n\r\nI have nothing to play\r\n\r\nI am playing low value\r\n\r\nI am playing high value";
    private string exaggerated = "I have nothing to play\r\n\r\nI am playing high value\r\n\r\nI am playing low value\r\n\r\nI am playing an ace\r\n\r\nI am playing a trump card\r\n\r\nI am playing a seven";

    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<TextMeshPro>().text = "";
        playerID = GetComponent<PlayerSetup>().id;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            if (activated == false) {
                activated = true;
            }
            list.gameObject.SetActive(true);
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)) { 
            activated = false;
            list.gameObject.SetActive(false);
        }

        if (game != 0 && playerID != 0 && activated) {
            LoadList();
        }
    }

    public void LoadList() {
        if (game == 2 && (playerID == 2 || playerID == 4))
        {
            text.GetComponent<TextMeshPro>().text = natural;
        }
        else if (game == 3 && (playerID == 1 || playerID == 3))
        {
            text.GetComponent<TextMeshPro>().text = exaggerated;
        }
        else { 
            text.GetComponent<TextMeshPro>().text = "";
        }
    }

    [PunRPC]
    public void ImportSettings(int nGame, int id) { 
        game = nGame;
        playerID = id;
    }

    public int GetPlayerID() { return playerID; }

    public int GetGame() { return game; }
}
