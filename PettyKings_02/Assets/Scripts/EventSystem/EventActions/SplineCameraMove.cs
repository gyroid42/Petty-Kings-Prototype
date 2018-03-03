using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCameraMove : BaseAction {

    // Properties

    public GameObject splineRoot_;
    private SplineController cameraController;

    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);


        cameraController = Camera.main.GetComponent<SplineController>();

        cameraController.SplineRoot = splineRoot_;
        cameraController.FollowSpline();
    }


    public override void End()
    {
        
    }


    public override bool Update()
    {

        if (!cameraController.isMoving())
        {
            actionRunning_ = false;
        }

        return actionRunning_;
    }
}
