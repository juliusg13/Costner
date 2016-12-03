using UnityEngine;
using System.Collections;

public class renderQuestionsByTags : MonoBehaviour {
    public bool cityTag, glacierTag, mountainTag, lakeRiverTag;
    public GameObject questionMarker;
    GameObject controller, world;
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
        cityTag = true;
        glacierTag = false;
        mountainTag = true;
        lakeRiverTag = false;
    }
    void setVariablesForCreatedQuestionGivers(GameObject thisQuestion, string qID, string tag, int i) {
        thisQuestion.GetComponent<quest>().questionID = qID;
        thisQuestion.transform.parent = world.transform;
        thisQuestion.transform.position = new Vector3(0, 15, 0);
      //  print("This baller was moved to " + thisQuestion.transform.position);
        thisQuestion.GetComponent<quest>().xCoord = float.Parse(controller.GetComponent<getJsonFromApi>().GetLatitude(qID));
        thisQuestion.GetComponent<quest>().zCoord = float.Parse(controller.GetComponent<getJsonFromApi>().GetLongitude(qID));
        thisQuestion.name = "QuestGiver:" + tag + i;
        thisQuestion.GetComponent<quest>().moveToLoc();
    }
    public void createByTag() {
        if (cityTag) {
            GameObject thisQuestion;
            string qID;
            for (int i = 0; i < controller.GetComponent<getJsonFromApi>().cities.Count; i++) {
                thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                qID = controller.GetComponent<getJsonFromApi>().cities.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                setVariablesForCreatedQuestionGivers(thisQuestion, qID, "cities", i);
            }
        }
        if (glacierTag) {
            GameObject thisQuestion;
            string qID;
            for (int i = 0; i < controller.GetComponent<getJsonFromApi>().glaciers.Count; i++) {
                thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                qID = controller.GetComponent<getJsonFromApi>().glaciers.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                setVariablesForCreatedQuestionGivers(thisQuestion, qID, "glaciers", i);
            }
        }
        if (mountainTag) {
            GameObject thisQuestion;
            string qID;
            for (int i = 0; i < controller.GetComponent<getJsonFromApi>().mountains.Count; i++) {
                thisQuestion = (GameObject)Instantiate(questionMarker, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                qID = controller.GetComponent<getJsonFromApi>().mountains.GetByIndex(i).ToString();
                thisQuestion.GetComponent<quest>().questGiver = thisQuestion;
                setVariablesForCreatedQuestionGivers(thisQuestion, qID, "mountains", i);
            }
        }
        if (lakeRiverTag) {
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