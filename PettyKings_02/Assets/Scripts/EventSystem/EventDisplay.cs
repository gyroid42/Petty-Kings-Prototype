using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDisplay : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static EventDisplay eventDisplay;


    private EventController eventController;

    // List of buttons of choices
    private List<Button> buttons_ = new List<Button>();


    public GameObject prefabButton;


    // Event currently being displayed
    public Event event_;


    // References to parts of event screen for displaying data
    public Text nameText_;

    public Text descriptionText_;
    public RawImage artworkImage_;


    // Button variable
    public float btnSizeX = 500;
    public float btnSizeY = 50;
    public float btnPosY  = -300;
    public float btnPosX  = 0;


    // When object is created
    void Awake()
    {

        // Check if an eventDisplay already exists
        if (eventDisplay == null)
        {

            // If not set the static reference to this object
            eventDisplay = this;
        }
        else if (eventDisplay != this)
        {

            // Else a different eventDisplay already exists destroy this object
            Destroy(gameObject);
        }
    }



    // Called when script is destroyed
    void OnDestroy()
    {

        // when destroyed remove static reference to itself
        eventDisplay = null;
    }


    // Method called when object created
    void Start ()
    {

        // Create reference to eventController
        eventController = EventController.eventController;

        // Event display is not active when created
        gameObject.SetActive(false);
	}


    public void DisplayDecision(int choice)
    {

        // Debug display the choice the player made
        Debug.Log(choice);

        // Update display to show choice made by player
        descriptionText_.text = event_.decisionDesc_[choice];
        artworkImage_.texture = event_.decisionArt_[choice];

        // Create button for leaving event
        CreateContinueButton();
    }


    // Set current event happening
    public void SetEvent(Event newEvent)
    {
        event_ = newEvent;
    }

    public void Display()
    {

        // Set all display elements with data from event
        nameText_.text = event_.name_;
        descriptionText_.text = event_.description_;
        artworkImage_.texture = event_.artwork_;

        // Create the buttons for the Decisions
        CreateDecisionButtons(event_.decisionText_);
    }

    public void Display(Event newEvent)
    {

        // Set all display elements with data from event
        nameText_.text = newEvent.name_;
        descriptionText_.text = newEvent.description_;
        artworkImage_.texture = newEvent.artwork_;


        // Create teh buttons for the Decisions
        CreateDecisionButtons(newEvent.decisionText_);
    }


    // Set function for each button
    void CreateDecisionButtons(string[] decisionText)
    {

        // Remove all the buttons from the previous screen
        DestroyButtons();

        // Loop for each decision option
        for (int i = 0; i < decisionText.Length; i++)
        {

            // Create a button for each option and set the buttons position from the number of buttons and size of screen
            GameObject newButton = (GameObject)Instantiate(prefabButton);
            newButton.transform.SetParent(GetComponent<RectTransform>(), false);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.GetComponent<RectTransform>().sizeDelta = new Vector2((btnSizeX / decisionText.Length), btnSizeY);
            newButton.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            newButton.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            newButton.GetComponent<RectTransform>().pivot = new Vector2(0, 1);

            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * newButton.GetComponent<RectTransform>().sizeDelta.x + btnPosX, btnPosY, 0);
            
            // Add method that button calls when pressed
            int tempInt = i;
            newButton.GetComponent<Button>().onClick.AddListener(() => eventController.DecisionSelected(tempInt));
            newButton.GetComponentInChildren<Text>().text = decisionText[i];

            // Add the new button to the list of buttons
            buttons_.Add(newButton.GetComponent<Button>());
        }
    }

    void CreateContinueButton()
    {

        // Remove all the buttons from the previous screen
        DestroyButtons();

        // Create a continue button
        GameObject newButton = (GameObject)Instantiate(prefabButton);
        newButton.transform.SetParent(GetComponent<RectTransform>(), false);
        newButton.transform.localScale = new Vector3(1, 1, 1);
        newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(btnSizeX, btnSizeY);
        newButton.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        newButton.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        newButton.GetComponent<RectTransform>().pivot = new Vector2(0, 1);


        newButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(btnPosX, btnPosY, 0);

        newButton.GetComponentInChildren<Text>().text = "continue!";


        // Add method that button calls when pressed
        newButton.GetComponent<Button>().onClick.AddListener(() => eventController.ContinueButtonClicked());

        // Add the button to the list of buttons
        buttons_.Add(newButton.GetComponent<Button>());
    }

    GameObject CreateButton(Vector3 buttonPosition, Vector2 buttonSize, Vector3 buttonScale)
    {
        GameObject newButton = (GameObject)Instantiate(prefabButton);
        newButton.transform.SetParent(GetComponent<RectTransform>(), false);
        newButton.transform.localScale = buttonScale;
        newButton.GetComponent<RectTransform>().sizeDelta = buttonSize;
        newButton.GetComponent<RectTransform>().position = buttonPosition;

        return newButton;
    }

    void DestroyButtons()
    {

        // Loop for each button in the list of buttons and destroy the button
        for (int i = 0; i < buttons_.Count; i++)
        {
            if (buttons_[i].gameObject)
            {
                Destroy(buttons_[i].gameObject);
            }
        }

        // After all the buttons are destroyed clear the list of buttons
        buttons_.Clear();
    }

}
