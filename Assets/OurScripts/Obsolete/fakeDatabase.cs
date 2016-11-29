using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class fakeDatabase : questionWindow {
	
	List<string> questions = new List<string>() { "Hvað heitir höfuðborg Íslands?", "Hvenær fékk Reykjavík kaupstaðarréttindi?", "Hversu há er Esjan?",  "Hver var fyrsti landnámsmaður Íslands?", "Hvert er móðurmál Íslendinga?" };
	List<string> firstChoice = new List<string>() { "Reykjavík", "20. ágúst 1800" , "Um 2000m", "Jón Jónsson", "Danska" };
	List<string> secondChoice = new List<string>() { "Hafnarfjörður", "18. ágúst 1786", "Um 300m ", "Ingólfur Arnarson", "Enska" };
	List<string> thirdChoice = new List<string>() { "Bessastaðir", "12. júní 1978", "Um 900m", "Júlíus Gíslason", "Þýska" };
	List<string> fourthChoice = new List<string>() { "Kópavogur", "17. júní 1944", "579m", "Gunnar Egill Ágústsson", "Íslenska" };
	List<string> correctAnswer = new List<string>() { "1", "2", "3", "2", "4"};
	List<string> answeredQuestions = new List<string>() { "0", "0", "0", "0", "0"};
	List<string> questionID = new List<string> (){ "0", "1", "2", "3", "4", "5" };
	// Use this for initialization

	public string[] returnData(int ID){
		string[] arr = new string[] {
			questions[ID],
			firstChoice[ID],
			secondChoice[ID],
			thirdChoice[ID],
			fourthChoice[ID],
			correctAnswer[ID],
			questionID[ID],
			answeredQuestions[ID]
		};
		return arr;
	}
	public void changeAnsweredQs(int Id){
		//string ID = int.Parse (Id); // string = string to int lol
		//string ID = Id.ToString();
		answeredQuestions [Id] = "1";
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
