using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DecisionEffect {

    public string name_;
    public int starChange_;

    public bool addEvent_;
    public Event newEvent_;

    public bool playSound_;
    [FMODUnity.EventRef]
    public string sound_;




    public List<BaseAction> effects_;

}
