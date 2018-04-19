using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpIconScript : MonoBehaviour
{

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
    private bool begin_;

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
    void Start()
    {
        // Ensure icon is visible on startup
        Open();

        // Initialise tutorial text
        Initialise();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // only update while the timer hasn't been set to false
        if (begin_ == false)
        {
            // if the control helper is open, pause it
            if (icon.activeSelf)
            {
                Timer t = FindTimer();

                // if a timer is present, pause it
                if (t != null)
                {
                    // need to wait for a short period for events to initialise
                    //yield return new WaitForSeconds(0.6f);

                    t.Pause();
                    begin_ = true;
                }
            }
        }
    }

    void Initialise()
    {
        // Create queue object
        helperText = new Queue<HelperText>();

        // Cycle through the tutorial text list, adding each item to the queue
        foreach (string text in tutorialText)
        {
            // create struct
            HelperText newItem = new HelperText("", text);

            helperText.Enqueue(newItem);
        }

        // used for setting timer to pause later
        begin_ = false;

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
        if (eventTimer != null)
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
