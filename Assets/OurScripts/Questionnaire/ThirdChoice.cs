using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class ThirdChoice : MonoBehaviour {

    List<string> thirdChoice = new List<string>() { "Bessastaðir" , "12. júní 1978", "Um 900m", "Júlíus Gíslason", "Þýsku" };
    // Use this for initialization
    void Start()
    {
        //GetComponent<TextMesh>().text = thirdChoice[0];
    }


    // Update is called once per frame
    void Update () {
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = thirdChoice[TextControl.randQuestions];
        }

    }

    void OnMouseDown()
    {
        TextControl.selectedAnswer = gameObject.name;
        TextControl.choiceSelected = "y";
        //Debug.Log(gameObject.name);
    }
}
