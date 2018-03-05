using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spline Camera Move", menuName = "EventActions/SplineCamMove")]
public class SplineCameraMove : BaseAction {

    // Properties

    // Spline root the camera will move through
    public GameObject splineRoot_;

    // reference to spline controller
    private SplineController splineController;

    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        // Setup reference to spline controller
        splineController = Camera.main.GetComponent<SplineController>();

        // set spline root and start movement
        splineController.SplineRoot = splineRoot_;
        splineController.FollowSpline();
    }


    public override void End()
    {

        Debug.Log("spline move ended");
    }


    public override bool Update()
    {

        // If camera isn't moving then end the action
        if (!splineController.isMoving())
        {
            actionRunning_ = false;
        }

        return actionRunning_;
    }
}
