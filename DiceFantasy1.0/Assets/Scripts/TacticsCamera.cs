using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{
    public GameObject leftCameraButton;
    public GameObject rightCameraButton;
    //Rotacao da camera ao pressionar os botoes

    void Start()
    {
        leftCameraButton.SetActive(false);
    }
    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, 90, Space.Self);
        leftCameraButton.SetActive(false);
        rightCameraButton.SetActive(true);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up, -90, Space.Self);
        rightCameraButton.SetActive(false);
        leftCameraButton.SetActive(true);
    }

}
