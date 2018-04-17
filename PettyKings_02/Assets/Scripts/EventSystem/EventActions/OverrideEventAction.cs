using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Override Event", menuName = "EventActions/OverrideEvent")]
public class OverrideEventAction : BaseAction {

    // Properties
    public NarrativeEvent overrideEvent_;

    private EventController eventController;

    // Called at start of action
    public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);

        Debug.Log("Override begin");

        type_ = ACTIONTYPE.OVERRIDEEVENT;

        // Get reference to event controller
        eventController = EventController.eventController;

        // End the current event
        currentEvent_.EndEvent();

        // Start the new event and set instantPriority to true so it starts straight away
        overrideEvent_.instantPriority_ = true;
        eventController.StartEvent(overrideEvent_);

        // End the action
        actionRunning_ = false;

    }

    // Called when action ends
    public override void End()
    {
        
    }

    // Called every frame action is active
    public override bool Update()
    {
        return actionRunning_;
    }
}
