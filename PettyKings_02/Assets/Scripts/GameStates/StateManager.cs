using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    // Enums
    enum GAMESTATE {
        MENU,
        STAGEONE,
        STAGETWO,
        STAGETHREE,
        GAMEOVER
    };

    // Variables
    GAMESTATE CurrentState_;
    public bool OverlayActive_;

    // Script references
    public HUDToggle GameUI_;
    public CameraController CameraController_;

	// Use this for initialization
	void Start () {
        CurrentState_ = GAMESTATE.MENU;

        // Set UI to be inactive at start
        OverlayActive_ = false;
        GameUI_.UIVisible(OverlayActive_);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Change the game's state
    void ChangeState(GAMESTATE newState)
    {
        CurrentState_ = newState;

        // Set UI off if in the menu or end state
        if ((newState == GAMESTATE.MENU) || (newState == GAMESTATE.GAMEOVER))
        {
            OverlayActive_ = false;
        }
        else
        {
            OverlayActive_ = true;
        }
        GameUI_.UIVisible(OverlayActive_);
    }

    public void ReturnToMenu()
    {
        ChangeState(GAMESTATE.MENU);
    }

    // Set game to be playing
    public void ActivateStageOne()
    {
        ChangeState(GAMESTATE.STAGEONE);
        CameraStageOne();
    }
    
    // CAMERA MOVEMENTS
    void CameraStageOne()
    {
        // Position/Rotation for Stage One
        Vector3 pos = new Vector3(-6.36f, 15.1f, -10.0f);
        Vector3 rot = new Vector3(55.59f, 36.736f, 0.0f);
        CameraController_.AddGotoPosition(pos, rot, true);
    }
 }
