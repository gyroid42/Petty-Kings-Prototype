using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToggle : MonoBehaviour {

    // private gameobjects pointing to canvases
    private GameObject MainUICanvas_;
    private GameObject SplashScreen_;
    private GameObject SplashCamera_;
    
    void Awake()
    {
        MainUICanvas_ = GameObject.Find("Stage1UICanvas");
        SplashScreen_ = GameObject.Find("SplashScreenOverlay");
        SplashCamera_ = GameObject.Find("SplashScreenCam");
    }
    
	
	// Update is called once per frame
	void Update () {

	}

    // Only display UI if instructed by state machine
    public void UIVisible(bool t)
    {
        // When main UI is visible, disable splash screen display
        MainUICanvas_.SetActive(t);
        SplashScreen_.SetActive(!t);
        SplashCamera_.SetActive(!t);
    }

    // Update visible UI based on game state
    public void UpdateUI()
    {
        // Get the current state
        GAMESTATE current = StateManager.stateManager.CurrentState();

        // Idle state, display preview camera
        if (current == GAMESTATE.IDLE)
        {
            MainUICanvas_.SetActive(false);
            SplashScreen_.SetActive(true);
        }

        // Menu state, disable preview camera
        if (current == GAMESTATE.MENU)
        {
            MainUICanvas_.SetActive(false);
            SplashScreen_.SetActive(false);
        }

        // Game state, display game UI
        if (current == GAMESTATE.STAGEONE)
        {
            MainUICanvas_.SetActive(true);
            SplashScreen_.SetActive(false);
        }
    } 
}
