using UnityEngine;
using System.Collections;
using simpleJSON;
using System;

public class getJsonFromApi : MonoBehaviour {

	private WWW www;
	private bool loaded = false;
    public SortedList mountains, glaciers, riversLakes, cities;

	void Start()
	{
        initLists();
		var url = "http://geogame.api.costner.is/";
		www = new WWW(url);
		StartCoroutine(SendRequest());
	}

	public IEnumerator SendRequest()
	{
		yield return www;
		if (www.error == null)
		{
			loaded = true;
			//Debug.Log ("loaded = true");
			//Debug.Log (GetQuestion ("1234"));
        }
		else
		{
			Debug.Log("WWW Error: " + www.error);
		}
	}

	private JSONNode Question(string questionID)
	{
		if (loaded == true)
		{
			var N = JSON.Parse(www.text);
            JSONArray arr = N["projects"].AsArray;
            bool didFind = false;
            string qID, tag;
            int retI = 0;
			for (int i = 0; i < arr.Count; i++) {
                qID = arr[i]["questionId"];
                tag = arr[i]["tag"];
                if (qID == questionID) {
                    retI = i;
                    didFind = true;
				}
                if(tag == "Borgir") {
                    cities.Add(i, qID);
                }
                if(tag == "Jöklar") {
                    glaciers.Add(i, qID);
                }
                if(tag == "Fjöll") {
                    mountains.Add(i, qID);
                }
                if(tag == "Ár og Vötn") {
                    riversLakes.Add(i, qID);
                }
			}
            if (didFind) {
                return arr[retI];
            }
		}
		return null;
	}

	///<summary>
	///<para>Returns the question with the questionID as string</para>
	///<returns>Returns: String</returns>
	///</summary>
	public string GetQuestion(string questionID)
	{
		var N = Question(questionID);
		if(N != null)
		{
			return N["questionTitle"];
		}
		Debug.LogError("Database not loaded");
		return "";
	}

	///<summary>
	///<para>Returns the answer from the position pos from the answerList as string</para>
	///<returns>Returns: String</returns>
	///</summary>
	public string GetAnswer(string questionID, int pos)
	{
		var N = Question(questionID);
		if (N != null)
		{
			return N["answerList"][pos];
		}
		Debug.LogError("Database not loaded");
		return "";
	}

	///<summary>>
	/// <para>Returns the list of all answers</para>
	/// <returns>Returns : String array</returns>
	/// </summary>
	public string[] getQuestionForm(string questionID){
		var N = Question (questionID);

		string[] arr = new string[] {
			N["questionTitle"],
			N["answerList"][0],
			N["answerList"][1],
			N["answerList"][2],
			N["answerList"][3],
			N["correctAnswer"]
		};
		return arr;
	}
    
	///<summary>
	///<para>Returns the latitude variable from the answerList as string</para>
	///<returns>Returns: String</returns>
	///</summary>
	public string GetLatitude(string questionID)
	{
		var N = Question(questionID);
		if (N != null)
		{
			return N["latitude"];
		}
		Debug.LogError("Database not loaded");
		return "";
	}

	///<summary>
	///<para>Returns the longitude variable from the answerList as string</para>
	///<returns>Returns: String</returns>
	///</summary>
	public string GetLongitude(string questionID)
	{
		var N = Question(questionID);
		if (N != null)
		{
			return N["longitude"];
		}
		Debug.LogError("Database not loaded");
		return "";
	}

	///<summary>
	///<para>Returns the true if the question excites in the answerList as bool</para>
	///<returns>Returns: Boolean</returns>
	///</summary>
	public bool IsQuestionID(string questionID)
	{
		var N = Question(questionID);
		if(N != null)
		{
			return true;
		}
		return false;
	}

	///<summary>
	///<para>Returns the correctAnswer variable from the answerList as Int</para>
	///<returns>Returns: Integer</returns>
	///</summary>
	public int GetCorrectAnswer(string questionID)
	{
		var N = Question(questionID);
		if (N != null)
		{
			return int.Parse(N["correctAnswer"]);
		}
		Debug.LogError("Database not loaded");
		return 0;
	}
    private void initLists() {
        mountains = new SortedList();
        glaciers = new SortedList();
        riversLakes = new SortedList();
        cities = new SortedList();
    }
}