using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class changeLevel : MonoBehaviour {

	public GameObject levelImage, controller;
    private GameObject b1, b2, b3;
    Toggle t1, t2, t3, t4;
	public int adventureCoins;
	public Toggle citys, mountains, glacier;
	Canvas toggleGroup;


	// Opens the level menu
	public void Loadlevel(bool changeLevel) {
		levelImage.SetActive(true);

        b1 = GameObject.Find("Canvas/levelsImage/level1Button");
        b2 = GameObject.Find("/Canvas/levelsImage/level2Button");
        b3 = GameObject.Find("/Canvas/levelsImage/level3Button");

		t1 = GameObject.Find("/Canvas/toggleGroup/CityToggle").GetComponent<Toggle>();
		print(t1);
		t2 = GameObject.Find("/Canvas/toggleGroup/MountainToggle").GetComponent<Toggle>();
		t3 = GameObject.Find("/Canvas/toggleGroup/GlacierToggle").GetComponent<Toggle>();
		t4 = GameObject.Find("/Canvas/toggleGroup/LakeRiverToggle").GetComponent<Toggle>();
        print(b1 + " " + b2 + " " + b3);
        controller = GameObject.FindWithTag("GameController");
        //Hé þarf að nteractivatea question windowinn 
        toggleGroup = GameObject.Find("/Canvas/toggleGroup").GetComponent<Canvas>();
    }

	// Sends you back to continue playing the game aftur pushing a level button
	public void BackToGame() {
		toggleGroup.enabled = false;

		levelImage.SetActive(false);
    }

   public void Start () {
        //setLevels();
		controller = GameObject.FindWithTag("GameController");
	}

	// The game starts at level 1 and unavailable to go to level2 and 3
	public void setLevels () {
        b1.gameObject.GetComponent<Button>().interactable = true;
		b2.gameObject.GetComponent<Button>().interactable = false;
		b3.gameObject.GetComponent<Button>().interactable = false;
	}
    public void setInteractable(int buttonNumber) {
        adventureCoins = controller.GetComponent<rewardSystem>().returnCoins();
        print(adventureCoins);

        if (adventureCoins >= 10 && buttonNumber == 3) {
            b2.gameObject.GetComponent<Button>().interactable = true;
            b3.gameObject.GetComponent<Button>().interactable = true;
            nextLevel(3);
            return;
        } else if(adventureCoins >= 5 && buttonNumber == 2) {
            b2.gameObject.GetComponent<Button>().interactable = true;
            nextLevel(2);
            return;
        } else {
            Debug.Log("Þig vantar fleiri ævintýrakrónur");
        }
    }

	public void nextLevel (int buttonNumber) {
        if(buttonNumber == 2) {
            controller.GetComponent<rewardSystem>().spendCoins(5);
            //print("button 2 pressed");
            toggleGroup.enabled = true;
            chooseLevelTag();
           
        }
        if (buttonNumber == 3) {
            controller.GetComponent<rewardSystem>().spendCoins(10);
            //print("button 3 pressed");
			toggleGroup.enabled = true;
           chooseLevelTag();
        }
    }

    public void chooseLevelTag ()
	{
		print ("POOP");
		print(t1);
		if (t1.isOn) {
			controller.GetComponent<renderQuestionsByTags> ().setBoolean ("cityTag");
		}
		if (t2.enabled == true) {
			controller.GetComponent<renderQuestionsByTags> ().setBoolean ("mountainTag");
			print("opo");
		}
		if (t3.isOn) {
			controller.GetComponent<renderQuestionsByTags> ().setBoolean ("glacierTag");
		}
		if (t4.isOn) {
		print("<wesdfgjhj");
			controller.GetComponent<renderQuestionsByTags> ().setBoolean ("lakeRiverTag");
		}

	}
		/* void OnGUI (){

		if(t2.GetComponent<Toggle>().enabled) {
		print("wat");
			//controller.GetComponent<renderQuestionsByTag> ().setBoolean ("cityTag");
			controller.GetComponent<renderQuestionsByTags>().setBoolean("cityTag");
		}

	}

	void OnGUI() {
        GUILayout.BeginVertical("Toggle");
        selGridInt = GUILayout.SelectionGrid(selGridInt, radioButton, 1);
        if (GUILayout.Button("Spila"))
            Debug.Log("You chose " + radioButton[selGridInt]);
        
        GUILayout.EndVertical();
    }*/
}


