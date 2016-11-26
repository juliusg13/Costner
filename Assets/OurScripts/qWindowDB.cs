﻿using UnityEngine;
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
    GUIStyle smallFont, centerTitle, centerText, questionText;
	int questionTracker, correctCount;
	string[] data;
    int counter;

	// Use this for initialization
	void Start () {
        counter = 0;
        setGUIStyles();
        centerRectangle();
		correctCount = 0;

		rectAssemble ();
		controller = GameObject.FindWithTag("GameController");
		questionTracker = -1;
	}
    /// <summary>
    /// data[5] includes the string that says which option was the correct option for specific question.
    /// Rewards currency if answered correct.
    /// </summary>
    /// <param name="s">s is an integer in the form of a string to compare correct answer with.</param>
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
    /// <summary>
    /// Function that GETs all the data for the question on this specific question mark, includes question, possible answers and correct answer.
    /// </summary>
	void nextQuestion(){
		answer = false;
		correct = false;
        /*  data = new string[] {
                                  "Hver er höfuðborg Íslands?",
                                  "Bangkok",
                                  "Jerúsalem",
                                  "Selfoss",
                                  "Reykjavík",
                                  "4"
          };*/

        if (counter == 0) {
            data = controller.GetComponent<getJsonFromApi>().getQuestionForm("1234");
            counter++;
        } else if(counter == 1) {
            data = controller.GetComponent<getJsonFromApi>().getQuestionForm("1235");
            counter++;
        }
        else if (counter == 2) {
            data = controller.GetComponent<getJsonFromApi>().getQuestionForm("1236");
        }
    }

	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// OnGUI is a built in function.
    /// All rendering are already done when game is loaded, if the render booleans are set to true, the window will appear.
    /// render is the default question view
    /// quitRender is the "return back to map" button
    /// skipRender is the button to skip specific question if there are multiple questions on spot.
    /// answer and correct will create a different colored result window
    /// answer will create a negative colored result window.
    /// 
    /// Sizes are always relative to master window.
    /// </summary>
	void OnGUI(){
	//	GUI.skin.textField.fontSize = 30;
	//	GUI.skin.button.fontSize = 30;
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
    /// <summary>
    /// this is a continuous function like OnGUI.
    /// 
    /// Do my window creates specifics for the OnGUI function.
    /// For example render which has the window ID 0, is listed here,
    /// it will create sub windows for the main question window which include the questions in the form of "buttons"
    /// textfield is the question itself to be shown.
    /// all four buttons are created each with individual answers, data[1,2,3,4].
    /// 
    /// Since the windows are already created, the windowID 1 and 2 just add buttons on top of the window to skip/return to map.
    /// lastly windowid 3 handles different outcomes of the answers the user does.
    /// </summary>
    /// <param name="windowID">windowID is the window we are rendering at that point of time.</param>
	void DoMyWindow(int windowID){
		
		if (windowID == 0) {
			GUI.TextField (new Rect (x * 0.03f, y * 0.07f, x * 0.75f, y * 0.25f), data[0], questionText);

            if (GUI.Button(new Rect(x * 0.03f, y * 0.37f, x * 0.35f, y * 0.2f), data[1])) {
                Answer("1");
            }
			if (GUI.Button (new Rect (x * 0.42f, y * 0.37f, x * 0.35f, y * 0.2f), data[2])) { 
				Answer ("2");
            }
            if (GUI.Button (new Rect (x * 0.03f, y * 0.59f, x * 0.35f, y * 0.2f), data[3])) { 
				Answer ("3");
            }
            if (GUI.Button (new Rect (x * 0.42f, y * 0.59f, x * 0.35f, y * 0.2f), data[4])) {
                Answer ("4");
            }
        }

		if (windowID == 1){
            if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Smelltu hér", smallFont)) {
                HideWindow();
            }
		}
		if (windowID == 2) {
            if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Smelltu hér", smallFont)) {
                nextQuestion();
            }
		}
		if (windowID == 3) {
			if (correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rétt hjá þér, vel gert", centerTitle);
			else if (!correct) GUI.TextField (new Rect (100, 20, 600, 50), "Rangt hjá þér!!! Reyndu aftur eftir augnablik", centerTitle);
            if (GUI.Button(new Rect(100, 100, 600, 75), "Halda áfram", centerText)){
                //HideWindow(); //Used to be next question.
                nextQuestion();
            }
		}

	}
    /// <summary>
    /// sets the booleans in order for the windows to go from invisible to visible on screen.
    /// </summary>
	public void ShowWindow(){
		nextQuestion ();
		render = true;
		quitRender = true;
		skipRender = true;
	}
    /// <summary>
    /// sets the booleans to false to make visible windows invisible on screen.
    /// </summary>
	public void HideWindow(){
		render = false;
		quitRender = false;
		skipRender = false;
		answer = false;
		correct = false;
	}
    /// <summary>
    /// Dummy function just to gather the data for screen sizes.
    /// </summary>
	private void centerRectangle(){
		x = Screen.width;
		y = Screen.height;

        qX = x * 0.8f;
        qY = y * 0.8f;
    }
    /// <summary>
    /// Function that sets the window sizes for the window renderings.
    /// Sizes are relative to master window.
    /// </summary>
	private void rectAssemble(){
		windowRect = new Rect(x*0.1f, y*0.1f, qX, qY);
		resultRect = new Rect((qX/2) - x*0.2f, (qY/2) - y*0.1f, x*0.6f, y*0.25f);
		quitRect = new Rect((x*0.007f), (y * 0.007f), (x / 10), (y / 10));
		skipRect = new Rect((x * 0.007f), (y * 0.9f), (x / 10), (y / 10));
	}
    /// <summary>
    /// Helper function that creates various GUI styles for each element that needs one.
    /// </summary>
    private void setGUIStyles() {
        smallFont = new GUIStyle();
        centerText = new GUIStyle();
        centerTitle = new GUIStyle();
        questionText = new GUIStyle();

        smallFont.fontSize = 15;

        centerText.alignment = TextAnchor.MiddleCenter;
        centerText.fontSize = 25;

        centerTitle.alignment = TextAnchor.MiddleCenter;
        centerTitle.fontSize = 40;

        questionText.fontSize = 25;
        questionText.alignment = TextAnchor.MiddleCenter;
    }
}