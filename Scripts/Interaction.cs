using Photon.Pun;
using UnityEngine;

public class Interaction : MonoBehaviourPun
{

    public GameObject remote;
    public GameObject menu;
    public Material mat;
    public Material mat2;

    private float playerX;
    private float playerZ;

    private GameObject line;
    private LineRenderer lr;

    private bool isPressed = false;
    private bool isDeployed = false;

    private GameObject menuRef;

    private int[] uiAddr = { 2001, 3001, 4001, 5001 };

    // Start is called before the first frame update
    void Start()
    {
        playerX = transform.position.x;
        playerZ = transform.position.z;

        line = new GameObject();
        line.transform.position = remote.transform.position;
        line.AddComponent<LineRenderer>();

        Vector3 v1 = remote.transform.position;
        v1 = remote.transform.TransformPoint(Vector3.forward * 10);

        lr = line.GetComponent<LineRenderer>();
        
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(remote.transform.position, remote.transform.forward, out hit, 100))
        {
            lr.enabled = true;
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                isPressed = true;
                if (hit.transform.gameObject.tag == "Card")
                {
                    GameObject card = hit.transform.gameObject;

                    PhotonView photonView = PhotonView.Find(1002);
                    photonView.RPC("PlayCard", RpcTarget.All, GetComponent<PlayerSetup>().id, playerX, playerZ, card.GetComponent<CardData>().name, card.GetComponent<CardData>().suit);
                }

                else if (hit.transform.gameObject.tag == "Menu") {
                    GameObject option = hit.transform.gameObject;

                    PhotonView photonView = PhotonView.Find(uiAddr[GetComponent<PlayerSetup>().id - 1]);
                    photonView.RPC("SetExpression", RpcTarget.All, GetComponent<PlayerSetup>().id, option.name);

                    PhotonView master = PhotonView.Find(1002);
                    master.RPC("LogExpression", RpcTarget.All, GetComponent<PlayerSetup>().id, option.name);
                }

            }
            else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) { 
                isPressed = false;
            }
            SetLine(isPressed ? mat2 : mat, remote.transform.position, hit.point);

        }
        else
        {
            lr.enabled = false;
        }

        ListControl lc = gameObject.GetComponent<ListControl>();
        int g = lc.GetGame();
        int p = lc.GetPlayerID();

        if ((g == 2 && (p == 2 || p == 4)) || (g == 3 && (p == 1 || p == 3))) {
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                if (!isDeployed)
                {
                    menuRef = Instantiate(menu, remote.transform.position + remote.transform.forward, remote.transform.rotation);
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

    void SetLine(Material material, Vector3 start, Vector3 end) {
        lr.material = material;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
