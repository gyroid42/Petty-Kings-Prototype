using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    // Queue for list of position camera is to go to
    private Queue<CameraLocation> gotoPositions_;
    private CameraLocation lastLocation_;
    private CameraLocation currentGotoPosition_;


    // Bool to say whether camera is moving or not
    private bool isMoving_;
    private bool finishedMovement_;


    // Speed the camera moves at
    public float speed_;


    // Use this for initialization
    void Start()
    {

        // Set the last location added to the queue to the cameras current position
        isMoving_ = false;
        finishedMovement_ = true;
        lastLocation_ = new CameraLocation(transform.position, transform.rotation.eulerAngles, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0);
        gotoPositions_ = new Queue<CameraLocation>();
    }

    // Update is called once per frame
    void Update()
    {

        // Check if camera is moving
        if (isMoving_)
        {
            if (currentGotoPosition_.position_ == transform.position)
            {
                GotoNextPosition();
            }
            else
            {
                // Calculate the distance the camera moves this frame and apply it to the position
                Vector3 moveDistance = currentGotoPosition_.moveDirection_ * speed_ * Time.deltaTime;
                transform.position += moveDistance;

                // Calculate the distance the camera rotates this frame and apply it to the rotation
                transform.eulerAngles += currentGotoPosition_.rotationDirection_ * currentGotoPosition_.rotationSpeed_ * Time.deltaTime;

                // Check if the camera is at the end of the movement
                if ((currentGotoPosition_.position_ - transform.position).sqrMagnitude <= moveDistance.sqrMagnitude)
                {

                    // Goto the next position in the move queue
                    GotoNextPosition();
                }
            }
        }
    }


    // Method for going to next movement in the move queue
    private void GotoNextPosition()
    {

        // Set the camera position and rotation to the end of the last movement
        transform.position = currentGotoPosition_.position_;
        transform.eulerAngles = currentGotoPosition_.rotation_;

        // If there's no positions left to goto
        if (gotoPositions_.Count <= 0)
        {

            // Stop moving camera
            isMoving_ = false;
            finishedMovement_ = true;
        }
        else
        {

            // Set next goto position
            currentGotoPosition_ = gotoPositions_.Dequeue();
        }


    }

    // Skips to the current goto position and starts the next one
    public void SkipToNextPosition()
    {

        // If there's no position left to goto
        if (gotoPositions_.Count <= 0)
        {

            // Stop moving camera
            isMoving_ = false;
            finishedMovement_ = true;
        }
        else
        {

            // Calculate the new move direction, rotation direction and rotation speed

            // Calculate direction from current position and next goto position
            Vector3 moveDirection = gotoPositions_.Peek().position_ - transform.position;
            moveDirection.Normalize();

            // Calculate rotation direction from current rotation and new goto rotation
            Vector3 rotationDirection = gotoPositions_.Peek().rotation_ - transform.rotation.eulerAngles;
            rotationDirection.Normalize();

            // Calculate the speed of the rotation based on the amount of time taken to move to the next location
            float time = ((gotoPositions_.Peek().position_ - transform.position) / speed_).magnitude;
            float rotationSpeed = (gotoPositions_.Peek().rotation_ - transform.rotation.eulerAngles).magnitude / time;

            // Set the current goto position to the next one in the queue
            currentGotoPosition_ = gotoPositions_.Dequeue();

            // Set the new directions and rotation speed to the new current goto position
            currentGotoPosition_.moveDirection_ = moveDirection;
            currentGotoPosition_.rotationDirection_ = rotationDirection;
            currentGotoPosition_.rotationSpeed_ = rotationSpeed;
        }

    }


    // Adds a new Goto position to the goto position queue
    public void AddGotoPosition(Vector3 newPos, Vector3 newRot, bool startMoving = false)
    {

        // Calculate the new move direction, rotation direction and rotation speed from the new position/rotation
        // And the last location added to the queue

        // Calculate move direction
        Vector3 moveDirection = newPos - lastLocation_.position_;
        moveDirection.Normalize();

        // Calculate rotation direction
        Vector3 rotationDirection = newRot - lastLocation_.rotation_;
        rotationDirection.Normalize();

        // Calculate the speed of the rotation based on the amount of time taken to move to the next location
        float time = ((newPos - lastLocation_.position_) / speed_).magnitude;
        float rotationSpeed = (newRot - lastLocation_.rotation_).magnitude / time;

        // Create the struct for the new location
        CameraLocation newLocation = new CameraLocation(newPos, newRot, moveDirection, rotationDirection, rotationSpeed);

        
        // If the camera has no movements to do
        if (finishedMovement_)
        {

            // Set current goto to the new location
            currentGotoPosition_ = newLocation;
        }
        else
        {

            // Add new location to goto queue
            gotoPositions_.Enqueue(newLocation);
        }

        // Set isMoving bool
        isMoving_ = startMoving;

        // New movement added so camera cannot have finished movement
        finishedMovement_ = false;

        lastLocation_ = newLocation;
    }

    // Clears all movement from the camera
    public void ClearMovement()
    {

        // Clear the goto list and skip the current Goto
        gotoPositions_.Clear();
        SkipToNextPosition();
    }

    // Camera Controls
    public void StartMoving()
    {
        isMoving_ = true;
    }

    public void StopMoving()
    {
        isMoving_ = false;
    }


    // Getters
    public bool FinishedMove()
    {
        return finishedMovement_;
    }

    public bool IsMoving()
    {
        return isMoving_;
    }

}
