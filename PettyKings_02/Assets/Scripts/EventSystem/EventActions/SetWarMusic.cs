using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SetWarMusic", menuName = "EventActions/SetWarMusic")]
public class SetWarMusic : BaseAction {

    public bool war_;

	MusicManager musicManager;

        public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);


        musicManager = MusicManager.musicManager;
        musicManager.SetWar(war_);


        actionRunning_ = false;
    }

}
