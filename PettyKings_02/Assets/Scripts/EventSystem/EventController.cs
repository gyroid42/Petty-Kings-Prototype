using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static EventController eventController;


    // Reference to Event Display script
    private EventDisplay display_;


    // List of available events
    public List<Event> eventList_;


    // When object is created
    void Awake()
    {

        // Check if an eventController already exists
        if (eventController == null)
        {

            // If not set the static reference to this object
            eventController = this;
        }
        else if (eventController != this)
        {

            // Else a different eventController already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {

        // when destroyed remove static reference to itself
        eventController = null;
    }


    void Start ()
    {

        // Set reference to event display
        display_ = EventDisplay.eventDisplay;
        
	}



    // Method called on button being pressed
    public void DecisionSelected(int choice)
    {

        // Update display
        display_.DisplayDecision(choice);
    }

}
