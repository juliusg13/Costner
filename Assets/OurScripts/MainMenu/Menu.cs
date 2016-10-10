using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
    public Canvas MainCanvas;

    public void QuitOn()
    {
        Application.Quit();
    }
	
    public void PlayOn()
    {
        SceneManager.LoadScene("Test");
    }
}
