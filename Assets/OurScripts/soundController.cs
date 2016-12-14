using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour {
    rewardSystem coinsSound;
    AudioSource uiSound;
    public AudioClip[] uiClip;
    GameObject controller;
    // Use this for initialization
    void Start() {
        controller = GameObject.FindWithTag("GameController");
        coinsSound = controller.GetComponent<rewardSystem>();
        uiSound = GetComponent<AudioSource>();
    }
    /// <summary>
    /// clip 0 is open questionwindow
    /// clip 1 is closing questionwindow
    /// clip 3 is choosing an answer
    /// </summary>
    public void questionUISound(int soundNumber) {
        uiSound.clip = uiClip[soundNumber];
        uiSound.Play();
    }

    // Update is called once per frame
    void Update() {

    }
    public void playCoinsSound() {
        coinsSound.randomCoinClip();
    }
}
