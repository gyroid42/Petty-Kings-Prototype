using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToggle : MonoBehaviour {

    // Public gameobjects pointing to canvases
    public GameObject MainUICanvas_;
    public GameObject SplashScreen_;

	// Use this for initialization
	void Start () {
		
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
    }
}
