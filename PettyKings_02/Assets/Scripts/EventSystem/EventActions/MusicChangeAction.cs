using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new MusicChange", menuName = "EventActions/MusicChange")]
public class MusicChangeAction : BaseAction {

    // properties
    [FMODUnity.EventRef]
    public string music_;

    // Begin method called when action starts
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.MUSIC;

        // MusicManager.StartMusic(music_)

        actionRunning_ = false;
    }

    // End method called when action finishes
    public override void End()
    {
        
    }

    // Called every frame the action is happening
    public override bool Update()
    {

        return actionRunning_;
    }
}
