using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject {

    // List of actions in event
    public List<EventAction> actionList_;

    // Current action happening
    EventAction currentAction_ = null;

    // Index of currentAction
    int actionIndex_;

    // Bool for ending an event
    bool eventRunning_ = true;


    // Called when event starts
	public void Begin()
    {
        // Set default values
        actionIndex_ = -1;
        currentAction_ = null;
        eventRunning_ = true;

        // Start next action
        GotoNextAction();
    }

    // Called every frame event is happening
    public bool Update()
    {

        // Update currentAction
        if (!currentAction_.Update())
        {

            // If action is finished start next action
            GotoNextAction();
        }

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
        // If current action exists
        if (currentAction_ != null)
        {

            // End current action
            currentAction_.End();
            currentAction_ = null;
        }

        // Increment action index to next action
        actionIndex_++;

        // If there's still more actions to do
        if (actionIndex_ < actionList_.Count)
        {
            actionIndex_ %= actionList_.Count;

            // Set current action to next action
            currentAction_ = actionList_[actionIndex_];
            
            // Start next action
            currentAction_.Begin(this);
        }
        else
        {

            // Else no more actions left, end the event
            eventRunning_ = false;
        }

    }

}
