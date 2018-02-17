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
    private StateManager stateManager;

    // List of events
    private List<DecisionAction> introductionEvents_;
    private List<DecisionAction> springEventList_;
    private List<DecisionAction> summerEventList_;
    private List<DecisionAction> autumnEventList_;
    private List<DecisionAction> harvestEventList_;
    private List<DecisionAction> winterEventList_;
    private List<DecisionAction> springEventList2_;


    private List<DecisionAction> springIntroEvents_;
    private List<DecisionAction> summerIntroEvents_;
    private List<DecisionAction> autumnIntroEvents_;
    private List<DecisionAction> harvestIntroEvents_;
    private List<DecisionAction> winterIntroEvents_;
    private List<DecisionAction> spring2IntroEvents_;


    // Current event
    private DecisionAction currentEvent_;


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




}
