using UnityEngine;
using System.Collections;
using System.Collections.Generic; //for lists

public class TextControl : MonoBehaviour {

    List<string> questions = new List<string>() { "Hvað heitir höfuðborg Íslands?", "Hvenær fékk Reykjavík kaupstaðarréttindi?", "Hversu há er Esjan?",  "Hver var fyrsti landnámsmaður Íslands?", "Hvert er móðurmál Íslendinga?" };
    

    List<string> correctAnswer = new List<string>() { "1", "2", "3", "2", "4"};

    public static int randQuestions = -1;
    public static string selectedAnswer;
    public static string choiceSelected = "n";
    public Transform resultObj; 
    // Use this for initialization
    void Start () {
      //  GetComponent<TextMesh>().text = questions[0];
	}

    // Update is called once per frame
    void Update()
    {
        if (randQuestions == -1)
        {
            randQuestions = Random.Range(0, 5);
        }
        if (randQuestions > -1)
        {
            GetComponent<TextMesh>().text = questions[randQuestions];
        }
        // Debug.Log(questions[randQuestions]);

        if (choiceSelected == "y")
        {
            choiceSelected = "n"; 
            if(correctAnswer[randQuestions] == selectedAnswer)
            {
                resultObj.GetComponent<TextMesh>().text = "Rétt hjá þér!"; 
                //Debug.Log("Correct!" + "    " + randQuestions); 
            }
            else
            {
                resultObj.GetComponent<TextMesh>().text = "Rangt! Reyndu aftur";
            }
        }
    }
}
