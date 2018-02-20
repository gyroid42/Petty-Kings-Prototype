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
    public EventDisplayData[] decisionDisplay_;

    // Modifiers for resources
    public int[] decisionFood_ = new int[2];
    public int[] decisionWood_ = new int[2];
    public int[] decisionMen_ = new int[2];

    // References to event display and resource manager
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
        if (decisionDisplay_.Length > 0)
        {
            // Create btnFunction list
            mainDisplay_.btnFunctions_ = new ButtonDel[decisionDisplay_.Length];
            for (int i = 0; i < mainDisplay_.btnFunctions_.Length; i++)
            {

                // Set button functions to DecisionSelected(int choice) method
                mainDisplay_.btnFunctions_[i] = DecisionSelected;
            }

            // Set decision Display button functions
            for (int i = 0; i < decisionDisplay_.Length; i++)
            {

                // Set button functions to ContinuePresseed(int choice) method
                decisionDisplay_[i].btnFunctions_ = new ButtonDel[1] { ContinuePressed };
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

        // Make event display not active
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


    // Method for decision events
    public void DecisionSelected(int choice)
    {

        // If event Display exists
        if (eventDisplay != null)
        {
            // Display choice made
            eventDisplay.Display(decisionDisplay_[choice]);

            // Update resources from decision made
            resourceManager.UpdateResources(GetDecisionResources(choice));
        }

    }


    // Method for continuing to next action
    public void ContinuePressed(int choice)
    {
        // Set actionRunning_ to false to end the action
        actionRunning_ = false;
    }


    // Returns Resources from a choice
    public int[] GetDecisionResources(int choice)
    {
        int[] resources = new int[3];
        resources[0] = decisionFood_[choice];
        resources[1] = decisionWood_[choice];
        resources[2] = decisionMen_[choice];
        return resources;
    }

}
