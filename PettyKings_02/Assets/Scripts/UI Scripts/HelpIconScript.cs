using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpIconScript : MonoBehaviour {

    // Public variables
    public GameObject icon;
    public Text bodyObject;
    public Text headObject;

    // Set larger text input in editor
    [TextArea(1, 7)]
    public List<string> tutorialText;

    // Queue to handle text efficiently
    private Queue<string> helperText;
    private bool initialised_;
    

	// Use this for initialization
	void Start () {
        // Ensure icon is visible on startup
        Open();

        // Initialise tutorial text
        Initialise();
    }
	
	// Update is called once per frame
	void Update () {

	}

    void Initialise()
    {
        // Create queue object
        helperText = new Queue<string>();

        // Cycle through the tutorial text list, adding each item to the queue
        foreach(string text in tutorialText)
        {
            helperText.Enqueue(text);
        }

        // Update text, starting showing preloaded tutorial text
        UpdateText();
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

    // Added a new item to the queue
    public void AddItem(string bodyText, string title = "")
    {
        // Add text to the queue to be displayed
        helperText.Enqueue(bodyText);

        // Update title if new title provided
        if (title != "")
        {
            UpdateTitle(title);
        }
    }

    void UpdateTitle(string newTitle)
    {
        headObject.text = newTitle;
    }

    // Update the UI box with the string stored in the queue
    // Pops top item off the top and makes the next item in the queue the top
    void UpdateText()
    {
        bodyObject.text = helperText.Dequeue();
    }

    // Goes to next text panel for tutorial helper, closes if the last one
    public void CycleText()
    {
        // check if last item in list
        if (helperText.Count == 0)
        {
            // close helper
            Close();
        }
        else
        {
            // update text
            UpdateText();
        }
    }
}
