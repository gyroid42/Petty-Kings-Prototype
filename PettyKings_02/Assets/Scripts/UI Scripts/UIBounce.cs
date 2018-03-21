using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBounce : MonoBehaviour {

    public Vector2 velocity_;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(velocity_.x, velocity_.y);
        transform.eulerAngles += new Vector3(0, 0, velocity_.magnitude);
	}
}
