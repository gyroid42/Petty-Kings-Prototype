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
    private Queue<HelperText> helperText;
    private bool initialised_;

    // Struct for helper data
    struct HelperText
    {
        private string title;
        private string body;

        public HelperText(string t, string b)
        {
            title = t;
            body = b;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }
    }
    

	// Use this for initialization
	void Start () {
        // Ensure icon is visible on startup
        Open();

        // Initialise tutorial text
        Initialise();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddItem("This is a test\n\nDoes the help icon script work?","Testing testing 123");
        }
	}

    void Initialise()
    {
        // Create queue object
        helperText = new Queue<HelperText>();

        // Cycle through the tutorial text list, adding each item to the queue
        foreach(string text in tutorialText)
        {
            // create struct
            HelperText newItem = new HelperText("",text);

            helperText.Enqueue(newItem);
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
        // New struct
        HelperText newItem = new HelperText(title, bodyText);

        // Add text to the queue to be displayed
        helperText.Enqueue(newItem);
    }

    // Update the UI box with the string stored in the queue
    // Pops top item off the top and makes the next item in the queue the top
    void UpdateText()
    {
        HelperText currentItem = helperText.Dequeue();

        // Update values
        bodyObject.text = currentItem.Body;

        // Update title if new title
        if (currentItem.Title != "")
        {
            headObject.text = currentItem.Title;
        }
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
