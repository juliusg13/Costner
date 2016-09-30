using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {

	public GameObject quest;
	public float xCoord;
	public float zCoord;

	// Use this for initialization
	void Start () {
		quest = GameObject.FindGameObjectWithTag("QuestGiver");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (xCoord, this.transform.position.y, zCoord);
	}
	void OnMouseUp(){
		if (this.gameObject.tag == "QuestGiver") {
			Destroy (this.gameObject);
		}
	}
}
