using UnityEngine;
using System.Collections;
using System;

public class Mouse : MonoBehaviour {
    [Tooltip("Higher value = lower speed")]
    public int dragSpeed = 1;
    public float scrollSpeed = 10;
    public double dragSpeedReduction = 0.2;
    public int cameraStartPosX = 5;
    public int cameraStartPosY = -5;

    void Start()
    {
        this.transform.position = new Vector3(cameraStartPosX, 1, cameraStartPosY);
    }

    // Update is called once per frame
    void Update () {
        float h = -Input.GetAxis("Mouse X") / dragSpeed;
        float v = -Input.GetAxis("Mouse Y") / dragSpeed;

        //If left click
        if (Input.GetMouseButton(0))
        {
            this.gameObject.transform.Translate(h, v, 0);
        }
    }
}
