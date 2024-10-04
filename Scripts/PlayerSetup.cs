using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.Events;

public class PlayerSetup : MonoBehaviourPun
{
    public Movement movement;
    public Interaction interaction;
    public GameObject cam;
    public GameObject avatar;
    public int id;

    public GameObject turnUI;
    public GameObject playerUI;
    public GameObject extraUI;

    public UnityEvent onSpawn;

    public void Start()
    {
        if (photonView.IsMine || PhotonNetwork.IsMasterClient) { 
            onSpawn.Invoke();
        }
    }

    public void IsLocalPlayer(int playerNumber) {
        movement.enabled = false;
        interaction.enabled = true;
        cam.SetActive(true);
        avatar.SetActive(false);
        id = playerNumber;
    }

    [PunRPC]
    public void UpdateTurnUI(int currentPlayer)
    {
        if (turnUI == null) {
            return;
        }

        Debug.Log("Update Turn UI");
        int playerID = currentPlayer + 1;
        turnUI.GetComponent<TextMeshProUGUI>().text = "P" + playerID.ToString() + " Turn";
    }

    [PunRPC]
    public void ResetTurnUI()
    {
        if (turnUI == null)
        {
            return;
        }

        Debug.Log("Reset Turn UI");
        turnUI.GetComponent<TextMeshProUGUI>().text = "P1 Turn";
    }

    [PunRPC]
    public void UpdatePlayerUI(string text)
    {
        if (playerUI == null) {
            return;
        }

        Debug.Log("Update Player UI");
        playerUI.GetComponent<TextMeshProUGUI>().text = text;
    }

    [PunRPC]
    public void ResetPlayerUI()
    {
        if (playerUI == null)
        {
            return;
        }

        Debug.Log("Reset Player UI");
        playerUI.GetComponent<TextMeshProUGUI>().text = "P1 -> None\n\nP2 -> None\n\nP3 -> None\n\nP4 -> None";
    }

    [PunRPC]
    public void UpdateExtraUI(int round, string trump)
    { 
        if (extraUI == null)
        {
            return;
        }

        Debug.Log("Update Extra UI");
        extraUI.GetComponent<TextMeshProUGUI>().text = "\nRound " + round.ToString() + "\n\n\nTrump: " + trump;
    }

    [PunRPC]
    public void ResetExtraUI()
    {
        if (extraUI == null)
        {
            return;
        }

        Debug.Log("Reset Extra UI");
        extraUI.GetComponent<TextMeshProUGUI>().text = "\nRound 0\n\n\nTrump: None";
    }

    [PunRPC]
    public void UpdateRoundWin(int winner)
    {
        if (turnUI == null)
        {
            return;
        }

        Debug.Log("Update Round Win");
        turnUI.GetComponent<TextMeshProUGUI>().text = "P" + winner.ToString() + " Round Win";
    }

    [PunRPC]
    public void UpdateGameWin(int one, int two)
    {
        if (turnUI == null)
        {
            return;
        }

        turnUI.GetComponent<TextMeshProUGUI>().text = "P" + one.ToString() + " & P" + two.ToString() + " Game Win";
    }
}
