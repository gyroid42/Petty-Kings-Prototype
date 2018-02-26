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

    // References to event display and resource manager
    private EventController eventController;
    private EventDisplay eventDisplay;
    private ResourceManager resourceManager;


    // Called at start of action
    public override void Begin(Event newEvent)
    {
        // Call Base Begin Method
        base.Begin(newEvent);

        // Set action type
        type_ = ACTIONTYPE.DECISION;

        // Create references to eventDisplay and resourceManager
        eventDisplay = EventDisplay.eventDisplay;
        resourceManager = ResourceManager.resourceManager;

        // If there are decisions
        if (decisionEffect_.Length > 0)
        {
            // Create btnFunction list
            mainDisplay_.btnFunctions_ = new ButtonDel[decisionEffect_.Length];
            for (int i = 0; i < mainDisplay_.btnFunctions_.Length; i++)
            {

                mainDisplay_.btnFunctions_[i] += UpdateStars;

                if (decisionEffect_[i].playSound_)
                {
                    mainDisplay_.btnFunctions_[i] += PlaySound;
                }

                // Set button functions to DecisionSelected(int choice) method
                if (decisionEffect_[i].displayScreen_)
                {
                    mainDisplay_.btnFunctions_[i] += DecisionSelected;
                    decisionEffect_[i].decisionDisplayData_.btnFunctions_ = new ButtonDel[1] { ContinuePressed };
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

        return actionRunning_;
    }


    public void PlaySound(int choice)
    {

        FMODUnity.RuntimeManager.PlayOneShot(decisionEffect_[choice].sound_, Camera.main.transform.position);
    }

    public void UpdateStars(int choice)
    {

        // starmanager update stars ( getDecisionStars(choice))
    }

    // Method for decision events
    public void DecisionSelected(int choice)
    {

        // If event Display exists
        if (eventDisplay != null)
        {
            // Display choice made
            eventDisplay.Display(decisionEffect_[choice].decisionDisplayData_);

            // Update resources from decision made
            //resourceManager.UpdateResources(GetDecisionResources(choice));
        }

    }


    // Method for continuing to next action
    public void ContinuePressed(int choice)
    {
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
        return decisionEffect_[choice].starChange_;
    }

}
