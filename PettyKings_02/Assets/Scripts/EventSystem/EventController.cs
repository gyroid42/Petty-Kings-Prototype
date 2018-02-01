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

    // Current event
    public Event currentEvent_;

    // Current Season
    public Season currentSeason_;
    public Season[] seasonList_ = new Season[6] { Season.INTRO, Season.SPRING, Season.SUMMER, Season.AUTUMN, Season.WINTER, Season.SPRING2 };

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

        introductionEvents_ = new List<Event>(Resources.LoadAll("Event/Introduction", typeof(Event)).Cast<Event>().ToArray());
        springEventList_ = new List<Event>(Resources.LoadAll("Events/Spring", typeof(Event)).Cast<Event>().ToArray());
        summerEventList_ = new List<Event>( Resources.LoadAll("Events/Summer", typeof(Event)).Cast<Event>().ToArray());
        autumnEventList_ = new List<Event>(Resources.LoadAll("Events/Autumn", typeof(Event)).Cast<Event>().ToArray());
        winterEventList_ = new List<Event>(Resources.LoadAll("Events/Winter", typeof(Event)).Cast<Event>().ToArray());
        springEventList2_ = new List<Event>(Resources.LoadAll("Events/Spring2", typeof(Event)).Cast<Event>().ToArray());

    }


    void Update()
    {

        // debug if mouse button is pressed start a new event
        if (Input.GetMouseButtonDown(0))
        {

            // If no event is active start a new event
            if (!eventActive)
            {
                StartEvent(Season.SUMMER);
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
            ResourceManager.resourceManager.UpdateResources(currentEvent_.GetDecisionResources(choice));
        }
    }

    public void ContinueButtonClicked()
    {

        // When continue button is pressed end the event
        Debug.Log("Event has ended");
        EndEvent();
    }


    // Method for starting a new event
    public void StartEvent(Season season)
    {

        // If no event is currently active
        if (!eventActive)
        {
            bool eventFound = false;
            switch (season)
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
            
            if (!eventFound)
            {
                return;
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

    bool GetRandomEvent(List<Event> eventList)
    {
        if (eventList.Count > 0)
        {
            int index = Random.Range(0, eventList.Count);
            currentEvent_ = eventList[index];
            eventList.RemoveAt(index);
            return true;
        }

        return false;
    }

    bool GetNextEvent(List<Event> eventList)
    {
        if (eventList.Count > 0)
        {
            currentEvent_ = eventList[0];
            eventList.RemoveAt(0);
            return true;
        }

        return false;
    }


    // Method for ending an event
    public void EndEvent()
    {
        // Set that no event is active and make event display not active
        eventActive = false;
        EventDisplay.eventDisplay.gameObject.SetActive(false);
    }

}
