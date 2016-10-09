using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {

	public GameObject quest;
    public GameObject questionID;
	public float xCoord;
	public float zCoord;
    private GameObject controller;
    


    // Use this for initialization
    void Start () {
		quest = GameObject.FindGameObjectWithTag("QuestGiver");
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
            controller.GetComponent<questionWindow>().ShowWindow();
        }
	}

}
