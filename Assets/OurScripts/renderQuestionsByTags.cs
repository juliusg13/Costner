using UnityEngine;
using System.Collections;

public class renderQuestionsByTags : MonoBehaviour {
    public bool cityTag, mountainTag, lakeRiverTag;
    public GameObject questionMarker;
    GameObject controller;
	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController");
        initFunction();
   //     Invoke("createByTag", 5);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    void initFunction() {
        cityTag = true;
        mountainTag = false;
        lakeRiverTag = false;
    }
    public void createByTag() {
        if (cityTag) {
            /*      foreach(DictionaryEntry qID in controller.GetComponent<getJsonFromApi>().cities) {
                      GameObject thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0 + i, 15, 1), transform.rotation);
                      Debug.Log(controller.GetComponent<getJsonFromApi>().cities);
                      i++;
                  }*/
            /*   for (int i = 0; i < 3; i++) {
                   GameObject thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0 + i, 15, 0), transform.rotation);
               }*/
            for(int i = 0; i < controller.GetComponent<getJsonFromApi>().cities.Count; i++) {
                GameObject thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 15, 5), Quaternion.Euler(new Vector3(90,0,0)));
                //thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                thisQuestion.GetComponent<quest>().questionID = controller.GetComponent<getJsonFromApi>().cities.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().xCoord = 15;
                thisQuestion.GetComponent<quest>().zCoord = 15;
                Debug.Log(controller.GetComponent<getJsonFromApi>().cities.GetByIndex(i));
            }
        }

    }
}
