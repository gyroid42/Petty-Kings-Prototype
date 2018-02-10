using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToggle : MonoBehaviour {

    public GameObject MainUICanvas_;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    // Only display UI if instructed by state machine
    public void UIVisible(bool t)
    {
        MainUICanvas_.SetActive(t);
    }
}
