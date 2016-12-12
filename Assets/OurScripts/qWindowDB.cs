using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;

public class qWindowDB : MonoBehaviour {
    private GameObject controller, quest;
    private bool render, quitRender, skipRender, answer, correct;
    public bool answeredThisQuestionCorrectAlready;

    float x, y, qX, qY;
    private Rect windowRect, resultRect, questionButtonRect1, questionButtonRect2, questionButtonRect3, questionButtonRect4, quitRect, skipRect;
    private RectOffset qButtonRect;
    GUIStyle smallFont, centerTitle, centerText, questionText, questionOptions;
    string[] data;
    public int adventureCoins;
    private GameObject cam, randomQuestionWindow, levelsWindow, menuWindow;
    private string qID;

    // Use this for initialization
    void Start () {
        setGUIStyles();
        initializeVariables();
        centerRectangle();
        cam = GameObject.Find("Main Camera"); 
		rectAssemble ();
		controller = GameObject.FindWithTag("GameController");
        randomQuestionWindow = GameObject.Find("/Canvas/randomQuestion");
        levelsWindow = GameObject.Find("Canvas/settings");
        menuWindow = GameObject.Find("Canvas/levelsButton");
    }
    /// <summary>
    /// Function that sets basic variables initally.
    /// </summary>
    void initializeVariables() {
        render = false;
        quitRender = false;
        skipRender = false;
        answer = false;
        correct = false;
        answeredThisQuestionCorrectAlready = false;
    }

    /// <summary>
    /// data[5] includes the string that says which option was the correct option for specific question.
    /// Rewards currency if answered correct.
    /// </summary>
    /// <param name="s">s is an integer in the form of a string to compare correct answer with.</param>
    void Answer(string s){

        if (data[5] == s)
        {             //correct answer
            answer = true;
            correct = true;
            answeredThisQuestionCorrectAlready = true;
            controller.GetComponent<rewardSystem>().increaseCoins(adventureCoins);
            quest.GetComponent<quest>().changeColorCorrect();
            //controller.GetComponent<soundController>().questionUISound(2);
            //		Missing a function that makes sure we do not get the same question back up.
        }
        else {                          //wrong asnwer 
            answer = true;
            correct = false;
            quest.GetComponent<quest>().changeColorWrong();
            controller.GetComponent<soundController>().questionUISound(3);
        }
        StartCoroutine(PostRequest(correct, s));
    }


    /// <summary>
    /// Function that GETs all the data for the question on this specific question mark, includes question, possible answers and correct answer.
    /// </summary>
	void nextQuestion(string questionID){
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
        data = controller.GetComponent<getJsonFromApi>().getQuestionForm(questionID);
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
            GUI.color = new Color32(255, 255, 255, 0);
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        }
		if (quitRender){
			GUI.color = new Color(0.9f, 0.75f, 0.3f, 1f);
			quitRect = GUI.Window(1, quitRect, DoMyWindow, "");
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
            GUI.skin.button = questionOptions;
			GUI.TextField (new Rect (x * 0.03f, y * 0.07f, x * 0.75f, y * 0.25f), data[0], questionText);
            if (answeredThisQuestionCorrectAlready == true) {
                if(data[5] == "1") {
        
                    if(GUI.Button(questionButtonRect1, data[1])) {
                        HideWindow();
                    }
                } else if(data[5] == "2") {
                    if(GUI.Button(questionButtonRect2, data[2])) {
                        HideWindow();
                    }   
                } else if(data[5] == "3") {
                    if(GUI.Button(questionButtonRect3, data[3])){
                        HideWindow();
                    }
                } else if(data[5] == "4") {
                    if(GUI.Button(questionButtonRect4, data[4])) {
                       HideWindow();
                    }
                }

            }
            else {
                if (GUI.Button(questionButtonRect1, data[1])) {
                    render = false;
                    quitRender = false;
                    //HideWindow();
                    Answer("1");
                }
                if (GUI.Button(questionButtonRect2, data[2])) {
                    render = false;
                    quitRender = false;
                    //HideWindow();
                    Answer("2");
                }
                if (GUI.Button(questionButtonRect3, data[3])) {
                    render = false;
                    quitRender = false;
                    //HideWindow();
                    Answer("3");
                }
                if (GUI.Button(questionButtonRect4, data[4])) {
                    render = false;
                    quitRender = false;
                    //HideWindow();
                    Answer("4");
                }
            }
        }

