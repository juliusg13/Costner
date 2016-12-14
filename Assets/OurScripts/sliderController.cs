using MapzenGo.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderController : MonoBehaviour {

    public Slider slide;
    public Button but;
    public GameObject world;
    private GameObject qParent;

    // Use this for initialization
    void Start() {
        slide.value = world.GetComponent<CachedDynamicTileManager>().Zoom;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (world == null) {
            world = GameObject.Find("World");
            slide.value = world.GetComponent<CachedDynamicTileManager>().Zoom;
        }
    }

    public void ButtonSelect() {
        GameObject oldWorld = world;
        oldWorld.GetComponent<CachedDynamicTileManager>().Zoom = (int)slide.value;
        qParent = GameObject.Find("questionParent");
        qParent.transform.parent = GameObject.Find("worldMAPQParent").transform;
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
        slide.value = world.GetComponent<CachedDynamicTileManager>().Zoom;
    }
}
