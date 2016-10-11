using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class SecondChoice : MonoBehaviour {

    List<string> secondChoice = new List<string>() { "Hafnarfjörður", "18. ágúst 1786", "Um 300m ", "Ingólfur Arnarson", "Ensku" };
    // Use this for initialization
    void Start()
    {
        //GetComponent<TextMesh>().text = secondChoice[0];
    }


    // Update is called once per frame
    void Update () {
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = secondChoice[TextControl.randQuestions];
        }
    }

    void OnMouseDown()
    {
        TextControl.selectedAnswer = gameObject.name;
        TextControl.choiceSelected = "y";
        //Debug.Log(gameObject.name);
    }
}
