using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pause Action", menuName = "EventActions/Pause")]
public class PauseAction : BaseAction
{
    // properties
    public float pauseLength_;

    private Timer timer_;

    // Begin method called when action starts
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.PAUSE;

        timer_ = new Timer();
        timer_.SetTimer(pauseLength_);
    }


    // End method called when action finishes
    public override void End()
    {



    }


    // Called every frame the action is happening
    public override bool Update()
    {


        if (timer_.UpdateTimer())
        {
            actionRunning_ = false;
        }

        return actionRunning_;
    }
}
