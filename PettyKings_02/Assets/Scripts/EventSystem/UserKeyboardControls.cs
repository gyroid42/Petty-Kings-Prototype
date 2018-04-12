using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserKeyboardControls : MonoBehaviour {

    public static UserKeyboardControls userKeyboardControls;
    // Object containing decision's options
    // private [VARIABLETYPE] decisionPanel;


    public ButtonDel[] btnFunctions;

    // When object is created
    void Awake()
    {

        // Check if an eventController already exists
        if (userKeyboardControls == null)
        {

            // If not set the static reference to this object
            userKeyboardControls = this;
        }
        else if (userKeyboardControls != this)
        {

            // Else a different eventController already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {
        if (userKeyboardControls == this)
        {

            // when destroyed remove static reference to itself
            userKeyboardControls = null;
        }
    }

    // Use this for initialization
    void Start () {
        // decisionPanel = null;
        btnFunctions = new ButtonDel[2];
	}
	
	// Update is called once per frame
	void Update () {
        // if decision panel is null, find the current decision object (if present)
        /* 
        if(decisionPanel == null)
        {
            // find decision panel
        }
        */

		// Get user input, making sure that the decision panel is there
        if(Input.anyKeyDown /*&& decisionPanel != null*/)
        {
            // Left option
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //  Activate option 1
                //  1.1 Find event system
                //  1.2 Trigger function   
                btnFunctions[0](0);
            }
            // Must be else if to avoid both options being triggered
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                //  Activate option 2
                //  1.1 Find event system
                //  1.2 Trigger function  
                btnFunctions[1](1);
            }
        }
	}


    public void SetButtons(ButtonDel[] newFunctions)
    {

        btnFunctions = null;

        btnFunctions = newFunctions;
    }
}
