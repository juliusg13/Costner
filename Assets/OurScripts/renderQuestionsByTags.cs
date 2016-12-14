using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class renderQuestionsByTags : MonoBehaviour {
    public bool cityTag, glacierTag, mountainTag, lakeRiverTag;
    private bool cityAlreadyRendered, glacierAlreadyRendered, mountainAlreadyRendered, lakeRiverAlreadyRendered;
    public GameObject questionMarker;
    GameObject controller, world, qparent;
    public SortedList allQuestionsOnMap;
    int llCounter;
    // Use this for initialization
    void Start() {
        controller = GameObject.FindWithTag("GameController");
        world = GameObject.FindWithTag("OpenWorld");
        initFunction();
        //     Invoke("createByTag", 5);
    }

    // Update is called once per frame
    void Update() {

    }
    void initFunction() {
        cityTag = false;
        //cityAlreadyRendered = true;
        glacierTag = false;
        mountainTag = false;
        lakeRiverTag = false;
        allQuestionsOnMap = new SortedList();
        llCounter = 0;
    }
    public void setBoolean(string tag) {
        //print(tag);
        if (tag == "cityTag") {
            cityTag = true;
            createByTag();
            cityAlreadyRendered = true;
        }
        if (tag == "glacierTag") {
            glacierTag = true;
            createByTag();
            glacierAlreadyRendered = true;
        }
        if (tag == "mountainTag") {
            mountainTag = true;
            createByTag();
            mountainAlreadyRendered = true;
        }
        if (tag == "lakeRiverTag") {
            lakeRiverTag = true;
            createByTag();
            lakeRiverAlreadyRendered = true;
        }

    }
    void setVariablesForCreatedQuestionGivers(GameObject thisQuestion, string qID, string tag, int i) {
        world = GameObject.Find("World");
        qparent = GameObject.Find("questionParent");
        thisQuestion.GetComponent<quest>().questionID = qID;
        thisQuestion.transform.parent = qparent.transform;
        thisQuestion.transform.position = new Vector3(0, 15, 0);
        //  print("This baller was moved to " + thisQuestion.transform.position);
        thisQuestion.GetComponent<quest>().xCoord = float.Parse(controller.GetComponent<getJsonFromApi>().GetLatitude(qID));
        thisQuestion.GetComponent<quest>().zCoord = float.Parse(controller.GetComponent<getJsonFromApi>().GetLongitude(qID));
        thisQuestion.name = "QuestGiver:" + tag + i;
        thisQuestion.GetComponent<quest>().moveToLoc();
        allQuestionsOnMap.Add(llCounter, thisQuestion);
        llCounter++;
    }
    public void createByTag() {
        if (cityTag && !cityAlreadyRendered) {
            GameObject thisQuestion;
            string qID;
            for (int i = 0; i < controller.GetComponent<getJsonFromApi>().cities.Count; i++) {
                thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                qID = controller.GetComponent<getJsonFromApi>().cities.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                setVariablesForCreatedQuestionGivers(thisQuestion, qID, "cities", i);
            }
        }
        if (glacierTag && !glacierAlreadyRendered) {
            GameObject thisQuestion;
            string qID;
            for (int i = 0; i < controller.GetComponent<getJsonFromApi>().glaciers.Count; i++) {
                thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                qID = controller.GetComponent<getJsonFromApi>().glaciers.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                setVariablesForCreatedQuestionGivers(thisQuestion, qID, "glaciers", i);
            }
        }
        if (mountainTag && !mountainAlreadyRendered) {
            GameObject thisQuestion;
            string qID;
            for (int i = 0; i < controller.GetComponent<getJsonFromApi>().mountains.Count; i++) {
                thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                qID = controller.GetComponent<getJsonFromApi>().mountains.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                setVariablesForCreatedQuestionGivers(thisQuestion, qID, "mountains", i);
            }
        }
        if (lakeRiverTag && !lakeRiverAlreadyRendered) {
            GameObject thisQuestion;
            string qID;
            for (int i = 0; i < controller.GetComponent<getJsonFromApi>().lakesRivers.Count; i++) {
                thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                qID = controller.GetComponent<getJsonFromApi>().lakesRivers.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                setVariablesForCreatedQuestionGivers(thisQuestion, qID, "lakesRivers", i);
            }
        }
    }

}