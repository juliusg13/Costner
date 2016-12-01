using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLevel : MonoBehaviour {

	public GameObject levelImage;

	public void Loadlevel(bool changeLevel) {

		levelImage.SetActive(true);
	}
	// Sends you back to continue playing the game
	public void BackToGame(bool scene) {

		levelImage.SetActive(false);
    }
}
