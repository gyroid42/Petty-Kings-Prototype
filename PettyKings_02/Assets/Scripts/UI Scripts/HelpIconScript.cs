using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpIconScript : MonoBehaviour {

    // Find icon game object
    public GameObject Icon;

	// Use this for initialization
	void Start () {
        // Ensure icon is visible on startup
        Icon.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Close down icon
    public void Close(){
        Icon.SetActive(false);
    }
}
