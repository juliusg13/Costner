using UnityEngine;
using System.Collections;
using SimpleJSON;
using System;

public class GetJsonFromApi : MonoBehaviour {

	private WWW www;
	private bool loaded = false;

	void Start()
	{
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
			//Debug.Log (GetQuestion ("\"1234\""));
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
			for (int i = 0; i < N.Count; i++)
			{
				if (N["projects"][i]["questionId"].ToString() == questionID)
				{
					return N["projects"][i];
				}
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
			return N["questionTitle"].ToString ();
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
			return N["answerList"][pos].ToString();
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
			N["questionTitle"].ToString(),
			N["answerList"][0].ToString(),
			N["answerList"][1].ToString(),
			N["answerList"][2].ToString(),
			N["answerList"][3].ToString(),
			N["correctAnswer"].ToString()
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
			return N["latitude"].ToString();
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
			return N["longitude"].ToString();
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
}