using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {


    private StateManager stateManager_;
    public List<GameObject> MenuStates_;

    // Use this for initialization
    void Start()
    {
        // Locate state manager in scene
        stateManager_ = StateManager.stateManager;

        // Ensure that when the object is created,
        // the visible panel is the menu
        Back();
    }

    // This clears the UI so the new state can be shown
    void Clear()
    {
        // This will loop through all the list in the menu state
        foreach (GameObject panel in MenuStates_)
        {
            // Set the panel to be inactive
            panel.SetActive(false);
        }
    }

    // Sets the game into is playing state
    public void PlayButton()
    {
        // Activate scene stage one
        stateManager_.ActivateStageOne();
    }

    // Calling this function sets the menu back to its main state
    public void Back()
    {
        // Clear current states
        Clear();

        // Set the main panel to be active
        MenuStates_[0].SetActive(true);
    }

    // Head to a specific state in the list
    public void ChangeMenu(int n)
    {
        // Clear current states
        Clear();

        // Make the new UI panel visible
        MenuStates_[n].SetActive(true);
    }
}
