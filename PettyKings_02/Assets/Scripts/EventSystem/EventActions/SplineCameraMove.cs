using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spline Camera Move", menuName = "EventActions/SplineCamMove")]
public class SplineCameraMove : BaseAction {

    // Properties

    // Spline root the camera will move through
    public GameObject splineRoot_;
    public bool loopingMovement_ = false;
    public float movementTime_ = 5.0f;
    private GameObject instantiatedRoot_;

    // Camera might require a specific point to look at, if true set variables below
    public bool lookAtActive_ = false;
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

        // set time it will take the spline movement to complete one whole movement
        // Default is 5 seconds
        splineController.Duration = movementTime_;

        // set wrapping of the spline 
        // LOOP repeats, ONCE moves through the spline once
        // Make sure that looping splines autoclose or there will be a noticeable gap
        if (loopingMovement_)
        {
            splineController.WrapMode = eWrapMode.LOOP;
            splineController.AutoClose = true;
        }
        else
        {
            splineController.WrapMode = eWrapMode.ONCE;
            splineController.AutoClose = false;
        }


        // Set look at variables if required
        forceLookAt.active = lookAtActive_;
        forceLookAt.target = forcedLookAtPosition_;

        // set spline root and start movement
        splineController.FollowSpline(splineRoot_);

        // Also end action if the camera movement is set to loop in the background
        if (loopingMovement_)
        {
            actionRunning_ = false;
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
