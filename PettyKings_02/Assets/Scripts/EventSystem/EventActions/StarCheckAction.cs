using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StarCheck", menuName = "EventActions/StarCheck")]
public class StarCheckAction : BaseAction {

    // Properties

    // Reference to worldController to check star rating
    private WorldManager worldController;

    // Value check will be tested against
    public int testValue_;

    // Action List for each result
    public List<BaseAction> aboveActions_;
    public List<BaseAction> belowActions_;
    
    // Called at start of action
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.STARCHECK;

        // Check star rating and get action List being used
        List<BaseAction> activeActions_ = null;
        if (worldController.starRating >= testValue_)
        {
            activeActions_ = new List<BaseAction>(aboveActions_);
        }
        else
        {
            activeActions_ = new List<BaseAction>(belowActions_);
        }

        // Start that list of actions
        currentEvent_.StartAction(activeActions_);

        // End the action
        actionRunning_ = false;
    }


    // Called at end of action
    public override void End()
    {
        
    }


    // Called every frame action is active
    public override bool Update()
    {
        return actionRunning_;
    }
}
