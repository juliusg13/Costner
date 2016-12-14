using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class quest : MonoBehaviour {

    //public GameObject questGiver;
    public string questionID;
    public float xCoord;
    public float zCoord;
    private GameObject controller;
    public GameObject questGiver;


    // Use this for initialization
    void Start() {

    }
    public void moveToLoc() {
        //print("QG :" + questGiver + " xCoord :" + xCoord + " zCoord :" + zCoord);
        questGiver.GetComponent<moveObjToTile>().MoveToTile(questGiver, xCoord, zCoord);
    }
    public void changeColorCorrect() {
        SpriteRenderer renderer = questGiver.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 1f, 1f);
        questGiver.GetComponent<SpriteRenderer>().sprite = Resources.Load("Correct", typeof(Sprite)) as Sprite;
        questGiver.GetComponent<Animator>().enabled = false;
    }
    public void changeColorWrong() {
        SpriteRenderer renderer = questGiver.GetComponent<SpriteRenderer>();
        renderer.color = new Color(0.8f, 0.1f, 0.05f);
    }
    // Update is called once per frame
    void Update() {
    }
    void OnMouseUp() {
        //Destroy (this.gameObject);
        if (!EventSystem.current.IsPointerOverGameObject()) {
            questGiver.GetComponent<qWindowDB>().ShowWindow(questionID, questGiver);
            Debug.Log("Clicked on the UI");
        }

    }

}
