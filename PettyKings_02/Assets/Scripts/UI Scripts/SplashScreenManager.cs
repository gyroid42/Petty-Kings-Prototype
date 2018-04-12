using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour {


    private MusicManager musicManager;

	// Use this for initialization
	void Start ()
    {

        musicManager = MusicManager.musicManager;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKeyDown)
        {
            StateManager.stateManager.ChangeState(GAMESTATE.MENU);
            musicManager.FadeSplashScreen(false);
        }
	}
}
