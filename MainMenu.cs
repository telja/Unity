using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Canvas mainMenu;
    // Use this for initialization
    void Start () {
        mainMenu.enabled = true;
	}
    public void exit()
    {
        mainMenu.enabled = false;
        Application.Quit();
    }
    // Update is called once per frame
    public void playGame()
    {
        SceneManager.LoadScene("game");
    }
    public void menu()
    {
        SceneManager.LoadScene("mainMenu");
    }
	void Update () {
	
	}
}
