using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerMovement : MonoBehaviour {

    private CameraController cameraController_;

    public float scrollSpeed_;
    public float scrollEdgeWidth_;
    public float zoomStepDistance_;
    public float maxZoomDistanceY_;

    private Vector3 mouseScrollPoint_;
    private bool isMouseScrolling_;
    private RaycastHit scrollHitPoint_;
    private Plane scrollPlane_;
    private Vector3 scrollPlanePoint_;

	// Use this for initialization
	void Start () {


        cameraController_ = GetComponent<CameraController>();
        isMouseScrolling_ = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!cameraController_.IsMoving())
        {

            HandleZoomInput();
            HandlePositionScrolling();

            
        }
    }


    void HandleZoomInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            
            if (Input.mouseScrollDelta.y > 0 && transform.position.y == maxZoomDistanceY_)
            {
                return;
            }

            transform.position += Input.mouseScrollDelta.y * zoomStepDistance_ * transform.forward;

            if (transform.position.y < maxZoomDistanceY_)
            {
                transform.position = new Vector3(transform.position.x, maxZoomDistanceY_, transform.position.z);
            }



        }
    }

    void HandlePositionScrolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out scrollHitPoint_, 100, LayerMask.GetMask("Terrain")))
            {
                scrollPlanePoint_ = scrollHitPoint_.point;
                isMouseScrolling_ = true;

                scrollPlane_ = new Plane(Vector3.up, scrollHitPoint_.point);
            }

        }

        if (isMouseScrolling_)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDistance;

            if (scrollPlane_.Raycast(ray, out hitDistance))
            {

                Vector3 moveDistance = scrollPlanePoint_ - ray.GetPoint(hitDistance);
                transform.position += moveDistance;
            }


            if (Input.GetMouseButtonUp(1))
            {
                isMouseScrolling_ = false;
            }
        }
        else
        {
            if (Input.mousePosition.x <= scrollEdgeWidth_)
            {
                transform.position -= transform.right * scrollSpeed_ * Time.deltaTime;

            }
            else if (Input.mousePosition.x >= Screen.width - scrollEdgeWidth_)
            {
                transform.position += transform.right * scrollSpeed_ * Time.deltaTime;
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
    }
}
