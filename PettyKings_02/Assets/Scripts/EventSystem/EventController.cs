using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static EventController eventController;


    // List of available events
    public List<Event> availableEventsList_;


    // List of next events
    public List<Event> nextEventList_;

    // Current event
    public Event currentEvent_;


    // flag if event is active
    bool eventActive;


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

        // No event is active when event controller is created
        eventActive = false;
	}


    void Update()
    {

        // debug if mouse button is pressed start a new event
        if (Input.GetMouseButtonDown(0))
        {

            // If no event is active start a new event
            if (!eventActive)
            {
                StartEvent();
            }
        }
    }



    // Method called on button being pressed
    public void DecisionSelected(int choice)
    {

        // Update display
        if (EventDisplay.eventDisplay)
        {
            EventDisplay.eventDisplay.DisplayDecision(choice);
        }
    }

    public void ContinueButtonClicked()
    {

        // When continue button is pressed end the event
        Debug.Log("Event has ended");
        EndEvent();
    }


    // Method for starting a new event
    public void StartEvent()
    {

        // If no event is currently active
        if (!eventActive)
        {

            // Check for an event on the next event list
            if (nextEventList_.Count > 0)
            {

                // If list isn't empty start next event
                currentEvent_ = nextEventList_[0];
                nextEventList_.RemoveAt(0);
            }
            else
            {

                // If no events on next event list
                // Get event a random event from available events list
                int index = Random.Range(0, availableEventsList_.Count);
                currentEvent_ = availableEventsList_[index];
                availableEventsList_.RemoveAt(index);
            }
            
            // Make event display active and display the new event
            if (EventDisplay.eventDisplay)
            {
                EventDisplay.eventDisplay.gameObject.SetActive(true);
                EventDisplay.eventDisplay.SetEvent(currentEvent_);
                EventDisplay.eventDisplay.Display();
            }

            // Event Active flag is now true
            eventActive = true;
        }
    }


    // Method for ending an event
    public void EndEvent()
    {
        // Set that no event is active and make event display not active
        eventActive = false;
        EventDisplay.eventDisplay.gameObject.SetActive(false);
    }

}
