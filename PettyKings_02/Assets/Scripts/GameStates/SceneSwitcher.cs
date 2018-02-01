using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    //
	// Define functions for switching scenes below
    //

    // Switch to game state
	public void GoToGameScene()
    {
        SceneManager.LoadScene("SceneInit");
    }
    // Switch to menu state
    public void GoToMenuScene()
    {
        SceneManager.LoadScene("SplashScreen");
    }
    // Exit Game
    public void QuitGame()
    {
        Application.Quit();
    }
}
