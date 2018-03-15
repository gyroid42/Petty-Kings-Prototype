using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideEventAction : BaseAction {

    // Properties
    public Event overrideEvent_;

    private EventController eventController;

    // Called at start of action
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

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
