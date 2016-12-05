using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getRandomQuestion : MonoBehaviour {
    GameObject controller, questionObject;
    string questionName;
    int linkedListCount, random;
    string qID;
	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController");
        linkedListCount = 0;
        random = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void findQuestion() {
        linkedListCount = controller.GetComponent<renderQuestionsByTags>().allQuestionsOnMap.Count;
     
        random = Random.Range(0, linkedListCount);
        isValidQuestion(random);

    }
    bool isValidQuestion(int random) {
        questionObject = controller.GetComponent<renderQuestionsByTags>().allQuestionsOnMap.GetByIndex(random) as GameObject;
        print(questionObject);


        return false;
    }
}
