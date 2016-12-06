using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class rewardSystem : MonoBehaviour {
    private int adventureCoins;
    private string adventureText;
    private GUIText coinText;
    public AudioClip[] coinsClip;
    AudioSource coinsSound;
    public Canvas coinUI;
    GameObject moneyUI;
    soundController _soundController;

    // Use this for initialization
    void Start () {
        _soundController = GetComponentInParent<soundController>();
        coinsSound = GetComponent<AudioSource>();
        adventureCoins = 0;
        adventureText = "Ævintýra krónur: " + adventureCoins.ToString();
        moneyUI = GameObject.Find("CoinUI");
        updateUIText();
    }
    public void randomCoinClip() {
        coinsSound.clip = coinsClip[Random.Range(0, coinsClip.Length)];
        coinsSound.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void increaseCoins(int amount) {
        adventureCoins += amount;
        updateUIText();
        _soundController.playCoinsSound();
    }
    public void spendCoins(int amount) {
        adventureCoins -= amount;
        updateUIText();
    }
    void updateUIText() {
        adventureText = "Ævintýra krónur: " + adventureCoins.ToString();
        moneyUI.GetComponent<Text>().text = adventureText;
    }
	public int returnCoins (){
		return adventureCoins;
	}
}
