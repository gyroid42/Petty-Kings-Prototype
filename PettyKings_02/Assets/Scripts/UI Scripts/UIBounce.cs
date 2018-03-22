using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBounce : MonoBehaviour {

    private Vector2 velocity_;
    private Vector2 gravity_;

	// Use this for initialization
	void Start () {
        velocity_ = new Vector2(Random.Range(-10f, 10f), 30);
        gravity_ = new Vector2(0, -5f);
	}
	
	// Update is called once per frame
	void Update () {

        velocity_ += gravity_;

        transform.position += new Vector3(velocity_.x, velocity_.y);
        //transform.eulerAngles += new Vector3(0, 0, velocity_.magnitude);
	}
}
