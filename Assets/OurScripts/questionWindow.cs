using UnityEngine;
using System.Collections;

public class questionWindow : Quest {
    private bool render = false;
    public string stringToEdit = "How big is Audur's dick?";

    float x, y, qX, qY;
    private Rect windowRect;
    // Use this for initialization
    void Start () {
        centerRectangle();
        qX = x * 0.8f;
        qY = y * 0.8f;
        windowRect = new Rect(x*0.1f, y*0.1f, qX, qY);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ShowWindow(){
        render = true;
    }
    public void HideWindow(){
        render = false;
    }

    void centerRectangle(){
        x = Screen.width;
        y = Screen.height;
    }

    void OnGUI(){
        GUI.skin.textField.fontSize = 30;
        GUI.skin.button.fontSize = 30;
        if (render) {
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
        }

    }
    void DoMyWindow(int windowID){
        
        stringToEdit = GUI.TextField(new Rect(x*0.03f, y*0.07f, x*0.75f, y*0.25f), stringToEdit, 25);
        if (GUI.Button(new Rect(x*0.03f, y*0.37f, x*0.35f, y*0.2f), "10 millimeters")) Debug.Log("Answer 1");
        if (GUI.Button(new Rect(x*0.42f, y*0.37f, x*0.35f, y*0.2f), "100 micro-meters")) Debug.Log("Answer 2");
        if (GUI.Button(new Rect(x*0.03f, y*0.59f, x*0.35f, y*0.2f), "1000 nano-meters")) Debug.Log("Answer 3");
        if (GUI.Button(new Rect(x*0.42f, y*0.59f, x*0.35f, y*0.2f), "10000 pico-meters")) Debug.Log("Answer 4");
        
    }
}
