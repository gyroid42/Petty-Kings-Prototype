using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStartUp : MonoBehaviour {

    public bool isHidden = true;

	// Use this for initialization
	void Start () {
        // If true, hide this object on game start
        gameObject.SetActive(!isHidden);
	}
}
