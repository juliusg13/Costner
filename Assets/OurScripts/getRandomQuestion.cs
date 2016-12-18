using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapzenGo.Models;

public class getRandomQuestion : MonoBehaviour {
    GameObject controller, questionObject, cam, world, qParent, worldMapQParent, notifyNoMoreQuestions, notifyLevelUnlocked, canv;
    
    string questionName;
    int linkedListCount, random, zoom;
    List<string> alreadyAnsweredQ;
    public int numberOfNewWorldsDistance;
    float camX, camZ, x, z, absX, absZ, distance, maxDistance;
    string qID;
    bool alreadyShowedLevelNotification;
    // Use this for initialization
    void Start() {
        controller = GameObject.FindWithTag("GameController");
        cam = GameObject.FindWithTag("MainCamera");
        world = GameObject.FindWithTag("OpenWorld");
        worldMapQParent = GameObject.Find("worldMAPQParent");
        notifyNoMoreQuestions = GameObject.Find("notifyNoQuestions");
        notifyLevelUnlocked = GameObject.Find("notifyLevelUnlocked");
        canv = GameObject.Find("Canvas");
        alreadyShowedLevelNotification = false;
        linkedListCount = 0;
        random = 0;
        numberOfNewWorldsDistance = 2;
        displayNotifyNoQuestions(false);
        displayNotifyLevelUnlocked(false);

    }

    // Update is called once per frame
    void Update() {

    }
    public void findQuestion() {
        linkedListCount = controller.GetComponent<renderQuestionsByTags>().allQuestionsOnMap.Count;
        random = Random.Range(0, linkedListCount);
        alreadyAnsweredQ = new List<string>();
        while (!isValidQuestion(random)) {
            random = Random.Range(0, linkedListCount);
            if (alreadyAnsweredQ.Count == linkedListCount) {
                print("alreadyansweredq Count: " + alreadyAnsweredQ.Count.ToString() + " linked list count: " + linkedListCount);
                displayNotifyNoQuestions(true);
                return;
            }
        }
        moveCamera(questionObject);
    }
    public void displayNotifyNoQuestions(bool set) {
        notifyNoMoreQuestions.SetActive(set);
    }
    public void displayNotifyLevelUnlocked(bool set) {
        if (alreadyShowedLevelNotification == true) {
            return;
        }
        if (set == true) {
            alreadyShowedLevelNotification = true;
        }
        notifyLevelUnlocked.SetActive(set);
    }
    public void notifyLevelUnlockedFalse() {
        notifyLevelUnlocked.SetActive(false);
    }

    /// <summary>
    /// If the question has been answered already then it is invalid, hence return false.
    /// </summary>
    /// <param name="random">random number in the list of questions on map.</param>
    /// <returns></returns>
    bool isValidQuestion(int random) {
        questionObject = controller.GetComponent<renderQuestionsByTags>().allQuestionsOnMap.GetByIndex(random) as GameObject;
        //print(questionObject);

        if (questionObject.GetComponent<qWindowDB>().answeredThisQuestionCorrectAlready == true) {
            alreadyAnsweredQ.Add(questionObject.GetComponent<qWindowDB>().qID);
            return false;
        }
        return true;
    }
    /// <summary>
    /// q is the question marker found on map!
    /// </summary>
    /// <param name="q"></param>
    void moveCamera(GameObject q) {
        setVariables(q);


        //print("dist: " + distance);
        //print("maxdist : " + maxDistance);
        if (distance > maxDistance) {
            world.GetComponent<CachedDynamicTileManager>().Latitude = q.GetComponent<quest>().xCoord;
            world.GetComponent<CachedDynamicTileManager>().Longitude = q.GetComponent<quest>().zCoord;
            world.GetComponent<MapzenGo.Models.TileManager>().Zoom = 16;
            cam.gameObject.transform.position = new Vector3(world.transform.position.x, 160f, world.transform.position.z);
            createNewWorld();
            repositionQuestions();

        }
        else {
            cam.gameObject.transform.position = new Vector3(x, cam.transform.position.y, z);
            //world.GetComponent<MapzenGo.Models.TileManager>().Zoom = 16;
        }
        canv.GetComponent<zoomController>().updateSlider();
    }

    void createNewWorld() {
        GameObject oldWorld = world;
        oldWorld.GetComponent<CachedDynamicTileManager>().Zoom = 16;
        qParent = GameObject.Find("questionParent");
        qParent.transform.parent = worldMapQParent.transform;

        GameObject newWorld = Instantiate(oldWorld);

        qParent.transform.parent = newWorld.transform;
        Destroy(oldWorld);
        newWorld.name = "World";
        for (int i = newWorld.transform.childCount - 1; i > -1; i--) {
            if (newWorld.transform.GetChild(i).name == "Tiles") {
                Destroy(newWorld.transform.GetChild(i).gameObject);
            }
        }
        world = newWorld;
    }

    void repositionQuestions() {
        qParent = GameObject.Find("questionParent");
        for (int i = qParent.transform.childCount - 1; i > -1; i--) {
            qParent.transform.GetChild(i).gameObject.GetComponent<quest>().moveToLoc();
        }

    }
    void setVariables(GameObject q) {
        world = GameObject.FindWithTag("OpenWorld");

        x = q.transform.position.x;
        z = q.transform.position.z;

        camX = cam.transform.position.x;
        camZ = cam.transform.position.z;

        absX = Mathf.Pow(x - camX, 2);
        absZ = Mathf.Pow(z - camZ, 2);
        distance = Mathf.Sqrt(absX + absZ);
        zoom = world.GetComponent<MapzenGo.Models.TileManager>().Zoom;
        if (zoom == 16) {
            maxDistance = numberOfNewWorldsDistance * 1 * 1834; //new worlds * scale due to zoom * 1834 is dist on new worlds.
        }
        else if (zoom == 15) {
            maxDistance = numberOfNewWorldsDistance * 2 * 1834;
        }
        else if (zoom == 14) {
            maxDistance = numberOfNewWorldsDistance * 4 * 1834;
        }
        else {
            maxDistance = 0;
        }
    }
}
