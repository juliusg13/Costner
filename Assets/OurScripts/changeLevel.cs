using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class changeLevel : MonoBehaviour {
 
	public GameObject levelImage, controller;
    private GameObject b1, b2, b3, b4, randomQuestion, menuButton;
    bool alreadyUnlocked1, alreadyUnlocked2, alreadyUnlocked3, alreadyUnlocked4, wasSomethingToggled;
    public int level1Cost, level2Cost, level3Cost, level4Cost;
    GameObject cityToggle, mountainToggle, glacierToggle, lakeRiverToggle, moneyUI;
    GameObject spilaButton;

    private int adventureCoins;
	public Toggle citys, mountains, glacier;
	Canvas toggleParent;
    ToggleGroup tGroup;
    string tag;


   public void Start () {
		controller = GameObject.FindWithTag("GameController");
        moneyUI = GameObject.Find("CoinUI");
        setBools();
        Loadlevel();
        //setButtons();
        setPriceText();
    }
    void setBools() {
        alreadyUnlocked1 = false;
        alreadyUnlocked2 = false;
        alreadyUnlocked3 = false;
        alreadyUnlocked4 = false;
        wasSomethingToggled = false;
    }
    void setButtons() {
        b1 = GameObject.Find("Canvas/levelsImage/level1Button");
        b2 = GameObject.Find("Canvas/levelsImage/level2Button");
        b3 = GameObject.Find("Canvas/levelsImage/level3Button");
        b4 = GameObject.Find("Canvas/levelsImage/level4Button");
    }
    void setPriceText() {
        b1.GetComponentInChildren<Text>().text = "LEVEL 1 - " + level1Cost.ToString() + "kr";
        b2.GetComponentInChildren<Text>().text = "LEVEL 2 - " + level2Cost.ToString() + "kr";
        b3.GetComponentInChildren<Text>().text = "LEVEL 3 - " + level3Cost.ToString() + "kr";
        b4.GetComponentInChildren<Text>().text = "LEVEL 4 - " + level4Cost.ToString() + "kr";
    }
    void setIndividualText(GameObject bWhich, string num) {
        bWhich.GetComponentInChildren<Text>().text = "LEVEL " + num;
        bWhich.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
    }
	// Opens the level menu
	public void Loadlevel() {
        levelImage.SetActive(true);
        moneyUI.GetComponent<Text>().color = new Color(0f, 0.7f, 0.01569f);
        tag = "";
        wasSomethingToggled = false;
        setButtons();
        b1.GetComponent<Button>().onClick.AddListener(delegate { setInteractable(1); });
        b2.GetComponent<Button>().onClick.AddListener(delegate { setInteractable(2); });
        b3.GetComponent<Button>().onClick.AddListener(delegate { setInteractable(3); });
        b4.GetComponent<Button>().onClick.AddListener(delegate { setInteractable(4); });

        cityToggle = GameObject.Find("/Canvas/toggleParent/CityToggle");
		mountainToggle = GameObject.Find("/Canvas/toggleParent/MountainToggle");
		glacierToggle = GameObject.Find("/Canvas/toggleParent/GlacierToggle");
		lakeRiverToggle = GameObject.Find("/Canvas/toggleParent/LakeRiverToggle");
        cityToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { setTagCity(); });
        mountainToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { setTagMountain(); });
        glacierToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { setTagGlacier(); });
        lakeRiverToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { setTagLakeRiver(); });

        //levelCostImage = GameObject.Find("/Canvas/levelsImage/levelCost");
        spilaButton = GameObject.Find("/Canvas/toggleParent/Spila");

        toggleParent = GameObject.Find("Canvas/toggleParent").GetComponent<Canvas>();
        tGroup = GameObject.Find("Canvas/toggleParent").GetComponent<ToggleGroup>();
        randomQuestion = GameObject.Find("/Canvas/randomQuestion");
        menuButton = GameObject.Find("Canvas/settings");
        tGroup.SetAllTogglesOff();
        adventureCoins = controller.GetComponent<rewardSystem>().returnCoins();

        if (alreadyUnlocked4) {
            b4.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            b3.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            b2.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            b1.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            setNonInteractable(true, true, true, true);
        } 
        else if (alreadyUnlocked3) {
            b4.gameObject.GetComponentInChildren<Text>().color = new Color(0.6f, 0.6f, 0f);
            b3.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            b2.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            b1.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            setNonInteractable(true, true, true, true);
        }
        else if (alreadyUnlocked2) {
            b4.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0f, 0f);
            b3.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0.7f, 0f);
            b2.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            b1.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            setNonInteractable(true, true, true, false);
        }
        else if (alreadyUnlocked1) {
            
            b4.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0f, 0f);
            b3.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0f, 0f);
            b2.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0.7f, 0.01569f);
            b1.gameObject.GetComponentInChildren<Text>().color = new Color(0f, 0.7f, 0.01569f);
            setNonInteractable(true, true, false, false);
        }
        else{
            b4.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0f, 0f);
            b3.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0f, 0f);
            b2.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0f, 0f);
            b1.gameObject.GetComponentInChildren<Text>().color = new Color(0.7f, 0.7f, 0f);
            setNonInteractable(true, false, false, false);
        }
        enableRandomQuestion(false);
    }

	// Sends you back to continue playing the game aftur pushing a level button
	public void BackToGame() {
        setNonInteractable(true, true, true, true);
        if (cityToggle.GetComponent<Toggle>().isOn) {
            //setTag("cityTag");
            cityToggle.SetActive(false);
        }
        if (mountainToggle.GetComponent<Toggle>().isOn) {
            //setTag("mountainTag");
            mountainToggle.SetActive(false);
        }
        if (glacierToggle.GetComponent<Toggle>().isOn) {
            //setTag("glacierTag");
            glacierToggle.SetActive(false);
        }
        if (lakeRiverToggle.GetComponent<Toggle>().isOn) {
            //setTag("lakeRiverTag");
            lakeRiverToggle.SetActive(false);
        }
        callRenderByTag(tag);
        toggleParent.enabled = false;
        levelImage.SetActive(false);
        enableRandomQuestion(true);
        moneyUI.GetComponent<Text>().color = new Color(0f, 0f, 0f);
    }
    private void setNonInteractable(bool set1, bool set2, bool set3, bool set4) {
        b1.gameObject.GetComponent<Button>().interactable = set1;
        b2.gameObject.GetComponent<Button>().interactable = set2;
        b3.gameObject.GetComponent<Button>().interactable = set3;
        b4.gameObject.GetComponent<Button>().interactable = set4;
    }
	// The game starts at level 1 and unavailable to go to level2 and 3
    public void setInteractable(int buttonNumber) {
        setSpilaButton(false);
        setNonInteractable(false, false, false, false);
        if ((alreadyUnlocked3 == true) && (buttonNumber == 4)) {
            if ((adventureCoins >= level4Cost)) {
                b4.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                b4.gameObject.GetComponent<Button>().onClick.AddListener(BackToGame);
                nextLevel(4);
            }
            else {
                Debug.Log("Þig vantar fleiri ævintýrakrónur");
                setNonInteractable(true, true, true, false);
            }
        }
        else if ((alreadyUnlocked2 == true) && (buttonNumber == 3)) {
            if (adventureCoins >= level3Cost) {
                b3.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                b3.gameObject.GetComponent<Button>().onClick.AddListener(BackToGame);
                nextLevel(3);
            }
            else {
                Debug.Log("Þig vantar fleiri ævintýrakrónur");
                setNonInteractable(true, true, false, false);
            }
        } else if ((alreadyUnlocked1 == true)  && (buttonNumber == 2)) {
            if (adventureCoins >= level2Cost) {
                b2.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                b2.gameObject.GetComponent<Button>().onClick.AddListener(BackToGame);
                nextLevel(2);
            }
            else {
                Debug.Log("Þig vantar fleiri ævintýrakrónur");
                setNonInteractable(true, false, false, false);
            }
        } else if((buttonNumber == 1) && (!alreadyUnlocked1)) {
            if (adventureCoins >= level1Cost) {
                b1.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                b1.gameObject.GetComponent<Button>().onClick.AddListener(BackToGame);
                nextLevel(1);
            } else {
                Debug.Log("Þig vantar fleiri ævintýrakrónur");
            }
        } else { 
            Debug.Log("Þú þarft að leysa stigið á undan");
        }
    }

	public void nextLevel (int buttonNumber) { 
        if (buttonNumber == 1 && !alreadyUnlocked1) {
            setNonInteractable(false, false, false, false);
            controller.GetComponent<rewardSystem>().spendCoins(level1Cost);
            toggleParent.enabled = true;
            alreadyUnlocked1 = true;
            setIndividualText(b1, "1");
        }
        else if(buttonNumber == 2 && !alreadyUnlocked2) {
            setNonInteractable(false, false, false, false);
            controller.GetComponent<rewardSystem>().spendCoins(level2Cost);
            toggleParent.enabled = true;
            alreadyUnlocked2 = true;
            setIndividualText(b2, "2");
        }
        else if (buttonNumber == 3 && !alreadyUnlocked3) {
            setNonInteractable(false, false, false, false);
            controller.GetComponent<rewardSystem>().spendCoins(level3Cost);
            toggleParent.enabled = true;
            alreadyUnlocked3 = true;
            setIndividualText(b3, "3");
        }
        else if (buttonNumber == 4 && !alreadyUnlocked4) {
            setNonInteractable(false, false, false, false);
            controller.GetComponent<rewardSystem>().spendCoins(level4Cost);
            toggleParent.enabled = true;
            alreadyUnlocked4 = true;
            setIndividualText(b4, "4");
        }
    }

    private void callRenderByTag(string tag) {
        controller.GetComponent<renderQuestionsByTags>().setBoolean(tag);
    }
    private void setTagCity() {
        tag = "cityTag";
        setSpilaButton(true);
    }
    private void setTagGlacier() {
        tag = "glacierTag";
        setSpilaButton(true);
    }
    private void setTagMountain() {
        tag = "mountainTag";
        setSpilaButton(true);
    }
    private void setTagLakeRiver() {
        tag = "lakeRiverTag";
        setSpilaButton(true);
    }
    private void setSpilaButton(bool set) {
        spilaButton.SetActive(set);
    }

    public void enableRandomQuestion(bool showOrNot) {
        randomQuestion.SetActive(showOrNot);
        menuButton.SetActive(showOrNot);
    }
    /*
    public void OnPointerEnter(PointerEventData eventData) {
        if(eventData.pointerEnter) {
            showCost(level4Cost);
        }
        
        if(this.gameObject.name == "level1Button") {
            showCost(level1Cost);
        } else if(this.gameObject.name == "level2Button") {
            showCost(level2Cost);
        } else if(this.gameObject.name == "level3Button") {
            showCost(level3Cost);
        } else if(this.gameObject.name == "level4Button") {
            showCost(level4Cost);
        }
    }
    void showCost(int cost) {
        string setText = "Kostar: " + cost.ToString();
        levelCostImage.GetComponentInChildren<Text>().text = setText;
    }
*/
}


