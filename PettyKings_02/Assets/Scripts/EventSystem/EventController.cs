using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

    // A static reference to itself so other scripts can access it
    public static EventController eventController;
    private SeasonController seasonController;

    

    // Current event
    private Event currentEvent_;
    private List<Event> activeEvents_;

    // Next event queue
    private Queue<Event> eventQueue_;


    
    // flag if event is active
    bool eventActive_;



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

    // Called when eventController created
    void Start()
    {
        // Create empty event queue
        eventQueue_ = new Queue<Event>();

        // EventActive is false since no event has started
        eventActive_ = false;

        // Initialse active Events list
        activeEvents_ = new List<Event>();

        // Create reference to seasonController
        seasonController = SeasonController.seasonController;
    }


    // Called every frame
    void Update()
    {

        // If an event is active
        if (eventActive_)
        {

            // Update current event
            if (!currentEvent_.Update())
            {
                // If event has ended call EndEvent()
                EndEvent();
            }
        }
    }


    // Method to start a new event
    public void StartEvent(Event newEvent)
    {
        // If no event is currently happening
        if (eventActive_ == false)
        {

            // Start new event
            Debug.Log("event is starting");
            eventActive_ = true;
            currentEvent_ = newEvent;
            currentEvent_.Begin();

            // Pause timer till next season
            seasonController.PauseTimer();
        }
        else
        {

            // Else add new Event to event queue
            eventQueue_.Enqueue(newEvent);
        }
    }


    // Called when event Ends
    void EndEvent()
    {

        // If currentEvent exists
        if (currentEvent_ != null)
        {
            // End CurrentEvent
            currentEvent_.End();

            // set currentEvent to null;
            currentEvent_ = null;
        }

        // Start next event
        GotoNextEvent();
    }

    // Starts next event in queue
    bool GotoNextEvent()
    {
        
        // If queue is empty
        if (eventQueue_.Count <= 0)
        {

            // No more events to run
            //
            // Start season timer again
            seasonController.StartTimer();

            // No event is active anymore
            eventActive_ = false;
            return false;
        }

        // Else start next event in queue
        eventActive_ = true;
        currentEvent_ = eventQueue_.Dequeue();
        currentEvent_.Begin();
        seasonController.PauseTimer();
        return true;
    }


}
