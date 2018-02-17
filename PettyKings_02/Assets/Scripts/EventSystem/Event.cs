using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject {


    public List<EventAction> actionList_;
    EventAction currentAction_;
    bool eventRunning_;

	public void Begin()
    {
        eventRunning_ = true;
        GotoNextAction();
    }

    public bool Update()
    {

        if (!currentAction_.Update())
        {
            GotoNextAction();
        }


        return eventRunning_;
    }

    public void EndEvent()
    {

    }


    void GotoNextAction()
    {

        if (currentAction_ != null)
        {
            currentAction_.End();
        }

        if (actionList_.Count > 0)
        {
            currentAction_ = actionList_[0];
            actionList_.RemoveAt(0);

            currentAction_.Begin(this);
        }
        else
        {
            eventRunning_ = false;
        }

    }

}
