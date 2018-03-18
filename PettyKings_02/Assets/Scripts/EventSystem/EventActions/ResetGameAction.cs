using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "New Reset Game Action", menuName = "EventActions/ResetGame")]
public class ResetGameAction : BaseAction {

    // Properties


    // Called at start of action
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.RESETGAME;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