		if (windowID == 1){
            if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Svara seinna", smallFont)) {
                render = false;
                quitRender = false;
            }
		}
		if (windowID == 2) {    
            if (GUI.Button(new Rect(x * 0.018f, y * 0.05f, x * 0.04f, y * 0.08f), "Smelltu hér", smallFont)) {
                render = false;
                quitRender = false;
            }
		}
		if (windowID == 3) {
            if (correct) GUI.TextField (new Rect ((qX/4), (qY/14), x*0.20f, y*0.15f), "Rétt hjá þér, vel gert", centerTitle);
            else if (!correct) GUI.TextField (new Rect ((qX / 8), (qY / 14), x * 0.4f, y * 0.15f), "Rangt hjá þér! Reyndu aftur", centerTitle); //centertitle
            if (GUI.Button(new Rect(qX/7, qY/3, x*0.4f, y*0.1f), "Halda áfram")){

                HideWindow(); //Used to be next question.

                //nextQuestion("nextQuestion");
            }
		}

	}

    /// <summary>
    /// json string with all the information about the students answer sent to costner api
    /// </summary>
    IEnumerator PostRequest(bool sCorrect, string studentAnswer)
    {
        WWWForm form = new WWWForm();
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        string jsonStr = "{\"studentId\":\"57d6fbde879baa8c33bc58fa\",\"applicationId\":\"3685211157\",\"applicationName\":\"GeoGame\",\"questionId\":" + qID + ", \"questionTitle\": \"" + data[0] + "\",\"levelId\":\"123563\", \"levelName\":\"Borgir\",\"answerCorrect\": \"" + sCorrect.ToString().ToLower() + "\",\"studentsAnswer\": \"" + data[Convert.ToInt32(studentAnswer)] + "\",\"correctAnswer\": \"" + data[Convert.ToInt32(data[5])] + "\", \"answerDescription\":\"123\"}";
        //               "{\"studentId\":\"57d6fbde879baa8c33bc58fa\",\"applicationId\":\"3685211157\",\"applicationName\":\"GeoGame\",\"questionId\":\"2468539\", \"questionTitle\":\"Hver er höfuðborg Íslands\",\"levelId\":\"123563\", \"levelName\":\"Borgir\",\"answerCorrect\": \"true\",\"studentsAnswer\": \"Reykjavík\", \"correctAnswer\": \"Reykjavík\", \"answerDescription\":\"123\"}";
        var formData = Encoding.UTF8.GetBytes(jsonStr);
        WWW www = new WWW("https://postman.api.costner.is/answers", formData, headers);
        yield return www;
        print(www.text);
    }

    /// <summary>
    /// sets the booleans in order for the windows to go from invisible to visible on screen.
    /// </summary>
	public void ShowWindow(string questionID, GameObject questGiver){
        cam.GetComponent<mouseDrag>().enabled = false;
        qID = questionID;
        quest = questGiver;
        nextQuestion (questionID);
		render = true;
		quitRender = true;
//		skipRender = true;
        hideUIButtons(false);
        controller.GetComponent<soundController>().questionUISound(0);
	}
    /// <summary>
    /// sets the booleans to false to make visible windows invisible on screen.
    /// </summary>
	public void HideWindow(){
        cam.GetComponent<mouseDrag>().enabled = true;
        render = false;
		quitRender = false;
		skipRender = false;
		answer = false;
		correct = false;
        hideUIButtons(true);
        controller.GetComponent<soundController>().questionUISound(1);
	}
    private void hideUIButtons(bool setBool) {
        randomQuestionWindow.SetActive(setBool);
        levelsWindow.SetActive(setBool);
        menuWindow.SetActive(setBool);
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
		resultRect = new Rect((qX/2) - x*0.2f, (qY/2) - y*0.1f, x*0.6f, y*0.4f);
        questionButtonRect1 = new Rect(x * 0.03f, y * 0.37f, x * 0.35f, y * 0.2f);
        questionButtonRect2 = new Rect(x * 0.42f, y * 0.37f, x * 0.35f, y * 0.2f);
        questionButtonRect3 = new Rect(x * 0.03f, y * 0.59f, x * 0.35f, y * 0.2f);
        questionButtonRect4 = new Rect(x * 0.42f, y * 0.59f, x * 0.35f, y * 0.2f);
        quitRect = new Rect((x*0.0007f), (y * 0.07f), (x / 10), (y / 10));
		skipRect = new Rect((x * 0.0007f), (y * 0.9f), (x / 10), (y / 10));
        qButtonRect = new RectOffset();
	}
    /// <summary>
    /// Helper function that creates various GUI styles for each element that needs one.
    /// </summary>
    private void setGUIStyles() {
        smallFont = new GUIStyle();
        centerText = new GUIStyle();
        centerTitle = new GUIStyle();
        questionText = new GUIStyle();
        //renderWindow = new GUIStyle();
        questionOptions = new GUIStyle();
      
        smallFont.fontSize = 15;

        centerText.alignment = TextAnchor.MiddleCenter;
        centerText.fontSize = 25;

        centerTitle.alignment = TextAnchor.MiddleCenter;
        centerTitle.fontSize = 35;

        questionText.fontSize = 25;
        questionText.alignment = TextAnchor.MiddleCenter;
        // renderWindow.normal.background

        questionOptions.fontSize = 25;
        questionOptions.alignment = TextAnchor.MiddleCenter;
        //questionOptions.border = qButtonRect;
    }
}
