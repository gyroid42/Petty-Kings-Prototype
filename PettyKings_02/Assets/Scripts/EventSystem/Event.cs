using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject {

    // List of actions in event
    public List<BaseAction> actionList_;

    // Current actions happening
    private List<BaseAction> activeActions_;

    // Index of currentAction
    int actionIndex_;

    // Bool for ending an event
    bool eventRunning_ = true;


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

        while (actionList_[actionList_.Count - 1])
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

    void UpdateActions()
    {

        for (int i = 0; i < activeActions_.Count; )
        {
            if (!activeActions_[i].Update())
            {
                if (activeActions_[i] != null)
                {
                    activeActions_[i].End();
                    activeActions_[i] = null;
                }
                
                activeActions_.RemoveAt(i);

                if (activeActions_.Count <= 0)
                {
                    GotoNextAction();
                    break;
                }
            }
            else
            {
                i++;
            }
        }
        

    }

}
