using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject firstPersonCam;
    public GameObject thirdPersonCam;

    private void Awake()
    {
        firstPersonCam.SetActive(true);
        thirdPersonCam.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (firstPersonCam.activeSelf)
            {
                firstPersonCam.SetActive(false);
                thirdPersonCam.SetActive(true);
            } else
            {
                firstPersonCam.SetActive(true);
                thirdPersonCam.SetActive(false);
            }
        }
    }
}
