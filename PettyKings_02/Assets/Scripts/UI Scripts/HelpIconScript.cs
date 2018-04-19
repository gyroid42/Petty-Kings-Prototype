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
	private HelperText inspectText;
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
	void Start ()
    {
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

        inspectText = new HelperText("", "");

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

        // When the druid helper is deactivated, the timer begins again
        // find timer
        Timer eventTimer = FindTimer();

        // if timer has been found, start timer again
        if(eventTimer != null)
        {
            eventTimer.Start();
        }
    }

    // Display tutorial again
    // If update is true, also update text
    // If text isn't updated, the previous text will be displayed instead
    public void Open()
    {
        icon.SetActive(true);

        // When the druid helper is activated, the timer stops
        // find timer
        Timer eventTimer = FindTimer();

        // if timer has been found, stop timer
        if (eventTimer != null)
        {
            eventTimer.Pause();
        }
    }

    // Added a new item to the queue
    public void AddItem(string bodyText, string title = "", bool priority = false)
    {

        // Clear queue if priority
        if (priority)
        {
            helperText.Clear();
        }

        // New struct
        HelperText newItem = new HelperText(title, bodyText);

        // Add text to the queue to be displayed
        helperText.Enqueue(newItem);

        // If helper text was empty, reopen it
        if (helperText.Count == 1)
        {
            Open();
            UpdateText();
        }
    }

	public void AddInspect(string bodyText, string title = "")
	{
        if (!icon.activeSelf)
        {
            inspectText = new HelperText (title, bodyText);
        
            Debug.Log("this is happening");
            Open();
            UpdateText();
        }
        

	}

    // Update the UI box with the string stored in the queue
    // Pops top item off the top and makes the next item in the queue the top
    void UpdateText()
    {
		if (helperText.Count > 0) {
			HelperText currentItem = helperText.Dequeue ();

			// Update values
			bodyObject.text = currentItem.Body;

			// Update title if new title
			if (currentItem.Title != "")
			{
				headObject.text = currentItem.Title;
			}
		} 
		else if (inspectText.Body != "") {
			bodyObject.text = inspectText.Body;
			headObject.text = inspectText.Title;
			inspectText.Body = "";
            inspectText.Title = "";
		}


        
    }

    // Goes to next text panel for tutorial helper, closes if the last one
    public void CycleText()
    {
        // check if last item in list
        if (helperText.Count == 0 && inspectText.Body == "")
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

    // return the timer of the current event
    Timer FindTimer()
    {
        // locate event display
        GameObject currEvent = GameObject.Find("EventScreen");

        if (currEvent)
        {
            return currEvent.GetComponent<EventDisplay>().GetTimer();
        }
        return null;
    }
}
