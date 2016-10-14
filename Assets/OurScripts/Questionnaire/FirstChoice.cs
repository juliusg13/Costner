using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class FirstChoice : MonoBehaviour {
    public int questionID;
	List<string> questions = new List<string>() { "Hvað heitir höfuðborg Íslands?", "Hvenær fékk Reykjavík kaupstaðarréttindi?", "Hversu há er Esjan?",  "Hver var fyrsti landnámsmaður Íslands?", "Hvert er móðurmál Íslendinga?" };
    List<string> firstChoice = new List<string>() { "Reykjavík", "20. ágúst 1800" , "Um 2000m", "Jón Jónsson", "Dönska" };
    List<string> secondChoice = new List<string>() { "Hafnarfjörður", "18. ágúst 1786", "Um 300m ", "Ingólfur Arnarson", "Enska" };
    List<string> thirdChoice = new List<string>() { "Bessastaðir", "12. júní 1978", "Um 900m", "Júlíus Gíslason", "Þýska" };
    List<string> fourthChoice = new List<string>() { "Kópavogur", "17. júní 1944", "579m", "Gunnar Egill Ágústsson", "Íslenska" };

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
            questionID = 1;
        }
/*        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = secondChoice[TextControl.randQuestions];
            questionID = 2;
        }
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = thirdChoice[TextControl.randQuestions];
            questionID = 3;
        }
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = fourthChoice[TextControl.randQuestions];
            questionID = 4;
        }*/
    }


    void OnMouseUp()
    {
        TextControl.selectedAnswer = gameObject.name;
        TextControl.choiceSelected = "y";
        //Debug.Log(gameObject.name); 
    }
}
