using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Camera Move", menuName = "EventActions/CameraMove")]
public class CameraMoveAction : BaseAction
{

    // properties
    public CameraGoto[] cameraGoto_;

    private CameraController cameraController;

    // Begin method called when action starts
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.CAMERAMOVE;

        cameraController = Camera.main.GetComponent<CameraController>();

        for (int i = 0; i < cameraGoto_.Length; i++)
        {
            cameraController.AddGotoPosition(cameraGoto_[i]);
        }
    }


    // End method called when action finishes
    public override void End()
    {

    }


    // Called every frame the action is happening
    public override bool Update()
    {


        if (cameraController.FinishedMove())
        {
            actionRunning_ = false;
        }

        return actionRunning_;
    }
}
