﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeasonController : MonoBehaviour {


    public static SeasonController seasonController;


    private StateManager stateManager;
    private EventController eventController;
    private SeasonAudioManager seasonAudioManager;

    // List of events
    private List<NarrativeEvent> introductionEvents_;
    private List<NarrativeEvent> springEventList_;
    private List<NarrativeEvent> summerEventList_;
    private List<NarrativeEvent> autumnEventList_;
    private List<NarrativeEvent> harvestEventList_;
    private List<NarrativeEvent> winterEventList_;
    private List<NarrativeEvent> spring2EventList_;



    private List<NarrativeEvent> springIntroEvents_;
    private List<NarrativeEvent> summerIntroEvents_;
    private List<NarrativeEvent> autumnIntroEvents_;
    private List<NarrativeEvent> harvestIntroEvents_;
    private List<NarrativeEvent> winterIntroEvents_;
    private List<NarrativeEvent> spring2IntroEvents_;


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
        if (seasonController == this)
        {
            // when destroyed remove static reference to itself
            seasonController = null;
        }
    }

    // Use this for initialization
    void Start () {

        // Create reference to stateManager/eventController and SeasonAudioManager
        stateManager = StateManager.stateManager;
        eventController = EventController.eventController;
        seasonAudioManager = SeasonAudioManager.seasonAudioManager;

        // Load all the events into the event lists
        introductionEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Introduction", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        springEventList_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Spring/Random", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        summerEventList_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Summer/Random", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        autumnEventList_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Autumn/Random", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        harvestEventList_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Harvest/Random", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        winterEventList_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Winter/Random", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        spring2EventList_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Spring2/Random", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());


        springIntroEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Spring/Intro", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        summerIntroEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Summer/Intro", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        autumnIntroEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Autumn/Intro", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        harvestIntroEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Harvest/Intro", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        winterIntroEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Winter/Intro", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());
        spring2IntroEvents_ = new List<NarrativeEvent>(Resources.LoadAll("Events/Spring2/Intro", typeof(NarrativeEvent)).Cast<NarrativeEvent>().ToArray());

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
        //StartSeasonEvents();
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
       
        // Createa lists for intro and random events
        List<NarrativeEvent> introEvents = null;
        List<NarrativeEvent> randomEventList = null;


        // Get event lists for current season
        switch (seasonList_[currentSeason_])
        {
            case Season.INTRO:
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
            NarrativeEvent randomEvent = GetRandomEvent(randomEventList, true);

            // If random event exists
            if (randomEvent != null)
            {
                // Start RandomEvent
                eventController.StartEvent(randomEvent);
            }
        }
    }


    // Gets a random event from a list of events
    private NarrativeEvent GetRandomEvent(List<NarrativeEvent> eventList, bool remove = false)
    {
        NarrativeEvent newEvent = null;

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
