using UnityEngine;
using System.Collections;

public class questionWindow : MonoBehaviour {
    private bool render = false;
	private bool questionListEmpty = false;
	private Rect resultRect;
	private GameObject controller;
	string[] data;
	private bool answer = false;
	private bool correct = false;
	int questionTracker;

    float x, y, qX, qY;
    private Rect windowRect;
    // Use this for initialization
    void Start () {
        centerRectangle();
        qX = x * 0.8f;
        qY = y * 0.8f;
        windowRect = new Rect(x*0.1f, y*0.1f, qX, qY);
		resultRect = new Rect((qX/2) - 225, (qY/2) - 75, 800, 200);
		controller = GameObject.FindWithTag("GameController");
		questionTracker = 0;
		NextQuestion ();

		//stringToEdit = data [0];
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
			GUI.color = new Color (0.1f, 0.25f, 0.7f, 1f);
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Spurning: ");
        }
		if (answer && correct) {
			GUI.color = new Color (0.1f, 1f, 0.1f, 1f);
			resultRect = GUI.Window (1, resultRect, DoMyWindow, "Svar");
		} else if(answer) {
			GUI.color = Color.red;
			resultRect = GUI.Window(1, resultRect, DoMyWindow, "Svar");
		}

    }
    void DoMyWindow(int windowID){
		if (windowID == 0) {
			GUI.TextField (new Rect (x * 0.03f, y * 0.07f, x * 0.75f, y * 0.25f), data [0]);
			if (GUI.Button (new Rect (x * 0.03f, y * 0.37f, x * 0.35f, y * 0.2f), data [1]))
				Answer (1);
			if (GUI.Button (new Rect (x * 0.42f, y * 0.37f, x * 0.35f, y * 0.2f), data [2]))
				Answer (2);
			if (GUI.Button (new Rect (x * 0.03f, y * 0.59f, x * 0.35f, y * 0.2f), data [3]))
				Answer (3);
			if (GUI.Button (new Rect (x * 0.42f, y * 0.59f, x * 0.35f, y * 0.2f), data [4]))
				Answer (4);
		}
		if (windowID == 1) {
			if (correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rétt hjá þér, vel gert");
			if (!correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rangt hjá þér!!! Þetta var ÖÖÖÖMURLEGT");
			if (GUI.Button (new Rect (100, 100, 600, 75), "Halda áfram fyrir næstu spurningu")) NextQuestion ();
		}
        
    }
	void Answer(int i){
		int ans = int.Parse (data [5]);
		if (ans == i) {			//correct answer
			answer = true;
			correct = true;
		} else {							//wrong asnwer 
			answer = true;
			correct = false;
		}
	}

	void NextQuestion(){
		data = controller.GetComponent<fakeDatabase> ().returnData (questionTracker);
		if (questionTracker == 4) {
			questionTracker = 0;
			questionListEmpty = true;
			render = false;
		} else questionTracker++;

		answer = false;
		correct = false;
	}
}
