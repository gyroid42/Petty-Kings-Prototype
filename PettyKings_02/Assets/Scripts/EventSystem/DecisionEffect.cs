using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DecisionEffect {

    public float starChange_;


    public bool playSound_;
    [FMODUnity.EventRef]
    public string sound_;


    public bool displayScreen_;
    public EventDisplayData decisionDisplayData_;

}
