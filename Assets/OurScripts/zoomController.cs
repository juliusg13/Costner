using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapzenGo.Models;

public class zoomController : MonoBehaviour {
    private GameObject world, minusBut, plusBut, slid, cam, zoomParent;


	// Use this for initialization
	void Start () {
        world = GameObject.Find("World");
        plusBut = GameObject.Find("Canvas/zoomParent/plusButton");
        minusBut = GameObject.Find("Canvas/zoomParent/minusButton");
        slid = GameObject.Find("Canvas/zoomParent/Slider");
        cam = GameObject.FindWithTag("MainCamera");
        zoomParent = GameObject.Find("Canvas/zoomParent");
        print(plusBut);
        print(slid);
        slid.GetComponent<UnityEngine.UI.Slider>().value = world.GetComponent<CachedDynamicTileManager>().Zoom;
        //displayZoomChildren(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(world == null)
        {
            world = GameObject.Find("World");
        }
	}

    public void plusFunc()
    {
        if(world.GetComponent<CachedDynamicTileManager>().Zoom < 16)
        {
            cam.GetComponent<mouseDrag>().ZoomPlus(world);
            slid.GetComponent <UnityEngine.UI.Slider>().value ++;
        }
    }

    public void minusFunc()
    {
        if (world.GetComponent<CachedDynamicTileManager>().Zoom > 3)
        {
            cam.GetComponent<mouseDrag>().ZoomMinus(world);
            slid.GetComponent<UnityEngine.UI.Slider>().value--;
        }
    }

    public void displayZoomChildren(bool show)
    {
        plusBut = GameObject.Find("Canvas/zoomParent/plusButton");
        minusBut = GameObject.Find("Canvas/zoomParent/minusButton");
        slid = GameObject.Find("Canvas/zoomParent/Slider");
        slid.SetActive(show);
        plusBut.SetActive(show);
        minusBut.SetActive(show);
    }
}
