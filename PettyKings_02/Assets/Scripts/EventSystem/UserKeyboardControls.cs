using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserKeyboardControls : MonoBehaviour {

    // Object containing decision's options
    // private [VARIABLETYPE] decisionPanel;

	// Use this for initialization
	void Start () {
		// decisionPanel = null;
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
            }
            // Must be else if to avoid both options being triggered
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                //  Activate option 2
                //  1.1 Find event system
                //  1.2 Trigger function  
            }
        }
	}
}
