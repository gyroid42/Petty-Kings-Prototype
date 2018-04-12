using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectBuilding : MonoBehaviour {

    // Public Variables
    // Set larger text input in editor
    [TextArea(1, 7)]
    public string description;
    public string header;

    // Private Variables
    private HelpIconScript HelpIcon_;

	// Use this for initialization
	void Awake () {
        // Locate help icon script in UI
        HelpIcon_ = GameObject.Find("ControlHelper").GetComponent<HelpIconScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HelpIcon_.AddItem(description, header);
        }
    }
}
