﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject {


    // Event blocking flag
    public bool isBlocking_ = true;

    // Event ignore blocking flag
    public bool instantPriority_ = false;

    // Event pauses seasonTimer
    public bool stopSeasonTimer_ = true;

    // List of actions in event
    public List<BaseAction> actionList_;

    // Current actions happening
    private List<BaseAction> activeActions_;

    // Index of currentAction
    private int actionIndex_;

    // Bool for ending an event
    private bool eventRunning_ = true;


    // Called when event starts
	public void Begin()
    {
        // Set default values
        actionIndex_ = -1;
        eventRunning_ = true;

        // Initialse active actions list
        activeActions_ = new List<BaseAction>();

        // Start next action
        GotoNextAction();
    }

    // Called every frame event is happening
    public bool Update()
    {

        // Update currentAction
        UpdateActions();

        return eventRunning_;
    }

    // Called when event ends
    public void End()
    {
        Debug.Log("event is ending");
    }


    // Method to start next action
    void GotoNextAction()
    {

        while (true)
        {
            // Increment action index to next action
            actionIndex_++;

            // If there's still more actions to do
            if (actionIndex_ < actionList_.Count)
            {

                // Set current action to next action
                BaseAction nextAction = actionList_[actionIndex_];

                nextAction.Begin(this);
                activeActions_.Add(nextAction);

                if (nextAction.isBlocking_)
                {
                    break;
                }
                
            }
            else if (activeActions_.Count <= 0)
            {

                // Else no more actions left, end the event
                eventRunning_ = false;
                break;
            }
            else
            {
                break;
            }
        }

    }


    // Updates each active action
    void UpdateActions()
    {

        // Loop for each active action and update it
        for (int i = 0; i < activeActions_.Count; )
        {

            // If the action has ended
            if (!activeActions_[i].Update())
            {

                // End the action

                // If the action exists
                if (activeActions_[i] != null)
                {

                    // Call end on action then delete it
                    activeActions_[i].End();
                    activeActions_[i] = null;
                }
                
                // Remove the action from the active list
                activeActions_.RemoveAt(i);

                // If no more actions in active list
                if (activeActions_.Count <= 0)
                {

                    // Goto next action and Break out of loop
                    GotoNextAction();
                    break;
                }
            }

            // Else move index to next active action
            else
            {
                i++;
            }
        }
    }

}
