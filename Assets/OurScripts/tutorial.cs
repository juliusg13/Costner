using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour {
    GameObject img, tex;
    float x, y;
    // Use this for initialization
    void Start() {
        img = GameObject.Find("Canvas/tutorialImage");
        tex = GameObject.Find("Canvas/tutorialImage/tutorialText");
        x = Screen.width;
        y = Screen.height;
        initWindow();
    }


    void initWindow() {
        //img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        //img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);

        //tex.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x * 0.4f);
        //tex.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y * 0.9f);
    }
    public void disableTutorial() {
        img.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }
}
