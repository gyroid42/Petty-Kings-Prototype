using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is used to force the camera to look at a certain point on update
// It is used to eliminate the horrible rotation applied to the camera in the
// preview camera spline movement as well as in the camera events used later in the events system
public class ForceLookAt : MonoBehaviour {

    // Look at point
    public Vector3 target;
    public bool active = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    // Late update forces this transform after the translation applied by the spline
	void LateUpdate () {
        // Force object to point at target
        // active variable is required as the script may be in use but not required for certain movements
        if (active)
        {
            gameObject.transform.LookAt(target);
        }
	}
}
