using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class menu : MonoBehaviour {
    GameObject menuImage, canv, notifyNoQuestions;
    float x, y;
	// Use this for initialization
	void Start () {
        menuImage = GameObject.Find("Canvas/settingsImage");
        canv = GameObject.Find("Canvas");
        initWindow();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openMenu() {
        menuImage.SetActive(true);
        canv.GetComponent<zoomController>().displayZoomChildren(false);
    }
    public void closeMenu() {
        menuImage.SetActive(false);
        canv.GetComponent<zoomController>().displayZoomChildren(true);
    }

    public void quitGame() {
#if UNITY_EDITOR
         if(UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
#endif
         if (Application.platform == RuntimePlatform.Android) {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        } else { 
            Application.Quit();
        }
    }
    void initWindow() {
        x = Screen.width;
        y = Screen.height;
        menuImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        menuImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);
        menuImage.SetActive(false);
    }
}
