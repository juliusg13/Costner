using UnityEngine;
using System.Collections;



public class qWindowDB : MonoBehaviour {
	private GameObject controller;
	private bool render = false;
	private bool quitRender = false;
	private bool skipRender = false;

	private bool answer = false;
	private bool correct = false;
	float x, y, qX, qY;
	private Rect windowRect, resultRect, quitRect, skipRect;
	GUIStyle smallFont = new GUIStyle();
	int questionTracker, correctCount;
	string[] data;
	// Use this for initialization
	void Start () {
		centerRectangle();
		correctCount = 0;
		qX = x * 0.8f;
		qY = y * 0.8f;
		smallFont.fontSize = 15;
		rectAssemble ();
		controller = GameObject.FindWithTag("GameController");
		questionTracker = -1;
	}

	void Answer(string s){
		if (data[5] == s) {             //correct answer
			answer = true;
			correct = true;
			controller.GetComponent<rewardSystem>().increaseCoins(10);
	//		Missing a function that makes sure we do not get the same question back up.
		} else {							//wrong asnwer 
			answer = true;
			correct = false;
		}
	}
	void nextQuestion(){
		answer = false;
		correct = false;

		data = controller.GetComponent<GetJsonFromApi> ().getQuestionForm ("1234");
	}

	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI(){
		
		GUI.skin.textField.fontSize = 30;
		GUI.skin.button.fontSize = 30;
		if (render) {
			GUI.color = new Color (0.1f, 0.25f, 0.7f, 1f);
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Spurning: ");
		}
		if (quitRender){
			GUI.color = new Color(0.9f, 0.75f, 0.3f, 1f);
			quitRect = GUI.Window(1, quitRect, DoMyWindow, "Fara aftur á kort");
		}
		if (skipRender) {
			skipRect = GUI.Window(2, skipRect, DoMyWindow, "Segja pass");
		}
		if (answer && correct) {
			GUI.color = new Color (0.1f, 1f, 0.1f, 1f);
			resultRect = GUI.Window (3, resultRect, DoMyWindow, "Svar");
		} else if(answer) {
			GUI.color = Color.red;
			resultRect = GUI.Window(3, resultRect, DoMyWindow, "Svar");
		}

	}
	void DoMyWindow(int windowID){
		
		if (windowID == 0) {
			GUI.TextField (new Rect (x * 0.03f, y * 0.07f, x * 0.75f, y * 0.25f), data[0]);
			if (GUI.Button (new Rect (x * 0.03f, y * 0.37f, x * 0.35f, y * 0.2f), data [1]))
				Answer ("0");
			if (GUI.Button (new Rect (x * 0.42f, y * 0.37f, x * 0.35f, y * 0.2f), data [2]))
				Answer ("1");
			if (GUI.Button (new Rect (x * 0.03f, y * 0.59f, x * 0.35f, y * 0.2f), data [3]))
				Answer ("2");
			if (GUI.Button (new Rect (x * 0.42f, y * 0.59f, x * 0.35f, y * 0.2f), data [4]))
				Answer ("3");
		}
		if (windowID == 1){
			if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Smelltu hér", smallFont)) HideWindow();
		}
		if (windowID == 2) {
			if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Smelltu hér", smallFont)) nextQuestion();
		}
		if (windowID == 3) {
			if (correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rétt hjá þér, vel gert");
			else if (!correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rangt hjá þér!!! Reyndu aftur eftir augnablik");
			if (GUI.Button (new Rect (100, 100, 600, 75), "Halda áfram fyrir næstu spurningu")) nextQuestion ();
		}

	}
	public void ShowWindow(){
		nextQuestion ();
		render = true;
		quitRender = true;
		skipRender = true;
	}
	public void HideWindow(){
		render = false;
		quitRender = false;
		skipRender = false;
		answer = false;
		correct = false;
	}

	private void centerRectangle(){
		x = Screen.width;
		y = Screen.height;
	}
	private void rectAssemble(){
		windowRect = new Rect(x*0.1f, y*0.1f, qX, qY);
		resultRect = new Rect((qX/2) - 225, (qY/2) - 75, 800, 200);
		quitRect = new Rect((x*0.007f), (y * 0.007f), (x / 10), (y / 10));
		skipRect = new Rect((x * 0.007f), (y * 0.9f), (x / 10), (y / 10));
	}
}
