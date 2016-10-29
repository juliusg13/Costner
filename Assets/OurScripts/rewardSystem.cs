using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class rewardSystem : MonoBehaviour {
    private int adventureCoins;
    private string adventureText;
    private GUIText coinText;
    public Canvas coinUI;
    GameObject moneyUI;
    // Use this for initialization
    void Start () {
        adventureCoins = 0;
        adventureText = "Ævintýra krónur: " + adventureCoins.ToString();
        moneyUI = GameObject.Find("CoinUI");
        updateUIText();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void increaseCoins(int amount) {
        adventureCoins += amount;
        updateUIText();
    }
    public void spendCoins(int amount) {
        adventureCoins -= amount;
        updateUIText();
    }
    void updateUIText() {
        adventureText = "Ævintýra krónur: " + adventureCoins.ToString();
        moneyUI.GetComponent<Text>().text = adventureText;
    }
}
