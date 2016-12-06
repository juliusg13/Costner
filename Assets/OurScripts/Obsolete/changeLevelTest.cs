/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeLevel : MonoBehaviour {

	public float levelStartDealy = 2f; 	public Canvas levels;   	private Button levelsButton; 	private Button level1Button; 	private Button level2Button; 	private Button level3Button;  	private Text levelText; 	private GameObject levelImage; 	private int level = 1; 	private bool doingSetup;   	private void levelUp (int index) {  		level++; 		initGame(); 	} 	// displays the level image when clicked 	void initGame () {  		doingSetup = true; 		levelImage = GameObject.Find("LevelImage"); 		levelText = GameObject.Find("LevelsButton").GetComponents<Button>(); 		levelImage.SetActive(true);  		 	} 	// hide the level image when game continues 	private void hideLevelImage () {  		levelImage.SetActive(false); 		doingSetup = false; 	} 	// Use this for initialization 	/*void Start () { 		 		level1 = true;  	} 	 	// Update is called once per frame 	void Update () { 	 	} */ //}  

