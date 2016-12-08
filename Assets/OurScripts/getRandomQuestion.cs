using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getRandomQuestion : MonoBehaviour {
    GameObject controller, questionObject, camera;
    string questionName;
    int linkedListCount, random;
    string qID;
	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController");
        camera = GameObject.FindWithTag("MainCamera");
        linkedListCount = 0;
        random = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void findQuestion() {
        linkedListCount = controller.GetComponent<renderQuestionsByTags>().allQuestionsOnMap.Count;
        random = Random.Range(0, linkedListCount);
        while (!isValidQuestion(random)) {
            random = Random.Range(0, linkedListCount);
        }
        moveCamera(questionObject);
    }
    /// <summary>
    /// If the question has been answered already then it is invalid, hence return false.
    /// </summary>
    /// <param name="random">random number in the list of questions on map.</param>
    /// <returns></returns>
    bool isValidQuestion(int random) {
        questionObject = controller.GetComponent<renderQuestionsByTags>().allQuestionsOnMap.GetByIndex(random) as GameObject;
        //print(questionObject);

        if(questionObject.GetComponent<qWindowDB>().answeredThisQuestionCorrectAlready == true) {
            return false;
        }
        return true;
    }

    void moveCamera(GameObject q) {
        float x = q.gameObject.transform.position.x;
        float z = q.gameObject.transform.position.z;
        //float y = camera.gameObject.transform.position.y;
        camera.gameObject.transform.position = new Vector3(x, 160f, z);
    }
}
