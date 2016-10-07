using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class FourthChoice : MonoBehaviour {
    List<string> fourthChoice = new List<string>() { "Kópavogur", "17. júní 1944", "579m", "Gunnar Egill Ágústsson", "Íslensku" };

    // Use this for initialization
    void Start()
    {
      //  GetComponent<TextMesh>().text = fourthChoice[0];
    }

    // Update is called once per frame
    void Update () {
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = fourthChoice[TextControl.randQuestions];
        }

    }

    void OnMouseDown()
    {
        TextControl.selectedAnswer = gameObject.name;
        TextControl.choiceSelected = "y";
        //Debug.Log(gameObject.name);
    }
}
