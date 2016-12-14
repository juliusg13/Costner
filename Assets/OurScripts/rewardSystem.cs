using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class rewardSystem : MonoBehaviour {
    public int adventureCoins;
    private string adventureText;
    private GUIText coinText;
    public AudioClip[] coinsClip;
    AudioSource coinsSound;
    public Canvas coinUI;
    GameObject moneyUI, notifyLevelUnlocked, canv;
    soundController _soundController;

    // Use this for initialization
    void Start() {
        _soundController = GetComponentInParent<soundController>();
        coinsSound = GetComponent<AudioSource>();
        adventureText = "Ævintýra krónur: " + adventureCoins.ToString();
        moneyUI = GameObject.Find("CoinUI");
        notifyLevelUnlocked = GameObject.Find("notifyLevelUnlocked");
        canv = GameObject.Find("Canvas");
        updateUIText();
    }
    public void randomCoinClip() {
        coinsSound.clip = coinsClip[Random.Range(0, coinsClip.Length)];
        coinsSound.Play();
    }

    // Update is called once per frame
    void Update() {

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
    public int returnCoins() {
        return adventureCoins;
    }
}
