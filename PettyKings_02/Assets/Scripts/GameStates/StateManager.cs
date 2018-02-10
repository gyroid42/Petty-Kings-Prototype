using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    enum GAMESTATE {
        MENU,
        STAGEONE,
        STAGETWO,
        STAGETHREE,
        GAMEOVER
    };

    GAMESTATE CurrentState_;

	// Use this for initialization
	void Start () {
        CurrentState_ = GAMESTATE.MENU;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ReturnToMenu()
    {
        CurrentState_ = GAMESTATE.MENU;
    }
}
