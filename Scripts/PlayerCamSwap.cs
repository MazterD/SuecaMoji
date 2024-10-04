using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamSwap : MonoBehaviour
{

    public GameObject camVR;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && camVR.activeSelf)
        {
            DisableVR();
            ChangeInteraction();
        }
    }

    public void DisableVR() {
        camVR.SetActive(false);
        cam.SetActive(true);
    }

    public void ChangeInteraction() { 
        gameObject.GetComponent<Interaction>().enabled = false;
        gameObject.GetComponent<NonVRInteraction>().enabled = true;
    }
}
