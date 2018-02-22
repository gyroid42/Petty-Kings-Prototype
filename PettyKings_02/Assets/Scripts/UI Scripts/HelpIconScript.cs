using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpIconScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Close down icon
    public void Close(){
        gameObject.SetActive(false);
    }
}
