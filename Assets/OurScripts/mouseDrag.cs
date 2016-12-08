using UnityEngine;
using System;
using MapzenGo.Models;

public class mouseDrag : MonoBehaviour {
    //Changes object position when mouse is dragged
    public double  dragSpeed = 10;
    private float andDragSpeed = 1;
    public float   scrollSpeed = 10;
    public int maxZoom = 230;
    public int minZoom = 90;
    public int startPos = 160;
    public GameObject world;
    float dragH, dragV; //Drag horizontal and vertical
    float d;
    Vector3 newZoom;

    void Start()
    {
        newZoom = transform.position;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidDrag();
            return;
        }
        /*LEFT CLICK DRAG*/
        //If left click
        if (Input.GetMouseButton(0))
        {
            dragH = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse X");
            dragV = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse Y");
            this.gameObject.transform.Translate(dragH, dragV, 0);
        }

     /*SCROLL UP AND DOWN*/
        d= Input.GetAxis("Mouse ScrollWheel"); //Detect scroll wheel 
        //If scroll up
        if (d < 0f && newZoom.y < maxZoom + 10)
        {
            newZoom.y = newZoom.y + scrollSpeed;
        }

        //If scroll down  
        else if (d > 0f && newZoom.y > minZoom - 10)
        {
            newZoom.y = newZoom.y - scrollSpeed;
        }

        //smooth zoom
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, newZoom.y, Time.deltaTime), transform.position.z);

        //If user scrolls up enough. We decrease the zoom, clone the world and delete the original
        //y position and dragSpeed reset
        if (d < 0f && newZoom.y >= maxZoom && world.GetComponent<CachedDynamicTileManager>().Zoom > 3)
        {
            GameObject oldWorld = world;
            oldWorld.GetComponent<CachedDynamicTileManager>().Zoom--;
            GameObject newWorld = Instantiate(oldWorld);
            Destroy(oldWorld);
            newWorld.name = "World";
            for (int i = newWorld.transform.childCount - 1; i > -1; i--)
            {
                if (newWorld.transform.GetChild(i).name == "Tiles")
                {
                    Destroy(newWorld.transform.GetChild(i).gameObject);
                }
            }
            world = newWorld;
            //reset camera y position
            Vector3 position = transform.position;
            position[1] = startPos;
            transform.position = position;
            newZoom.y = startPos;
        }

        //If user scrolls down enough. We increase the zoom, clone the world and delete the original
        if (d > 0f && newZoom.y <= minZoom && world.GetComponent<CachedDynamicTileManager>().Zoom < 16)
        {
            GameObject oldWorld = world;
            oldWorld.GetComponent<CachedDynamicTileManager>().Zoom++;
            GameObject newWorld = Instantiate(oldWorld);
            Destroy(oldWorld);
            newWorld.name = "World";
            for (int i = newWorld.transform.childCount - 1; i > -1; i--)
            {
                if (newWorld.transform.GetChild(i).name == "Tiles")
                {
                    Destroy(newWorld.transform.GetChild(i).gameObject);
                }
            }
            world = newWorld;
            //reset camera y position
            Vector3 position = transform.position;
            position[1] = startPos;
            transform.position = position;
            newZoom.y = startPos;
        }
    }

    void AndroidDrag()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * andDragSpeed, -touchDeltaPosition.y * andDragSpeed, 0);
        }
    }
}
