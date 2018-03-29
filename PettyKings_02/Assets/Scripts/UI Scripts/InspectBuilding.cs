using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InspectBuilding : MonoBehaviour {

    // Public Variables
    // Set larger text input in editor
    [TextArea(1, 7)]
    public string description;
    public string header;

    // Private Variables
    private HelpIconScript HelpIcon_;
    private UnityEngine.EventSystems.EventSystem eventSystem_;

	// Use this for initialization
	void Awake () {
        // Locate help icon script in UI
        HelpIcon_ = GameObject.Find("ControlHelper").GetComponent<HelpIconScript>();

        // Locate event system in scene for UI
        eventSystem_ = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseOver()
    {
        // Check no UI elements are blocking
        if(eventSystem_.IsPointerOverGameObject())
        {
            // Mouse pointer is over a UI element, exit function
            return;
        }
        // If left mouse button is pressed, trigger function
        if(Input.GetMouseButtonDown(0))
        {
            HelpIcon_.AddItem(description, header);
        }
    }
}
