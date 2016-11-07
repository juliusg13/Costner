using UnityEngine;
using System.Collections;

public class questionWindow : MonoBehaviour {
    private bool render = false;
    private bool quitRender = false;
    private bool skipRender = false;
	private bool questionListEmpty = false;
	private bool answer = false;
	private bool correct = false;

	private GameObject controller;
	string[] data;

	int questionTracker, ans, correctCount;
	float x, y, qX, qY;
	private Rect windowRect, resultRect, quitRect, skipRect;
    GUIStyle smallFont = new GUIStyle();
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
		NextQuestion ();
        
    } 
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ShowWindow(){
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
        if (windowID == 1){
            
           // if (GUI.Button(new Rect(x * 0.007f, y * 0.007f, x * 0.05f, y * 0.05f), "Smelltu hér")) HideWindow();
            if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Smelltu hér", smallFont)) HideWindow();
        }
        if (windowID == 2) {
            if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Smelltu hér", smallFont)) NextQuestion();

        }
		if (windowID == 3) {
			if (correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rétt hjá þér, vel gert");
			else if (!correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rangt hjá þér!!! Þetta var ÖÖÖÖMURLEGT");
			if (GUI.Button (new Rect (100, 100, 600, 75), "Halda áfram fyrir næstu spurningu")) NextQuestion ();
		}
        
    }
	void Answer(int i){
		ans = int.Parse (data [5]);
		if (ans == i) {						//correct answer
			answer = true;
			correct = true;
			correctCount++;
            controller.GetComponent<rewardSystem>().increaseCoins(10);
			controller.GetComponent<fakeDatabase> ().changeAnsweredQs (questionTracker);
		} else {							//wrong asnwer 
			answer = true;
			correct = false;
		}
	}

	void NextQuestion(){
		answer = false;
		correct = false;
		questionTracker++;
		if (questionTracker == 2) {
			HideWindow ();
			questionTracker = 0;

		}
		data = controller.GetComponent<fakeDatabase> ().returnData (questionTracker);
		if (data [7] == "1") {
			if (correctCount == 5) {
				HideWindow ();
				GameObject quest = GameObject.FindGameObjectWithTag("QuestGiver");
				Destroy (quest);
			} else
			NextQuestion ();
		}
	}
	private void rectAssemble(){
		windowRect = new Rect(x*0.1f, y*0.1f, qX, qY);
		resultRect = new Rect((qX/2) - 225, (qY/2) - 75, 800, 200);
		quitRect = new Rect((x*0.007f), (y * 0.007f), (x / 10), (y / 10));
		skipRect = new Rect((x * 0.007f), (y * 0.9f), (x / 10), (y / 10));
	}
}
