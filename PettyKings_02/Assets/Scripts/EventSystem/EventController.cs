using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static EventController eventController;


    // List of events
    public List<Event> introductionEvents_;
    public List<Event> springEventList_;
    public List<Event> summerEventList_;
    public List<Event> autumnEventList_;
    public List<Event> winterEventList_;
    public List<Event> springEventList2_;

    public List<Event> seasonStartEvents_;

    // Current event
    public Event currentEvent_;

  
    // Current Season
    public int currentSeason_;
    public Season[] seasonList_ = new Season[6] { Season.INTRO, Season.SPRING, Season.SUMMER, Season.AUTUMN, Season.WINTER, Season.SPRING2 };

    // flag if event is active
    bool eventActive_;


    private Timer nextEventTimer_ = new Timer();
    public float timeTillNextEvent_;

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
        eventActive_ = false;

        // Load all the events into the event lists
        introductionEvents_ = new List<Event>(Resources.LoadAll("Events/Introduction", typeof(Event)).Cast<Event>().ToArray());
        springEventList_ = new List<Event>(Resources.LoadAll("Events/Spring", typeof(Event)).Cast<Event>().ToArray());
        summerEventList_ = new List<Event>(Resources.LoadAll("Events/Summer", typeof(Event)).Cast<Event>().ToArray());
        autumnEventList_ = new List<Event>(Resources.LoadAll("Events/Autumn", typeof(Event)).Cast<Event>().ToArray());
        winterEventList_ = new List<Event>(Resources.LoadAll("Events/Winter", typeof(Event)).Cast<Event>().ToArray());
        springEventList2_ = new List<Event>(Resources.LoadAll("Events/Spring2", typeof(Event)).Cast<Event>().ToArray());


        // Setup Event Timer
        nextEventTimer_.SetTimer(timeTillNextEvent_, false);

        // Set starting season to the intro
        currentSeason_ = 0;
    }


    void Update()
    {

        // If stage 2 is running
        if (true)
        {

            // Check if timer for next event has been triggered
            if (nextEventTimer_.UpdateTimer() && !eventActive_) 
            {

                ChangeSeason();
                // Goto the next season
                //currentSeason_++;
                //currentSeason_ %= seasonList_.Length;

                // Start an event from that season
                //StartEvent();
            }

            // If there are still introduction events and no event is active
            if (introductionEvents_.Count > 0 && !eventActive_) 
            {

                // Start the next event from the intro
                StartEvent();
            }

        }
    }


    void ChangeSeason()
    {

        currentSeason_++;
        currentSeason_ %= seasonList_.Length;


        if (!eventActive_)
        {
            if (EventDisplay.eventDisplay != null)
            {


                currentEvent_ = seasonStartEvents_[currentSeason_];

                EventDisplay.eventDisplay.gameObject.SetActive(true);
                EventDisplay.eventDisplay.SetEvent(currentEvent_);
                EventDisplay.eventDisplay.DisplaySeasonStart();
            }

            eventActive_ = true;
        }
    }



    // Method called on button being pressed
    public void DecisionSelected(int choice)
    {

        // Update display
        if (EventDisplay.eventDisplay)
        {
            EventDisplay.eventDisplay.DisplayDecision(choice);
            ResourceManager.resourceManager.UpdateResources(currentEvent_.GetDecisionResources(choice));
        }
    }

    public void ContinueButtonClicked()
    {

        // When continue button is pressed end the event
        Debug.Log("Event has ended");
        eventController.EndEvent();
    }

    public void StartSeasonButtonClicked()
    {
        Debug.Log("start of season clicked");
        eventController.StartEvent();
    }


    // Method for starting a new event
    public void StartEvent()
    {

        // If no event is currently active
        //if (!eventActive_)
        //{

            // Bool to check if an event is even found
            bool eventFound = false;

            // Check the season and get event from appropriate list
            switch (seasonList_[currentSeason_])
            {
                case Season.SPRING:
                    eventFound = GetRandomEvent(springEventList_);
                    break;
                case Season.SUMMER:
                    eventFound = GetRandomEvent(summerEventList_);
                    break;
                case Season.AUTUMN:
                    eventFound = GetRandomEvent(autumnEventList_);
                    break;
                case Season.WINTER:
                    eventFound = GetRandomEvent(winterEventList_);
                    break;
                case Season.SPRING2:
                    eventFound = GetRandomEvent(springEventList2_);
                    break;
                case Season.INTRO:
                    eventFound = GetNextEvent(introductionEvents_);
                    break;
                
            }
            
            // If no event was found leave the method
            if (!eventFound)
            {
                return;
            }
            
            // Make event display active and display the new event
            if (EventDisplay.eventDisplay != null)
            {
                EventDisplay.eventDisplay.gameObject.SetActive(true);
                EventDisplay.eventDisplay.SetEvent(currentEvent_);
                EventDisplay.eventDisplay.Display();
            }

            // Event Active flag is now true
            eventActive_ = true;
        //}
    }

    bool GetRandomEvent(List<Event> eventList)
    {

        // If there are events in the list
        if (eventList.Count > 0)
        {

            // Get a random event in the list
            int index = Random.Range(0, eventList.Count);

            // Set that to the current event
            currentEvent_ = eventList[index];

            // Remove the event from the list
            eventList.RemoveAt(index);
            return true;
        }

        // If no event is found return false
        return false;
    }

    bool GetNextEvent(List<Event> eventList)
    {

        // If there are events in the list
        if (eventList.Count > 0)
        {

            // Get next event in the list
            currentEvent_ = eventList[0];

            // Remove the event from the list
            eventList.RemoveAt(0);
            return true;
        }

        // If not event is found return false
        return false;
    }


    // Method for ending an event
    public void EndEvent()
    {
        // Set that no event is active and make event display not active
        eventActive_ = false;
        EventDisplay.eventDisplay.gameObject.SetActive(false);

        // If the event was the last intro event
        if (introductionEvents_.Count <= 0) 
        {

            // Start the timer between events
            nextEventTimer_.Reset();
            nextEventTimer_.SetActive(true);
        }
    }


    public bool EventActive()
    {
        return eventActive_;
    }
}
