using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDelete : MonoBehaviour {

    public float time_;

    private Timer timer_;

	// Use this for initialization
	void Start () {

        timer_ = new Timer();

        timer_.SetTimer(time_);
	}
	
	// Update is called once per frame
	void Update () {


        if (timer_.UpdateTimer())
        {
            Destroy(gameObject);
        }
	}
}
