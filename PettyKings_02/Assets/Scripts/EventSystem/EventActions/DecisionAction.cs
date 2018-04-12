using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decision Action", menuName = "EventActions/DecisionAction")]
public class DecisionAction : BaseAction
{

    // All data required for an event

    private WorldManager worldController;
    private UserKeyboardControls keyboardControls;

    // Data for displaying initial event i.e. name, description...
    // Main display data
    public EventDisplayData mainDisplay_;

    // Decision display data
    public DecisionEffect[] decisionEffect_;

    // Timer ran out effect
    public DecisionEffect timerRanOutEffect_;

    // Timer ran out function pointer
    private ButtonDel timerFinished;

    // References to event display and resource manager
    private EventController eventController;
    private EventDisplay eventDisplay;

    // Timer for switching event
    private Timer decisionTimer_;


    // Called at start of action
    public override void Begin(Event newEvent)
    {
        // Call Base Begin Method
        base.Begin(newEvent);

        // Set action type
        type_ = ACTIONTYPE.DECISION;

        //get the world manager
        worldController = Terrain.activeTerrain.GetComponent<WorldManager>(); //get script
        keyboardControls = UserKeyboardControls.userKeyboardControls;

        // Create references to eventDisplay and resourceManager
        eventDisplay = EventDisplay.eventDisplay;
        eventController = EventController.eventController;

        // Start decision timer
        decisionTimer_ = new Timer();
        SetDisplayTimer(mainDisplay_.timerLength_);

        // If there are decisions
        if (decisionEffect_.Length > 0)
        {
            // Create btnFunction list
            mainDisplay_.btnFunctions_ = new ButtonDel[decisionEffect_.Length];

            // For each button
            for (int i = 0; i < mainDisplay_.btnFunctions_.Length; i++)
            {
                
                // Create all effects when button is pressed

                // Add update star method
                mainDisplay_.btnFunctions_[i] += UpdateStars;


                // If there's an event to add
                if (decisionEffect_[i].addEvent_)
                {

                    // Add the AddEventToPool method
                    mainDisplay_.btnFunctions_[i] += AddEventToPool;
                }

                // If there's sound to play
                if (decisionEffect_[i].playSound_)
                {

                    // Add play sound method
                    mainDisplay_.btnFunctions_[i] += PlaySound;
                }

                // if decision has a list of extra actions
                if (decisionEffect_[i].effects_.Count > 0)
                {
                    // Add StartActionList method
                    mainDisplay_.btnFunctions_[i] += StartActionList;
                }

                // Add Exit Decision Method
                mainDisplay_.btnFunctions_[i] += ExitDecision;
                
            }

            
        }
        else
        {

            // Else no decisions, set button function to ContinuePressed()
            mainDisplay_.btnFunctions_ = new ButtonDel[1] { ExitDecision };
        }

        
        // Set up effect when timer runs out
        timerFinished = UpdateStars;

        if (timerRanOutEffect_.addEvent_)
        {
            timerFinished += AddEventToPool;
        }

        if (timerRanOutEffect_.playSound_)
        {
            timerFinished += PlaySound;
        }

        if (timerRanOutEffect_.effects_.Count > 0)
        {
            timerFinished += StartActionList;
        }
        else
        {
            timerFinished += ExitDecision;
        }



        // Make event display active
        eventDisplay.gameObject.SetActive(true);

        // Display main
        eventDisplay.Display(mainDisplay_, decisionTimer_);

        if (keyboardControls)
        {
            keyboardControls.SetButtons(mainDisplay_.btnFunctions_);
        }
    }


    // Called at end of action
    public override void End()
    {
        eventDisplay.gameObject.SetActive(false);
    }


    // Called every frame action is active
    public override bool Update()
    {

        // QUICK FIX FOR WEIRD BUG (SHOULD REMOVE THIS CODE WHEN BUG IS FIXED)
        /*
        if (actionRunning_)
        {
            eventDisplay.gameObject.SetActive(true);
        }
        else
        {
            eventDisplay.gameObject.SetActive(false);
        }
        */

        // If timer has finished
        if (decisionTimer_.UpdateTimer())
        {

            // Call timer finished method
            Debug.Log("timer finished");
            timerFinished(-1);
        }

        eventDisplay.UpdateTimerBar(decisionTimer_.TimerPercentage());

        return actionRunning_;
    }


    // Method to add new Event to pool
    public void AddEventToPool(int choice)
    {

        // If choice was the timer running out
        if (choice < 0)
        {
            // Add timerRanOutEffect's new event
            eventController.AddEventToPool(timerRanOutEffect_.newEvent_);
            return;
        }

        // Add decision chosen's new event to the event pool
        eventController.AddEventToPool(decisionEffect_[choice].newEvent_);
    }


    // Method to play a sound
    public void PlaySound(int choice)
    {

        // If choice was the timer running out
        if (choice < 0)
        {
            // Play sound from timer ran out effect
            FMODUnity.RuntimeManager.PlayOneShot(timerRanOutEffect_.sound_);
            return;
        }

        // Play sound from decision chosen
        FMODUnity.RuntimeManager.PlayOneShot(decisionEffect_[choice].sound_);
    }


    // Updates star count
    public void UpdateStars(int choice)
    {
        if (choice < 0)
        {
            worldController.UpdateStars(timerRanOutEffect_.starChange_);
            return;
        }
        worldController.UpdateStars(decisionEffect_[choice].starChange_);
    }

    // Method to start effects from effect list
    public void StartActionList(int choice)
    {
        // Create action list
        List<BaseAction> actionList;

        // Get correct action list from choice
        if (choice < 0)
        {
            actionList = timerRanOutEffect_.effects_;
        }
        else
        {
            actionList = decisionEffect_[choice].effects_;
        }


        currentEvent_.StartAction(actionList);
        // Loop for each action in the list and start it
        //for (int i = 0; i < actionList.Count; i++)
        //{
        //    currentEvent_.StartAction(actionList[i]);
        //}

    }
    


    // Method for continuing to next action
    public void ExitDecision(int choice)
    {

        // Set actionRunning_ to false to end the action
        decisionTimer_.SetActive(false);
        actionRunning_ = false;
        eventDisplay.DisplayEnd();
    }


    // Returns Resources from a choice
    public float GetDecisionStars(int choice)
    {
        if (choice >= decisionEffect_.Length)
        {
            return 0f;
        }

        if (choice < 0)
        {
            return timerRanOutEffect_.starChange_;
        }
        return decisionEffect_[choice].starChange_;
    }


    private void SetDisplayTimer(float timerLength)
    {
        if (timerLength <= 0)
        {
            timerLength = eventDisplay.defaultTimerLength_;
        }

        decisionTimer_.SetTimer(timerLength);
    }

}
