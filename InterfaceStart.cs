using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceStart : MonoBehaviour
{
    public Camera camera;
    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;

    public float minimumY = -60.0f;
    public float maximumY = 60;

    float rotationX = 0.0f;
    float rotationY = 0.0f;

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
 
        camera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}
