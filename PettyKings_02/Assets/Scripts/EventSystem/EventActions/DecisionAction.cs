using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decision Action", menuName = "EventActions/DecisionAction")]
public class DecisionAction : BaseAction
{

    // All data required for an event

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
            for (int i = 0; i < mainDisplay_.btnFunctions_.Length; i++)
            {

                mainDisplay_.btnFunctions_[i] += UpdateStars;

                if (decisionEffect_[i].addEvent_)
                {
                    mainDisplay_.btnFunctions_[i] += AddEventToPool;
                }

                if (decisionEffect_[i].playSound_)
                {
                    mainDisplay_.btnFunctions_[i] += PlaySound;
                }

                // Set button functions to DecisionSelected(int choice) method
                /*if (decisionEffect_[i].displayScreen_)
                {
                    mainDisplay_.btnFunctions_[i] += DecisionSelected;
                    decisionEffect_[i].decisionDisplayData_.btnFunctions_ = new ButtonDel[1] { ContinuePressed };
                }
                */
                if (decisionEffect_[i].effects_.Count > 0)
                {
                    mainDisplay_.btnFunctions_[i] += StartActionList;
                }
                else
                {
                    mainDisplay_.btnFunctions_[i] += ContinuePressed;
                }
            }

            
        }
        else
        {

            // Else no decisions, set button function to ContinuePressed()
            mainDisplay_.btnFunctions_ = new ButtonDel[1] { ContinuePressed };
        }

        
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
            //timerFinished += DecisionSelected;
            //timerRanOutEffect_.decisionDisplayData_.btnFunctions_ = new ButtonDel[1] { ContinuePressed };
        }
        else
        {
            timerFinished += ContinuePressed;
        }



        // Make event display active
        eventDisplay.gameObject.SetActive(true);

        // Display main
        eventDisplay.Display(mainDisplay_);

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
        if (actionRunning_)
        {
            eventDisplay.gameObject.SetActive(true);
        }
        else
        {
            eventDisplay.gameObject.SetActive(false);
        }

        if (decisionTimer_.UpdateTimer())
        {
            Debug.Log("timer finished");
            timerFinished(-1);
        }

        return actionRunning_;
    }


    public void AddEventToPool(int choice)
    {
        if (choice < 0)
        {
            eventController.AddEventToPool(timerRanOutEffect_.newEvent_);
        }

        eventController.AddEventToPool(decisionEffect_[choice].newEvent_);
    }

    public void PlaySound(int choice)
    {
        if (choice < 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(timerRanOutEffect_.sound_, Camera.main.transform.position);
        }
        FMODUnity.RuntimeManager.PlayOneShot(decisionEffect_[choice].sound_, Camera.main.transform.position);
    }

    public void UpdateStars(int choice)
    {

        // starmanager update stars ( getDecisionStars(choice))
    }


    public void StartActionList(int choice)
    {


        List<BaseAction> actionList;
        if (choice < 0)
        {
            actionList = timerRanOutEffect_.effects_;
        }
        else
        {
            actionList = decisionEffect_[choice].effects_;
        }

        for (int i = 0; i < actionList.Count; i++)
        {
            currentEvent_.StartAction(actionList[i]);
        }

        Debug.Log("finished adding actions");
        decisionTimer_.SetActive(false);
        //eventController.StartCoroutine(StartingActions(choice));
        actionRunning_ = false;
    }


    IEnumerator StartingActions(int choice)
    {

        List<BaseAction> actionList;

        if (choice < 0)
        {
            actionList = timerRanOutEffect_.effects_;
        }
        else
        {
            actionList = decisionEffect_[choice].effects_;
        }


        int actionStarted = 0;
        while (actionStarted < actionList.Count)
        {
            for (int i = actionStarted; i < actionList.Count; i++)
            {

                if (currentEvent_.StartAction(actionList[i]))
                {
                    actionStarted++;
                }
                else
                {
                    actionStarted++;
                    //break;
                }
            }


            yield return null;
        }

    }

    // Method for decision events
    public void DecisionSelected(int choice)
    {

        // If event Display exists
        if (eventDisplay != null)
        {

            timerFinished = ContinuePressed;

            if (choice < 0)
            {
                eventDisplay.Display(timerRanOutEffect_.decisionDisplayData_);
                SetDisplayTimer(timerRanOutEffect_.decisionDisplayData_.timerLength_);
                return;
            }
            // Display choice made
            eventDisplay.Display(decisionEffect_[choice].decisionDisplayData_);

            SetDisplayTimer(decisionEffect_[choice].decisionDisplayData_.timerLength_);            
        }

    }


    // Method for continuing to next action
    public void ContinuePressed(int choice)
    {

        //Debug.Log("continue has been pressed");
        // Set actionRunning_ to false to end the action
        actionRunning_ = false;
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
