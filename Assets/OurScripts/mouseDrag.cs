using UnityEngine;
using System;
using MapzenGo.Models;

public class mouseDrag : MonoBehaviour {
    //Changes object position when mouse is dragged
    GameObject qParent, slid;
    GameObject worldMapQParent;
    public double dragSpeed = 10;
    private float andDragSpeed = 1;
    public float scrollSpeed = 10;
    public int maxZoom = 230;
    public int minZoom = 90;
    public int startPos = 160;
    public GameObject world;
    float dragH, dragV; //Drag horizontal and vertical
    float d;
    Vector3 newZoom;

    void Start() {
        newZoom = transform.position;
        worldMapQParent = GameObject.Find("worldMAPQParent");
        slid = GameObject.Find("Canvas/zoomParent/Slider");
    }

    void Update() {
        if (world == null) {
            world = GameObject.Find("World");
        }
        if (Application.platform == RuntimePlatform.Android) {
            AndroidDrag();
            AndroidZoom();
        }
        else {
            /*LEFT CLICK DRAG*/
            //If left click
            if (Input.GetMouseButton(0)) {
                dragH = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse X");
                dragV = Convert.ToInt32(dragSpeed) * -Input.GetAxis("Mouse Y");
                this.gameObject.transform.Translate(dragH, dragV, 0);
            }

            /*SCROLL UP AND DOWN*/
            d = Input.GetAxis("Mouse ScrollWheel"); //Detect scroll wheel 

            //If scroll up
            if (d < 0f && newZoom.y < maxZoom + 10) {
                newZoom.y = newZoom.y + scrollSpeed;
            }

            //If scroll down  
            else if (d > 0f && newZoom.y > minZoom - 10) {
                newZoom.y = newZoom.y - scrollSpeed;
            }

            //smooth zoom
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, newZoom.y, Time.deltaTime), transform.position.z);
        }

        //change zoom levels
        if (newZoom.y >= maxZoom && world.GetComponent<CachedDynamicTileManager>().Zoom > 3) {
            ZoomMinus(world);
            slid.GetComponent<UnityEngine.UI.Slider>().value--;
        }
        if (newZoom.y <= minZoom && world.GetComponent<CachedDynamicTileManager>().Zoom < 16) {
            ZoomPlus(world);
            slid.GetComponent<UnityEngine.UI.Slider>().value++;
        }
    }

    //If user scrolls down enough. We increase the zoom, clone the world and delete the original
    public void ZoomMinus(GameObject world) {
        GameObject oldWorld = world;
        oldWorld.GetComponent<CachedDynamicTileManager>().Zoom--;
        qParent = GameObject.Find("questionParent");
        qParent.transform.parent = worldMapQParent.transform;
        GameObject newWorld = Instantiate(oldWorld);

        qParent.transform.parent = newWorld.transform;
        Destroy(oldWorld);
        newWorld.name = "World";
        for (int i = newWorld.transform.childCount - 1; i > -1; i--) {
            if (newWorld.transform.GetChild(i).name == "Tiles") {
                Destroy(newWorld.transform.GetChild(i).gameObject);
            }
        }
        world = newWorld;
        setQuestionMarksActive();

        //reset camera y position
        Vector3 position = transform.position;
        position[1] = startPos;
        transform.position = position;
        newZoom.y = startPos;
    }

    //If user scrolls up enough. We decrease the zoom, clone the world and delete the original
    //y position and dragSpeed reset
    public void ZoomPlus(GameObject world) {
        GameObject oldWorld = world;
        oldWorld.GetComponent<CachedDynamicTileManager>().Zoom++;
        qParent = GameObject.Find("questionParent");
        qParent.transform.parent = worldMapQParent.transform;
        GameObject newWorld = Instantiate(oldWorld);
        qParent.transform.parent = newWorld.transform;
        Destroy(oldWorld);
        newWorld.name = "World";
        for (int i = newWorld.transform.childCount - 1; i > -1; i--) {
            if (newWorld.transform.GetChild(i).name == "Tiles") {
                Destroy(newWorld.transform.GetChild(i).gameObject);
            }
        }
        world = newWorld;
        setQuestionMarksActive();

        //reset camera y position
        Vector3 position = transform.position;
        position[1] = startPos;
        transform.position = position;
        newZoom.y = startPos;
    }
    public void setQuestionMarksActive() {
        if (world == null) {
            world = GameObject.Find("World");
        }
        if(qParent == null) {
            qParent = GameObject.Find("questionParent");
        }
        if (world.GetComponent<CachedDynamicTileManager>().Zoom < 14) {
            for (int i = qParent.transform.childCount - 1; i > -1; i--) {
                qParent.transform.GetChild(i).GetComponent<Animator>().enabled = false;
                qParent.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
                qParent.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            }
        }
        else if (world.GetComponent<CachedDynamicTileManager>().Zoom >= 14) {
            for (int i = qParent.transform.childCount - 1; i > -1; i--) {
                if (!(qParent.transform.GetChild(i).GetComponent<qWindowDB>().answeredThisQuestionCorrectAlready)) {
                    qParent.transform.GetChild(i).GetComponent<Animator>().enabled = true;
                }
                qParent.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                qParent.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void AndroidDrag() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * andDragSpeed, -touchDeltaPosition.y * andDragSpeed, 0);
        }
    }

    void AndroidZoom() {
        world = GameObject.Find("World");
        if (Input.touchCount == 2) {
            Touch touchZero = Input.GetTouch(0); //touch 1
            Touch touchOne = Input.GetTouch(1);  //touch 2

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            newZoom.y = (newZoom.y + (deltaMagnitudeDiff * 3));

            //zoom
            if (newZoom.y < maxZoom + 10 && newZoom.y > minZoom - 10) {
                transform.position = new Vector3(transform.position.x, newZoom.y, transform.position.z);
            }
        }
    }
}
