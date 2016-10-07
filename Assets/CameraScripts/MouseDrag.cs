using UnityEngine;
using System.Collections;
using System;

public class MouseDrag : MonoBehaviour {
    //Changes object position when mouse is dragged

    [SerializeField]
    public double dragSpeed = 10;
    [SerializeField]
    public float scrollSpeed = 10;
    [SerializeField]
    public double dragSpeedReduction = 0.2;
    void Update()
    {
        float h = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse X");
        float v = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse Y");

        //If left click
        if(Input.GetMouseButton(0))
        {
            this.gameObject.transform.Translate(h, v, 0);
        }

        //Detect scroll wheel 
        var d = Input.GetAxis("Mouse ScrollWheel");
        //If scroll up
        if (d > 0f && this.transform.position.y > 70)
        {
            this.gameObject.transform.Translate(0, 0, scrollSpeed);
            dragSpeed = dragSpeed - dragSpeedReduction;
        }
        //If scroll down
        else if(d < 0f)
        {
            this.gameObject.transform.Translate(0, 0, -scrollSpeed);
            dragSpeed = dragSpeed + dragSpeedReduction;
        }
        /*else if(this.transform.position.y >= 350)
        {
            Destroy(GameObject.Find("Tiles"));
        }*/
    }
}
