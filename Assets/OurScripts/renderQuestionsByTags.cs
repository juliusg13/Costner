﻿using UnityEngine;
using System.Collections;

public class renderQuestionsByTags : MonoBehaviour {
    public bool cityTag, mountainTag, lakeRiverTag;
    public GameObject questionMarker;
    GameObject controller, world;
	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController");
        initFunction();
        //world = GameObject.FindWithTag("OpenWorld");
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
            string qID;
         //   print("number of cities :" + controller.GetComponent<getJsonFromApi>().cities.Count);
            for(int i = 0; i < controller.GetComponent<getJsonFromApi>().cities.Count; i++) {
                //Debug.Log(i);
                GameObject thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 15, 0), Quaternion.Euler(new Vector3(90,0,0)));
                qID = controller.GetComponent<getJsonFromApi>().cities.GetByIndex(i).ToString();

                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                thisQuestion.GetComponent<quest>().questionID = qID;
                thisQuestion.GetComponent<quest>().xCoord = float.Parse(controller.GetComponent<getJsonFromApi>().GetLatitude(qID));
                thisQuestion.GetComponent<quest>().zCoord = float.Parse(controller.GetComponent<getJsonFromApi>().GetLongitude(qID));
                //thisQuestion.GetComponent<moveObjToTile>().world = world;
                //print("Lat " + controller.GetComponent<getJsonFromApi>().GetLatitude(qID));
                thisQuestion.GetComponent<quest>().MoveToLoc();
                // Debug.Log(controller.GetComponent<getJsonFromApi>().cities.GetByIndex(i));
            }
        }

    }
}
            /*      foreach(DictionaryEntry qID in controller.GetComponent<getJsonFromApi>().cities) {
                      GameObject thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0 + i, 15, 1), transform.rotation);
                      Debug.Log(controller.GetComponent<getJsonFromApi>().cities);
                      i++;
                  }*/
            /*   for (int i = 0; i < 3; i++) {
                   GameObject thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0 + i, 15, 0), transform.rotation);
               }*/
