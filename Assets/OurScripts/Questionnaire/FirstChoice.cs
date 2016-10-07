using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class FirstChoice : MonoBehaviour {

    List<string> firstChoice = new List<string>() { "Reykjavík", "20. ágúst 1800" , "Um 2000m", "Jón Jónsson", "Dönsku" };
    // Use this for initialization
    void Start()
    {
     //   GetComponent<TextMesh>().text = firstChoice[0];
    }


    // Update is called once per frame
    void Update () {
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = firstChoice[TextControl.randQuestions];
        }

    }


    void OnMouseDown()
    {
        TextControl.selectedAnswer = gameObject.name;
        TextControl.choiceSelected = "y";
        //Debug.Log(gameObject.name); 
    }
}
