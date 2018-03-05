using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerMovement : MonoBehaviour {

    private CameraController cameraController_;
    private SplineController splineController_;

    public float scrollSpeed_;
    public float scrollEdgeWidth_;
    public float zoomStepDistance_;
    public float maxZoomDistanceY_;
    public float maxScrollDistanceX_;
    public float maxScrollDisanceZ_;
    
    private bool isMouseScrolling_;
    private RaycastHit scrollHitPoint_;
    private Plane scrollPlane_;
    private Vector3 scrollPlanePoint_;

	// Use this for initialization
	void Start () {

        // Get reference to CameraController
        cameraController_ = GetComponent<CameraController>();
        splineController_ = GetComponent<SplineController>();
        isMouseScrolling_ = false;
	}
	
	// Update is called once per frame
	void Update () {

        // Only allow user movement when the camera isn't moving between scenes
        if (StateManager.stateManager.CurrentState() == GAMESTATE.STAGEONE && !cameraController_.IsMoving() && !splineController_.isMoving())
        {

            HandleZoomInput();
            HandlePositionScrolling();
        }
    }


    // Handles user control for zooming the camera
    void HandleZoomInput()
    {
        // If there has been some movement on the scroll wheel
        if (Input.mouseScrollDelta.y != 0)
        {
            // If user is try to zoom in and the camera is already at the max zoom
            if (Input.mouseScrollDelta.y > 0 && transform.position.y == maxZoomDistanceY_)
            {

                // Exit method without doing anything
                return;
            }

            // Apply zoom to camera
            transform.position += Input.mouseScrollDelta.y * zoomStepDistance_ * transform.forward;

            // If camera has zoomed farther than max zoom
            if (transform.position.y < maxZoomDistanceY_)
            {
                // Set Y position to Max Zoom Distance
                transform.position = new Vector3(transform.position.x, maxZoomDistanceY_, transform.position.z);
            }



        }
    }


    // Handles user control for moving the camera
    void HandlePositionScrolling()
    {
        // If user right clicks
        if (Input.GetMouseButtonDown(1))
        {

            // Get position of map user hit
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out scrollHitPoint_, 100, LayerMask.GetMask("Terrain")))
            {
                // Store the point and set is Scrolling to true
                scrollPlanePoint_ = scrollHitPoint_.point;
                isMouseScrolling_ = true;

                // Create plane for user to scroll on
                scrollPlane_ = new Plane(Vector3.up, scrollHitPoint_.point);
            }

        }

        // IF user is scrolling via right clicking
        if (isMouseScrolling_)
        {

            // Calculate position mouse position is hitting the scrolling plane
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDistance;

            if (scrollPlane_.Raycast(ray, out hitDistance))
            {

                // Apply the difference between the point the user clicked on the map and the cursors new position
                // To the camera position
                Vector3 moveDistance = scrollPlanePoint_ - ray.GetPoint(hitDistance);
                transform.position += moveDistance;
            }

            // If user let's go of right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                // Camera is no longer scrolling
                isMouseScrolling_ = false;
            }
        }

        // Else user isn't scrolling using right click
        else
        {

            // If mouse is at edge of screen apply the appropriate movement to camera
            if (Input.mousePosition.x <= scrollEdgeWidth_)
            {
                Vector3 moveDirection = new Vector3(transform.right.x, 0, transform.right.z);
                transform.position -= moveDirection * scrollSpeed_ * Time.deltaTime;

            }
            else if (Input.mousePosition.x >= Screen.width - scrollEdgeWidth_)
            {
                Vector3 moveDirection = new Vector3(transform.right.x, 0, transform.right.z);
                transform.position += moveDirection * scrollSpeed_ * Time.deltaTime;
            }

            if (Input.mousePosition.y <= scrollEdgeWidth_)
            {
                Vector3 moveDirection = new Vector3(transform.forward.x, 0, transform.forward.z);
                transform.position -= moveDirection * scrollSpeed_ * Time.deltaTime;
            }
            else if (Input.mousePosition.y >= Screen.height - scrollEdgeWidth_)
            {
                Vector3 moveDirection = new Vector3(transform.forward.x, 0, transform.forward.z);
                transform.position += moveDirection * scrollSpeed_ * Time.deltaTime;
            }
        }
        //bound camera movement to world
        if (transform.position.x > maxScrollDistanceX_)
        {
            transform.position = new Vector3(maxScrollDistanceX_, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -maxScrollDistanceX_)
        {
            transform.position = new Vector3(-maxScrollDistanceX_, transform.position.y, transform.position.z);
        }

        if(transform.position.z > maxScrollDisanceZ_)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxScrollDisanceZ_);
        }
        else if (transform.position.z < -maxScrollDisanceZ_)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxScrollDisanceZ_);
        }

        
    }

    
}
