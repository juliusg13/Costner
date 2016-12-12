using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getRandomQuestion : MonoBehaviour {
    GameObject controller, questionObject, camera, world;
    string questionName;
    int linkedListCount, random, zoom;
    public int numberOfNewWorldsDistance;
    float camX, camZ, x, z, absX, absZ, distance, maxDistance;
    string qID;
	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController");
        camera = GameObject.FindWithTag("MainCamera");
        world = GameObject.FindWithTag("OpenWorld");
        linkedListCount = 0;
        random = 0;
        numberOfNewWorldsDistance = 3;
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

        camX = camera.transform.position.x;
        camZ = camera.transform.position.z;
        x = q.gameObject.transform.position.x;
        z = q.gameObject.transform.position.z;
        absX = Mathf.Abs(camX - x);
        absZ = Mathf.Abs(camZ - z);
        //print("AbsX : " + absX + " AbsZ : " + absZ);

        distance = Mathf.Sqrt(Mathf.Pow(absX, 2) + Mathf.Pow(absZ, 2));
        world = GameObject.FindWithTag("OpenWorld");
        zoom = world.GetComponent<MapzenGo.Models.TileManager>().Zoom;
        setMaxDistance(zoom);
        print("dist: " + distance);
        print("maxdist : " + maxDistance);
        if (distance > maxDistance) {
            print("LOLSUCKSTO BE YOU NIGGA");
        } else {
            camera.gameObject.transform.position = new Vector3(x, 160f, z);
        }
    }

    void setMaxDistance(int zoom) {
        if(zoom == 16) {
            maxDistance = numberOfNewWorldsDistance * 1 * 1834; //new worlds * scale due to zoom * 1834 is dist on new worlds.
        } else if(zoom == 15) {
            maxDistance = numberOfNewWorldsDistance * 2 * 1834;
        } else if(zoom == 14) {
            maxDistance = numberOfNewWorldsDistance * 4 * 1834;
        } else {
            maxDistance = 0;
        }
        
    }
}
