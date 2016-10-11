using UnityEngine;
using System;
using MapzenGo.Models;

public class MouseDrag : MonoBehaviour {
    //Changes object position when mouse is dragged
    
    [SerializeField] public double  dragSpeed           = 10;
    [SerializeField] public float   scrollSpeed         = 10;
    [SerializeField] public double  dragSpeedReduction  = 0.2;

    void Update()
    {
    /*LEFT CLICK DRAG*/

        float h = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse X");
        float v = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse Y");

        //If left click
        if (Input.GetMouseButton(0))
        {
            this.gameObject.transform.Translate(h, v, 0);
        }

        //Detect scroll wheel 
        var d = Input.GetAxis("Mouse ScrollWheel");
        
    /*SCROLL UP AND DOWN*/

        //If scroll up
        if (d > 0f && (this.transform.position.y > 70 || GameObject.Find("World").GetComponent<CachedDynamicTileManager>().Zoom < 16))
        {
            this.gameObject.transform.Translate(0, 0, scrollSpeed);
            dragSpeed = dragSpeed - dragSpeedReduction;
        }

        //If scroll down  
        else if (d < 0f && (this.transform.position.y < 340 || GameObject.Find("World").GetComponent<CachedDynamicTileManager>().Zoom > 3))
        {
            this.gameObject.transform.Translate(0, 0, -scrollSpeed);
            dragSpeed = dragSpeed + dragSpeedReduction;
        }

        //If user scrolls up enough. We change the decrease the zoom, clone the world and delete the original
        //y position and dragSpeed reset
        if (d < 0f && this.transform.position.y > 350)
        {
            GameObject oldWorld = GameObject.Find("World");
            oldWorld.GetComponent<CachedDynamicTileManager>().Zoom--;
            GameObject newWorld = Instantiate(oldWorld);
            Destroy(oldWorld);
            newWorld.name = "World";
            Destroy(newWorld.gameObject.transform.GetChild(2).gameObject);
            //reset camera y position
            Vector3 position = transform.position;
            position[1] = 300;
            transform.position = position;
            //reset dragSpeed
            dragSpeed = 10;

        }

        //If user scrolls down enough. We change the increase the zoom, clone the world and delete the original
        if (d > 0f && this.transform.position.y < 70)
        {
            GameObject oldWorld = GameObject.Find("World");
            oldWorld.GetComponent<CachedDynamicTileManager>().Zoom++;
            GameObject NewWorld = Instantiate(oldWorld);
            Destroy(oldWorld);
            NewWorld.name = "World";
            Destroy(NewWorld.gameObject.transform.GetChild(2).gameObject);
            //reset camera y position
            Vector3 position = transform.position;
            position[1] = 300;
            transform.position = position;
            //reset dragSpeed
            dragSpeed = 10;
        }
    }
}
