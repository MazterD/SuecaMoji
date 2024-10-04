using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
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
        if (Input.GetKey(KeyCode.LeftShift) && camVR.activeSelf) {
            Debug.Log("Changing Camera to Desktop Mode");
            camVR.SetActive(false);
            cam.SetActive(true);
        }
    }
}
