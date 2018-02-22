using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RandomAction", menuName = "EventActions/RandomAction")]
public class RandomAction : BaseAction
{

    // properties
    public int numberOfRandomActions_;

    public List<BaseAction> actionList_;

    private List<BaseAction> runTimeList_;
    private EventController eventController;


    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        isBlocking_ = false;


        runTimeList_ = new List<BaseAction>(actionList_);
        eventController = EventController.eventController;
        eventController.StartCoroutine(StartRandomActions());
        
    }

    public override void End()
    {

        Debug.Log("random is ending");
    }


    public override bool Update()
    {


        return actionRunning_;
    }

    IEnumerator StartRandomActions()
    {
        int actionsStarted = 0;
        while (actionsStarted < numberOfRandomActions_)
        {
            for (int i = actionsStarted; i < numberOfRandomActions_; i++)
            {
                
                if (runTimeList_.Count > 0)
                {
                    int index = Random.Range(0, runTimeList_.Count);
                    if (currentEvent_.StartAction(runTimeList_[index]))
                    {

                        runTimeList_.RemoveAt(index);
                        numberOfRandomActions_++;
                    }
                }
                else
                {
                    break;
                }
            }

            yield return null;
        }

        
        actionRunning_ = false;
    }

}
