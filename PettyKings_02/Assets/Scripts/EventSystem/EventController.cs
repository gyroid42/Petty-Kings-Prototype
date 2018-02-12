using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static EventController eventController;


    // List of events
    private List<Event> introductionEvents_;
    private List<Event> springEventList_;
    private List<Event> summerEventList_;
    private List<Event> autumnEventList_;
    private List<Event> harvestEventList_;
    private List<Event> winterEventList_;
    private List<Event> springEventList2_;


    private List<Event> springIntroEvents_;
    private List<Event> summerIntroEvents_;
    private List<Event> autumnIntroEvents_;
    private List<Event> harvestIntroEvents_;
    private List<Event> winterIntroEvents_;
    private List<Event> spring2IntroEvents_;


    // Current event
    private Event currentEvent_;

  
    // Current Season
    private int currentSeason_;
    private Season[] seasonList_ = new Season[7] { Season.INTRO, Season.SPRING, Season.SUMMER, Season.AUTUMN, Season.HARVEST, Season.WINTER, Season.SPRING2 };

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
        springEventList_ = new List<Event>(Resources.LoadAll("Events/Spring/Random", typeof(Event)).Cast<Event>().ToArray());
        summerEventList_ = new List<Event>(Resources.LoadAll("Events/Summer/Random", typeof(Event)).Cast<Event>().ToArray());
        autumnEventList_ = new List<Event>(Resources.LoadAll("Events/Autumn/Random", typeof(Event)).Cast<Event>().ToArray());
        harvestEventList_ = new List<Event>(Resources.LoadAll("Events/Harvest/Random", typeof(Event)).Cast<Event>().ToArray());
        winterEventList_ = new List<Event>(Resources.LoadAll("Events/Winter/Random", typeof(Event)).Cast<Event>().ToArray());
        springEventList2_ = new List<Event>(Resources.LoadAll("Events/Spring2/Random", typeof(Event)).Cast<Event>().ToArray());


        springIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Spring/Intro", typeof(Event)).Cast<Event>().ToArray());
        summerIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Summer/Intro", typeof(Event)).Cast<Event>().ToArray());
        autumnIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Autumn/Intro", typeof(Event)).Cast<Event>().ToArray());
        harvestIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Harvest/Intro", typeof(Event)).Cast<Event>().ToArray());
        winterIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Winter/Intro", typeof(Event)).Cast<Event>().ToArray());
        spring2IntroEvents_ = new List<Event>(Resources.LoadAll("Events/Spring2/Intro", typeof(Event)).Cast<Event>().ToArray());

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

                if (!GotoNextIntroEvent())
                {
                    StartEvent();
                }
                
                
            }

            eventActive_ = true;
        }

        GetComponent<SeasonAudioManager>().UpdateAudio(); //updates audio being played
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
        if (GotoNextIntroEvent())
        {
            eventController.GotoNextIntroEvent();
        }
        else
        {
            eventController.StartEvent();
        }
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
                case Season.HARVEST:
                    eventFound = GetRandomEvent(harvestEventList_);
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

    private bool GotoNextIntroEvent()
    {

        bool eventFound = false;

        switch (seasonList_[currentSeason_])
        {
            case Season.SPRING:
                eventFound = GetNextEvent(springIntroEvents_);
                break;
            case Season.SUMMER:
                eventFound = GetNextEvent(summerIntroEvents_);
                break;
            case Season.AUTUMN:
                eventFound = GetNextEvent(autumnIntroEvents_);
                break;
            case Season.HARVEST:
                eventFound = GetNextEvent(harvestIntroEvents_);
                break;
            case Season.WINTER:
                eventFound = GetNextEvent(winterIntroEvents_);
                break;
            case Season.SPRING2:
                eventFound = GetNextEvent(spring2IntroEvents_);
                break;
            case Season.INTRO:
                eventFound = GetNextEvent(introductionEvents_);
                break;

        }

        if (!eventFound)
        {
            return false;
        }

        if (EventDisplay.eventDisplay != null)
        {
            EventDisplay.eventDisplay.gameObject.SetActive(true);
            EventDisplay.eventDisplay.SetEvent(currentEvent_);
            EventDisplay.eventDisplay.DisplaySeasonStart();
        }
        return true;
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

    public int CurrentSeason()
    {
        return currentSeason_;
    }
}
