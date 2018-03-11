using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpIconScript : MonoBehaviour {

    // Public variables
    public GameObject icon;
    public Text bodyObject;
    // Set larger text input in editor
    [TextArea(1, 7)]
    public List<string> helperText;
    // Private variables
    private int listPosition = 0;
    private int listSize;
    

	// Use this for initialization
	void Start () {
        // Ensure icon is visible on startup
        icon.SetActive(true);

		// Update text
		UpdateText();

        // Get list size, used in CycleText for iteration
		listSize = (helperText.Count - 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Close down icon
    public void Close()
    {
        icon.SetActive(false);
    }

    // Display tutorial again
    public void Open()
    {
        icon.SetActive(true);
    }

    // Update the UI box with the string stored in the list
    // Uses list position variable to point to index in list
    void UpdateText()
    {
        bodyObject.text = helperText[listPosition];
    }

    // Goes to next text panel for tutorial helper, closes if the last one
    public void CycleText()
    {
        // check if last item in list
        if (listPosition == listSize)
        {
            // close helper
            Close();
        }
        else
        {
            // next text
            listPosition++;
            // update text
            UpdateText();
        }
    }
}
