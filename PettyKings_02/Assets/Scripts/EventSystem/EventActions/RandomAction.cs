using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RandomAction", menuName = "EventActions/RandomAction")]
public class RandomAction : BaseAction
{

    // properties
    public int numberOfRandomActions_;

    // List of actions to get random action from
    public List<BaseAction> actionList_;
    private List<BaseAction> runTimeList_;


    // Called at start of action
    public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);


        // Copy action list to list to use during run time
        runTimeList_ = new List<BaseAction>(actionList_);


        // For each random action to add
        for (int i = 0; i < numberOfRandomActions_; i++)
        {

            // If there are actions to get an action from
            if (runTimeList_.Count <= 0)
            {
                break;
            }

            // Start random event from list and remove
            int index = Random.Range(0, runTimeList_.Count);
            currentEvent_.StartAction(runTimeList_[index]);
            runTimeList_.RemoveAt(index);
        }

        // Action finished
        actionRunning_ = false;
    }


    // Called when action ends
    public override void End()
    {

        Debug.Log("random is ending");
    }


    // Called every frame action is active
    public override bool Update()
    {


        return actionRunning_;
    }

}
