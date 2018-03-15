using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {

    // public variables
    public float hiddenFor = 0.5f;
    public float visibleFor = 0.5f;
    public string displayText;

    // private variables
    private bool hidden = false;
    private float elapsed = 0.0f;
    private Text thisText;

	// Use this for initialization
	void Start ()
    {
        // Find text object in object
        thisText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // add elapsed time from delta time
        elapsed += Time.deltaTime;

        if (hidden)
        {
            // Display text after ammount of time
            if(elapsed > hiddenFor)
            {
                ResetElapsed();
                thisText.text = displayText;
            }
        }
        else
        {
            // Hide text after ammount of time
            if(elapsed > visibleFor)
            {
                ResetElapsed();
                thisText.text = "";
            }
        }
	}

    private void ResetElapsed()
    {
        hidden = !hidden;
        elapsed = 0.0f;
    }
}
