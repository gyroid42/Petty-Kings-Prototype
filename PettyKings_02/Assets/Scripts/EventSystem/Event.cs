using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject {


    // Reference to event controller
    EventController eventController;

    // Event blocking flag
    public bool isBlocking_ = true;

    // Event ignore blocking flag
    public bool instantPriority_ = false;

    // Event pauses seasonTimer
    public bool stopSeasonTimer_ = true;

    // List of actions in event
    public List<BaseAction> actionList_;
    private List<BaseAction> runTimeActionList_;

    // Current actions happening
    private List<BaseAction> activeActions_;

    // Index of currentAction
    private int actionIndex_;

    // Bool for ending an event
    private bool eventRunning_ = true;

    // Flag for actions currently Blocking
    private bool blocked_ = false;

    // Flag if event is from eventPool
    public bool isPooled_ = false;


    // Called when event starts
	public void Begin()
    {
        // Set reference to event controller
        eventController = EventController.eventController;

        // Set default values
        actionIndex_ = -1;
        eventRunning_ = true;
        blocked_ = false;

        // Initialse runtime list of actions
        runTimeActionList_ = new List<BaseAction>(actionList_);

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
        if (isPooled_)
        {
            //eventController.StartEventFromPool();
        }

        foreach (BaseAction action in activeActions_)
        {
            action.End();
        }
    }


    // Method to start next action
    void GotoNextAction()
    {

        while (true)
        {


            // If there's still more actions to do
            if (actionIndex_ + 1 < runTimeActionList_.Count)
            {
                // Increment action index to next action
                actionIndex_++;

                // Set current action to next action
                BaseAction nextAction = runTimeActionList_[actionIndex_];

                nextAction.Begin(this);
                activeActions_.Add(nextAction);

                if (nextAction.isBlocking_)
                {
                    blocked_ = true;
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

                if (eventRunning_)
                {
                    // If no event is blocking stop blocking
                    if (!CheckEventsBlocking())
                    {
                        blocked_ = false;
                        GotoNextAction();
                    }

                    // If no more actions in active list
                    if (activeActions_.Count <= 0)
                    {
                        blocked_ = false;
                        // Goto next action and Break out of loop
                        GotoNextAction();
                        break;
                    }
                }
            }

            // Else move index to next active action
            else
            {
                i++;
            }
        }
    }

    private bool CheckEventsBlocking()
    {

        // Loop for each activeAction
        for (int i = 0; i < activeActions_.Count; i++)
        {

            // If action is blocking return true
            if (activeActions_[i].isBlocking_)
            {
                return true;
            }
        }

        // No action was blocking
        return false;
    }

    public void EndEvent()
    {
        eventRunning_ = false;
    }


    public bool StartAction(BaseAction newAction)
    {
        if (newAction != null)
        {
            if (blocked_)
            {
                runTimeActionList_.Insert(actionIndex_ + 1, newAction);
                Debug.Log(runTimeActionList_);
                return false;
            }
            else
            {
                newAction.Begin(this);
                activeActions_.Add(newAction);
                if (newAction.isBlocking_)
                {
                    blocked_ = true;
                }
            }
        }

        return true;
    }

    public bool StartAction(List<BaseAction> newActions)
    {
        if (newActions != null)
        {
            int indexPos = 0;

            for (int i = 0; i < newActions.Count; i++)
            {

                if (!newActions[i])
                {
                    continue;
                }

                if (blocked_)
                {
                    indexPos++;
                    runTimeActionList_.Insert(actionIndex_ + indexPos, newActions[i]);
                }
                else
                {
                    newActions[i].Begin(this);
                    activeActions_.Add(newActions[i]);
                    if (newActions[i].isBlocking_)
                    {
                        blocked_ = true;
                    }
                }

            }
        }

        return true;
    }

}
