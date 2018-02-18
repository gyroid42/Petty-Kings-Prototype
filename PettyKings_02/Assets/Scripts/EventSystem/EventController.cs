using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

    // A static reference to itself so other scripts can access it
    public static EventController eventController;


    private EventDisplay eventDisplay;
    private ResourceManager resourceManager;
    private SeasonAudioManager seasonAudioManager;

    

    // Current event
    private Event currentEvent_;

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

    void Start()
    {
        eventQueue_ = new Queue<Event>();
        eventActive_ = false;
    }


    void Update()
    {

        if (eventActive_)
        {
            if (!currentEvent_.Update())
            {
                EndEvent();
            }
        }
    }


    public void StartEvent(Event newEvent)
    {
        if (eventActive_ == false)
        {
            eventActive_ = true;
            currentEvent_ = newEvent;
            currentEvent_.Begin();
        }
        else
        {
            eventQueue_.Enqueue(newEvent);
        }
    }

    void EndEvent()
    {

        if (currentEvent_ != null)
        {
            currentEvent_ = null;
        }

        GotoNextEvent();
    }


    bool GotoNextEvent()
    {
        
        if (eventQueue_.Count <= 0)
        {
            eventActive_ = false;
            return false;
        }

        eventActive_ = true;
        currentEvent_ = eventQueue_.Dequeue();
        currentEvent_.Begin();

        return true;
    }


}
