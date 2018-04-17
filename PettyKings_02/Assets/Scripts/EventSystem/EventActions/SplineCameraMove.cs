using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spline Camera Move", menuName = "EventActions/SplineCamMove")]
public class SplineCameraMove : BaseAction {

    // Properties

    // Spline root the camera will move through
    public GameObject splineRoot_;
    private GameObject instantiatedRoot_;

    // Camera might require a specific point to look at, if true set variables below
    public bool lookAtActive_;
    public Vector3 forcedLookAtPosition_;

    // reference to spline controller
    private SplineController splineController;

    // reference to force camera look at script
    private ForceLookAt forceLookAt;


    // Begin called at start of action
    public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);

        // Setup reference to spline controller
        splineController = Camera.main.GetComponent<SplineController>();

        // Setup reference to force look at
        forceLookAt = Camera.main.GetComponent<ForceLookAt>();

        //instantiatedRoot_ = Instantiate(splineRoot_);

        // set spline root and start movement
        splineController.FollowSpline(splineRoot_);

        // Set look at variables if required
        if (forceLookAt != null)
        {
            forceLookAt.active = lookAtActive_;
            forceLookAt.target = forcedLookAtPosition_;
        }
    }


    // End called at end of action
    public override void End()
    {

        //Destroy(instantiatedRoot_);

        Debug.Log("spline move ended");
    }


    // Update called every frame action is active
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
