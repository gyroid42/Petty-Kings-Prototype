using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Data for displaying an event
[System.Serializable]
public struct EventDisplayData {

    // Name of event
    public string name_;

    // Timer for event
    public float timerLength_;

    // Text in Description box
    [TextArea(3, 5)]
    public string description_;

    // Display artwork
    public Texture artwork_;
    
    // Text for button
    public string[] btnText_;

    // Function pointers for buttons
    public ButtonDel[] btnFunctions_;

}
