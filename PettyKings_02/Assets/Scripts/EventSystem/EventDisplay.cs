using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDisplay : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static EventDisplay eventDisplay;


    private EventController eventController;

    // List of buttons of choices
    public List<Button> buttons_ = new List<Button>(2);


    // Event currently being displayed
    public Event event_;


    // References to parts of event screen for displaying data
    public Text nameText_;

    public Text descriptionText_;
    public RawImage artworkImage_;


    // When object is created
    void Awake()
    {

        // Check if an eventDisplay already exists
        if (eventDisplay == null)
        {

            // If not set the static reference to this object
            eventDisplay = this;
        }
        else if (eventDisplay != this)
        {

            // Else a different eventDisplay already exists destroy this object
            Destroy(gameObject);
        }
    }



    // Called when script is destroyed
    void OnDestroy()
    {

        // when destroyed remove static reference to itself
        eventDisplay = null;
    }


    // Start method currently sets to main event description
    void Start ()
    {

        eventController = EventController.eventController;

        // Set functions attached to each button
        CreateButtonListeners();

        // Set all the elements to be displayed
        nameText_.text = event_.name_;
        descriptionText_.text = event_.description_;
        artworkImage_.texture = event_.artwork_;
        for (int i = 0; i < buttons_.Count; i++)
        {
            buttons_[i].GetComponentInChildren<Text>().text = event_.decisionText_[i];
        }
	}


    public void DisplayDecision(int choice)
    {

        // debug display the choice the player made
        Debug.Log(choice);

        // Update display to show choice made by player
        descriptionText_.text = event_.decisionDesc_[choice];
        artworkImage_.texture = event_.decisionArt_[choice];
    }



    // Set function for each button
    void CreateButtonListeners()
    {

        // Loop for each button
        for (int i = 0; i < buttons_.Count; i++)
        {

            // set method on button with a parameter saying which button
            int temp = i;
            buttons_[i].onClick.AddListener(delegate { eventController.DecisionSelected(temp); });
        }
    }

}
