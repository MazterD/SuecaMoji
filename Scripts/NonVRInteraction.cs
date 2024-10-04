using Photon.Pun;
using UnityEngine;

public class NonVRInteraction : MonoBehaviour
{

    public Camera cam;
    public GameObject menu;

    private float playerX;
    private float playerZ;

    private bool isDeployed = false;

    private GameObject menuRef;

    private int[] uiAddr = { 2001, 3001, 4001, 5001 };

    // Start is called before the first frame update
    void Start()
    {
        playerX = transform.position.x;
        playerZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.transform.gameObject.tag == "Card")
                {
                    GameObject card = hit.transform.gameObject;

                    PhotonView photonView = PhotonView.Find(1002);
                    photonView.RPC("PlayCard", RpcTarget.All, GetComponent<PlayerSetup>().id, playerX, playerZ, card.GetComponent<CardData>().name, card.GetComponent<CardData>().suit);
                }

                else if (hit.transform.gameObject.tag == "Menu")
                {
                    GameObject option = hit.transform.gameObject;

                    PhotonView photonView = PhotonView.Find(uiAddr[GetComponent<PlayerSetup>().id - 1]);
                    photonView.RPC("SetExpression", RpcTarget.All, GetComponent<PlayerSetup>().id, option.name);

                    PhotonView master = PhotonView.Find(1002);
                    master.RPC("LogExpression", RpcTarget.All, GetComponent<PlayerSetup>().id, option.name);
                }
            }
        }

        ListControl lc = gameObject.GetComponent<ListControl>();
        int g = lc.GetGame();
        int p = lc.GetPlayerID();

        if ((g == 2 && (p == 2 || p == 4)) || (g == 3 && (p == 1 || p == 3)))
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (!isDeployed)
                {
                    //Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    float menuX = 0f;
                    float menuY = 2f;
                    float menuZ = 0f;

                    if (playerX == 0)
                    {
                        if (playerZ > 0)
                        {
                            //mousePos.z = 5;
                            menuZ = 8f;
                        }
                        else
                        {
                            //mousePos.z = -5;
                            menuZ = -8f;
                        }
                    }
                    else
                    {
                        if (playerX > 0)
                        {
                            //mousePos.x = 5;
                            menuX = 8f;
                        }
                        else
                        {
                            //mousePos.x = -5;
                            menuX = -8f;
                        }
                    }

                    //mousePos = cam.ScreenToWorldPoint(mousePos);
                    menuRef = Instantiate(menu, new Vector3(menuX, menuY, menuZ), cam.transform.rotation);
                    isDeployed = true;
                }
                menuRef.SetActive(true);
            }
            else if (isDeployed)
            {
                menuRef.SetActive(false);
                Destroy(menuRef);
                isDeployed = false;
            }
        }
    }
}
