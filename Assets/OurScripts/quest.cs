using UnityEngine;
using System.Collections;

public class quest : MonoBehaviour {

	//public GameObject questGiver;
    public string questionID;
	public float xCoord;
	public float zCoord;
    private GameObject controller;
    public GameObject questGiver;


    // Use this for initialization
    void Start () {
    }
    public void MoveToLoc() {
        //print("QG :" + questGiver + " xCoord :" + xCoord + " zCoord :" + zCoord);
        questGiver.GetComponent<moveObjToTile>().MoveToTile(questGiver, xCoord, zCoord);
    }
    // Update is called once per frame
    void Update () {
	}
	void OnMouseUp(){
        //Destroy (this.gameObject);

        questGiver.GetComponent<qWindowDB>().ShowWindow(questionID);
	}

}
