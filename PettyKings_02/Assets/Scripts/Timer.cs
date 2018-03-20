using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    float totalTime_;
    float timer_;
    bool active_;

	// Use this for initialization
	public Timer () {

        // Set timer to 0 and not active as default
        SetTimer(0, false);
	}


    // Used to reset timer to setTimer amount
    public void Reset() 
    {
        timer_ = totalTime_;
    }

    // Set the timer
    public void SetTimer(float time, bool active = true) 
    {

        totalTime_ = time;
        active_ = active;

        // Reset timer to totalTime
        Reset();
    }

    // Setters
    public void SetActive(bool value) 
    {
        active_ = value;
    }


    // Start timer
    public void Start() 
    {
        active_ = true;
    }

    // Pause timer
    public void Pause() 
    {
        active_ = false;
    }


    // To be called every frame
    public bool UpdateTimer() 
    {

        // If timer is currently active
        if (active_) 
        {

            // If timer is greater than 0
            if (timer_ > 0) 
            {

                // Update timer by time from last frame
                timer_ -= Time.deltaTime;
            }

            // Return if timer is finished
            return IsFinished();
        }
        return false;
    }


    // Check if timer is finished
    public bool IsFinished() 
    {

        // If no time left on timer return true
        if (timer_ <= 0) 
        {
            return true;
        }

        return false;
    }

    public float TimerPercentage()
    {
        return timer_ / totalTime_;
    }



    public void displayTimeRemaining()
    {
        Debug.Log("time remainging = " + timer_);
    }
}
