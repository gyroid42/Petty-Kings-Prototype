using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventAction : BaseAction {

    // properties
    public NarrativeEvent event_;

    private EventController eventController;

    // Called at start of action
    public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);

        eventController = EventController.eventController;

        eventController.StartEvent(event_);
        actionRunning_ = false;
    }

    // Called when action ends
    public override void End()
    {
        
    }

    // Called every frame the action is active
    public override bool Update()
    {




        return actionRunning_;
    }
}
