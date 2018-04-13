using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New EndGame Action", menuName = "EventActions/EndGame")]
public class EndGame : BaseAction {

    // Properties
    private EventDisplay eventDisplay;

    public bool victory_;

    private Timer endTimer_;
    public float endTime_;

    // Begin called at start of action
    public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);

        eventDisplay = EventDisplay.eventDisplay;

        eventDisplay.DisplayVictory(victory_);

        endTimer_ = new Timer();
        endTimer_.SetTimer(endTime_);

        Debug.Log("this is happening");
    }

    // Called at end of action
    public override void End()
    {
        Debug.Log("da end has triggered");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    // Called every frame action is active
    public override bool Update()
    {
        if (endTimer_.UpdateTimer())
        {
            actionRunning_ = false;
        }

        return actionRunning_;
    }
}
