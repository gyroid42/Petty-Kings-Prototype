using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

    // A static reference to itself so other scripts can access it
    public static EventController eventController;
    private SeasonController seasonController;


    // List of introduction Events
    private List<NarrativeEvent> introEvents_;
    private List<NarrativeEvent> eventPool_;
    

    // Current event
    private List<NarrativeEvent> activeEvents_;

    // Next event queue
    private Queue<NarrativeEvent> eventQueue_;


    // Flag for if last event started was blocking
    bool isBlocked_;



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
        if (eventController == this)
        {

            // when destroyed remove static reference to itself
            eventController = null;
        }
    }

    // Called when eventController created
    void Start()
    {
        // Create empty event queue
        eventQueue_ = new Queue<NarrativeEvent>();

        isBlocked_ = false;

        // Initialse active Events list
        activeEvents_ = new List<NarrativeEvent>();

        // Create reference to seasonController
        seasonController = SeasonController.seasonController;

        
    }

    // Called at start of game
    public void GameStart()
    {

        introEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Introduction", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());

        introEvents_[introEvents_.Count - 1].isPooled_ = true;

        foreach (NarrativeEvent ev in introEvents_)
        {
            StartEvent(ev);
        }

    }


    // Called every frame
    void Update()
    {

        // Update each event
        UpdateEvents();
        
    }


    // Updates each active event
    void UpdateEvents()
    {

        // Loop for each active event and update it
        for (int i = 0; i < activeEvents_.Count;)
        {

            // If the event has ended
            if (!activeEvents_[i].Update())
            {

                // End the event

                // If the event exists
                if (activeEvents_[i] != null)
                {

                    // Call end on event then delete it
                    activeEvents_[i].End();
                    activeEvents_[i] = null;
                }

                // Remove the event from the active list
                activeEvents_.RemoveAt(i);

                if (!CheckSeasonPause())
                {
                    seasonController.StartTimer();
                }

                // If no more events in active list
                if (activeEvents_.Count <= 0)
                {
                    // Goto next event and Break out of loop
                    GotoNextEvent();
                    break;
                }
            }

            // Else move index to next active event
            else
            {
                i++;
            }
        }
    }


    // Method to start a new event
    public void StartEvent(NarrativeEvent newEvent)
    {
        // If no event is currently happening or last event wasn't blocking
        if (activeEvents_.Count <= 0 || !isBlocked_ || newEvent.instantPriority_)
        {

            // Start the event
            newEvent.Begin();
            activeEvents_.Add(newEvent);

            // Pause season timer
            if (newEvent.stopSeasonTimer_)
            {
                //seasonController.PauseTimer();
            }

            // If event is blocking
            if (newEvent.isBlocking_)
            {

                // Set flag for last event blocking
                isBlocked_ = true;
            }
        }
        else
        {

            // Else add new Event to event queue
            eventQueue_.Enqueue(newEvent);
        }
    }

    

    // Starts next event in queue
    bool GotoNextEvent()
    {

        // Reset is Blocking flag
        isBlocked_ = false;

        // While queue isn't empty
        while (eventQueue_.Count > 0)
        {

            // Start next event
            NarrativeEvent nextEvent = eventQueue_.Dequeue();

            nextEvent.Begin();
            activeEvents_.Add(nextEvent);

            // Pause season timer
            if (nextEvent.stopSeasonTimer_)
            {
                seasonController.PauseTimer();
            }


            // If event is blocking
            if (nextEvent.isBlocking_)
            {
                // Set flag for last event blocking
                isBlocked_ = true;

                // Stop adding events to active event queue
                break;
            }
            
        }

        // If there are no active events
        if (activeEvents_.Count <= 0)
        {

            // No more events to run
            //
            // Start season timer again
            seasonController.StartTimer();

            // No event is active anymore
            return false;
        }

        // Event is running
        return true;
    }


    // Starts an event from the current event pool
    public bool StartEventFromPool()
    {

        // if event pool is empty cannot start an event
        if (eventPool_.Count <= 0)
        {
            return false;
        }

        // Start random event from event pool and remove it from the pool
        int index = Random.Range(0, eventPool_.Count);
        StartEvent(eventPool_[index]);
        eventPool_.RemoveAt(index);


        // Event did start so return true
        return true;
    }

    // Adds an event to the event pool
    public void AddEventToPool(NarrativeEvent newEvent)
    {
        if (newEvent != null)
        {
            newEvent.isPooled_ = true;
            eventPool_.Add(newEvent);
        }
    }

    // Checks if any of the active events are pausing the season timer
    private bool CheckSeasonPause()
    {

        // Loop for each activeEvent
        for (int i = 0; i < activeEvents_.Count; i++)
        {

            // If event is pausing timer return true
            if (activeEvents_[i].stopSeasonTimer_)
            {
                return true;
            }
        }


        // No event returned true therefore no event is pausing the timer
        return false;
    }


}
