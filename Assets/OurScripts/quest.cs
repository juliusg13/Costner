using UnityEngine;
using System.Collections;

public class quest : MonoBehaviour {

	public GameObject questGiver;
    public string questionID;
	public float xCoord;
	public float zCoord;
    private GameObject controller;
    


    // Use this for initialization
    void Start () {
		questGiver = GameObject.FindGameObjectWithTag("QuestGiver");
        controller = GameObject.FindWithTag("GameController");

    }
    
    // Update is called once per frame
    void Update () {
		this.transform.position = new Vector3 (xCoord, this.transform.position.y, zCoord);
	}
	void OnMouseUp(){
		if (this.gameObject.tag == "QuestGiver") {
            //Destroy (this.gameObject);
            //window.GetComponent<questionWindow>().ShowWindow();
          //  controller.GetComponent<qWindowDB>().ShowWindow();
			controller.GetComponent<qWindowDB> ().ShowWindow (questionID);
        }
	}

}
