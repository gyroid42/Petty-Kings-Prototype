using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour {

    // A public static reference to itself for other scripts to access it
    public static ResourceDisplay resourceDisplay;

    // Text elements for displaying resources
    public Text foodText, woodText, menText;


    // When object is created
    void Awake()
    {

        // Check if an resourceDisplay already exists
        if (resourceDisplay == null)
        {

            // If not set the static reference to this object
            resourceDisplay = this;
        }
        else if (resourceDisplay != this)
        {

            // Else a different resourceDisplay already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {
        if (resourceDisplay == this)
        {
            // when destroyed remove static reference to itself
            resourceDisplay = null;
        }
    }

    // Use this for initialization
    void Start () {

        // When resource display is created update the display
        UpdateDisplay();
    }


    // Mehod to update display with new information
    public void UpdateDisplay()
    {

        // Update each resource value
        foodText.text = "Food: " + ResourceManager.resourceManager.GetFood().ToString();
        woodText.text = "Wood: " + ResourceManager.resourceManager.GetWood().ToString();
        menText.text = "Men: " + ResourceManager.resourceManager.GetMen().ToString();
    }
}
