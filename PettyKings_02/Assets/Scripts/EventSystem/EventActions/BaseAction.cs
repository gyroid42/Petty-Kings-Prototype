using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Enum for for describing type of action
public enum ACTIONTYPE
{
    BASE,
    DECISION,
    CAMERAMOVE,
    CAMERARESET,
    PAUSE,
    AUDIO,
    MUSIC
}

// Base Action
//[CreateAssetMenu(fileName = "New Action", menuName = "EventActions/BaseAction"), System.Serializable]
public class BaseAction : ScriptableObject
{

    // properties
    //
    // Type of action
    protected ACTIONTYPE type_;

    // Reference to event this action is in
    protected Event currentEvent_;

    // Bool for ending action
    protected bool actionRunning_ = true;

    // IsBlocking flag
    public bool isBlocking_ = true;

    // Begin method called when action starts
    public virtual void Begin(Event newEvent)
    {
        
        // Set Default values
        type_ = ACTIONTYPE.BASE;
        currentEvent_ = newEvent;
        actionRunning_ = true;
    }


    // End method called when action finishes
    public virtual void End()
    {

    }


    // Called every frame the action is happening
    public virtual bool Update()
    {


        return actionRunning_;
    }

    // Getter for type of event action
    public virtual ACTIONTYPE Type()
    {

        return type_;
    }



    // Print method
    public virtual void Print()
    {

        // Displays information about event to debug log
        Debug.Log(type_);
    }

}


