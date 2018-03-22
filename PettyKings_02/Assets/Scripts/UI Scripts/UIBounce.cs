using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBounce : MonoBehaviour {

    private Vector2 velocity_;
    public Vector2 gravity_;
    public Vector2 gravity2_;
    private float disappearSpeed_;

	// Use this for initialization
	void Start () {
        velocity_ = new Vector2(Random.Range(-100f, 100f) * 5, 30);


        //gravity_ = new Vector2(0, -1000f);
        //gravity2_ = new Vector2(0, -100f);
        disappearSpeed_ = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        gravity_ += gravity2_ * Time.deltaTime;
        velocity_ += gravity_ * Time.deltaTime;

        transform.position += (new Vector3(velocity_.x, velocity_.y)) * Time.deltaTime;
        transform.localScale += (new Vector3(disappearSpeed_, disappearSpeed_, 0)) * Time.deltaTime;
        Debug.Log("gravity = " + gravity_);
        Debug.Log("velocity = " + velocity_);
        //transform.eulerAngles += new Vector3(0, 0, velocity_.magnitude);
	}
}
