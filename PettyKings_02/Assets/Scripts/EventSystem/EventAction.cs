using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIONTYPE
{
    BASE,
    DECISION
}

[CreateAssetMenu(fileName = "New Action", menuName = "EventActions/BaseAction")]
public class EventAction : ScriptableObject
{

    private ACTIONTYPE type_;
    Event currentEvent_;
    bool actionRunning_;

    public virtual void Begin(Event newEvent)
    {
        type_ = ACTIONTYPE.BASE;
        currentEvent_ = newEvent;
        actionRunning_ = true;
    }

    public virtual void End()
    {

    }

    public virtual bool Update()
    {

        return actionRunning_;
    }

    public ACTIONTYPE Type()
    {
        return type_;
    }


    // Print method
    public void Print()
    {

        // Displays information about event to debug log
        Debug.Log(type_);
    }

}

[CreateAssetMenu(fileName = "New Decision Action", menuName = "EventActions/DecisionAction")]
public class DecisionAction : EventAction {

    // All data required for an event

    // Data for displaying initial event i.e. name, description...
    public string name_;
    public Texture artwork_;
    [TextArea(3, 5)]
    public string description_;

    // Data for each decision for each event
    public string[] decisionText_ = new string[2];
    public Texture[] decisionArt_ = new Texture[2];
    [TextArea(3, 5)]
    public string[] decisionDesc_ = new string[2];
    public int[] decisionFood_ = new int[2];
    public int[] decisionWood_ = new int[2];
    public int[] decisionMen_ = new int[2];




    

    // Returns Resources from a choice
    public int[] GetDecisionResources(int choice)
    {
        int[] resources = new int[3];
        resources[0] = decisionFood_[choice];
        resources[1] = decisionWood_[choice];
        resources[2] = decisionMen_[choice];
        return resources;
    }

}
