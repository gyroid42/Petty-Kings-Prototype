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

        eventActive = false;
	}


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        Debug.Log("this si happenienginei");
        // foreach (Transform child in EventDisplay.eventDisplay.transform)
        //{
        //GameObject.Destroy(child.gameObject);
        //}
        EndEvent();
    }


    public void StartEvent()
    {
        if (!eventActive)
        {

            if (nextEventList_.Count > 0)
            {

                currentEvent_ = nextEventList_[0];
                nextEventList_.RemoveAt(0);
            }
            else
            {
                int index = Random.Range(0, availableEventsList_.Count);
                currentEvent_ = availableEventsList_[index];

                availableEventsList_.RemoveAt(index);
            }
            

            EventDisplay.eventDisplay.gameObject.SetActive(true);

            if (EventDisplay.eventDisplay)
            {
                EventDisplay.eventDisplay.SetEvent(currentEvent_);
                EventDisplay.eventDisplay.Display();
            }

            eventActive = true;
        }
    }

    public void EndEvent()
    {

        eventActive = false;
        //Destroy(EventDisplay.eventDisplay.gameObject);
        EventDisplay.eventDisplay.gameObject.SetActive(false);
    }

}
