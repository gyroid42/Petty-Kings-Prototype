using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeasonController : MonoBehaviour {


    public static SeasonController seasonController;


    private StateManager stateManager;
    private EventController eventController;
    private SeasonAudioManager seasonAudioManager;

    // List of events
    private List<Event> introductionEvents_;
    private List<Event> springEventList_;
    private List<Event> summerEventList_;
    private List<Event> autumnEventList_;
    private List<Event> harvestEventList_;
    private List<Event> winterEventList_;
    private List<Event> spring2EventList_;



    private List<Event> springIntroEvents_;
    private List<Event> summerIntroEvents_;
    private List<Event> autumnIntroEvents_;
    private List<Event> harvestIntroEvents_;
    private List<Event> winterIntroEvents_;
    private List<Event> spring2IntroEvents_;


    // Current Season
    private int currentSeason_;
    private Season[] seasonList_ = new Season[7] { Season.INTRO, Season.SPRING, Season.SUMMER, Season.AUTUMN, Season.HARVEST, Season.WINTER, Season.SPRING2 };


    private Timer seasonTimer_ = new Timer();
    public float timeTillNextSeason_;


    // When object is created
    void Awake()
    {

        // Check if an seasonController already exists
        if (seasonController == null)
        {

            // If not set the static reference to this object
            seasonController = this;
        }
        else if (seasonController != this)
        {

            // Else a different seasonController already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {

        // when destroyed remove static reference to itself
        seasonController = null;
    }

    // Use this for initialization
    void Start () {

        // Create reference to stateManager/eventController and SeasonAudioManager
        stateManager = StateManager.stateManager;
        eventController = EventController.eventController;
        seasonAudioManager = SeasonAudioManager.seasonAudioManager;

        // Load all the events into the event lists
        introductionEvents_ = new List<Event>(Resources.LoadAll("Events/Introduction", typeof(Event)).Cast<Event>().ToArray());
        springEventList_ = new List<Event>(Resources.LoadAll("Events/Spring/Random", typeof(Event)).Cast<Event>().ToArray());
        summerEventList_ = new List<Event>(Resources.LoadAll("Events/Summer/Random", typeof(Event)).Cast<Event>().ToArray());
        autumnEventList_ = new List<Event>(Resources.LoadAll("Events/Autumn/Random", typeof(Event)).Cast<Event>().ToArray());
        harvestEventList_ = new List<Event>(Resources.LoadAll("Events/Harvest/Random", typeof(Event)).Cast<Event>().ToArray());
        winterEventList_ = new List<Event>(Resources.LoadAll("Events/Winter/Random", typeof(Event)).Cast<Event>().ToArray());
        spring2EventList_ = new List<Event>(Resources.LoadAll("Events/Spring2/Random", typeof(Event)).Cast<Event>().ToArray());


        springIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Spring/Intro", typeof(Event)).Cast<Event>().ToArray());
        summerIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Summer/Intro", typeof(Event)).Cast<Event>().ToArray());
        autumnIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Autumn/Intro", typeof(Event)).Cast<Event>().ToArray());
        harvestIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Harvest/Intro", typeof(Event)).Cast<Event>().ToArray());
        winterIntroEvents_ = new List<Event>(Resources.LoadAll("Events/Winter/Intro", typeof(Event)).Cast<Event>().ToArray());
        spring2IntroEvents_ = new List<Event>(Resources.LoadAll("Events/Spring2/Intro", typeof(Event)).Cast<Event>().ToArray());

        // Setup Event Timer
        seasonTimer_.SetTimer(timeTillNextSeason_, false);

        // Set starting season to the intro
        currentSeason_ = 0;
    }

    // Called when GameStarts
    public void StartGame()
    {
        // Start introduction events
        currentSeason_ = 0;
        StartSeasonEvents();
    }
	
	// Update is called once per frame
	void Update () {
		
        // If game is in the first stage
        if (stateManager.CurrentState() == GAMESTATE.STAGEONE)
        {

            // Update timer till next season
            if (seasonTimer_.UpdateTimer())
            {
                // If timer finished Goto next season
                Debug.Log("Season Change");
                ChangeSeason();
            }
        }
        
        
	}



    private void ChangeSeason()
    {
        // Increment currentSeason Index
        currentSeason_++;
        currentSeason_ %= seasonList_.Length;

        // Start next Seasons events
        StartSeasonEvents();

        // Update season audio
        seasonAudioManager.UpdateAudio();

        // Reset the timer
        seasonTimer_.Reset();
        
    }


    // Starts events for a season
    private void StartSeasonEvents()
    {
        Debug.Log("starting season event");

        // Createa lists for intro and random events
        List<Event> introEvents = null;
        List<Event> randomEventList = null;


        // Get event lists for current season
        switch (seasonList_[currentSeason_])
        {
            case Season.INTRO:
                Debug.Log("intro is selcted");
                introEvents = introductionEvents_;
                randomEventList = null;
                break;
            case Season.SPRING:
                introEvents = springIntroEvents_;
                randomEventList = springEventList_;
                break;
            case Season.SUMMER:
                introEvents = summerIntroEvents_;
                randomEventList = summerEventList_;
                break;
            case Season.AUTUMN:
                introEvents = autumnIntroEvents_;
                randomEventList = autumnEventList_;
                break;
            case Season.HARVEST:
                introEvents = harvestIntroEvents_;
                randomEventList = harvestEventList_;
                break;
            case Season.WINTER:
                introEvents = winterIntroEvents_;
                randomEventList = winterEventList_;
                break;
            case Season.SPRING2:
                introEvents = spring2IntroEvents_;
                randomEventList = spring2EventList_;
                break;
        }

        // if there is an intro event list
        if (introEvents != null)
        {
            Debug.Log("starting each intro event");

            // Start an event for each item in intro Events in order
            for (int i = 0; i < introEvents.Count; i++)
            {
                // If introEvents[i] exists
                if (introEvents[i] != null)
                {
                    eventController.StartEvent(introEvents[i]);
                }
            }
        }

        // If there is a random event list
        if (randomEventList != null)
        {
            // Get a random event
            Event randomEvent = GetRandomEvent(randomEventList, true);

            // If random event exists
            if (randomEvent != null)
            {
                // Start RandomEvent
                eventController.StartEvent(randomEvent);
            }
        }
    }


    // Gets a random event from a list of events
    private Event GetRandomEvent(List<Event> eventList, bool remove = false)
    {
        Event newEvent = null;

        // If list is empty exit method
        if (eventList.Count <= 0)
        {
            return newEvent;
        }

        // Get random int within range of eventLists size
        int index = Random.Range(0, eventList.Count);

        // newEvent = event at random index
        newEvent = eventList[index];

        // If remove is true remove the event from the list
        if (remove)
        {
            eventList.RemoveAt(index);
        }

        return newEvent;
    }


    // Pauses timer for next season
    public void PauseTimer()
    {
        seasonTimer_.Pause();
    }

    // Starts timer for next season
    public void StartTimer()
    {
        seasonTimer_.Start();
    }

    // Getter for current season
    public Season CurrentSeason()
    {
        return seasonList_[currentSeason_];
    }
}
