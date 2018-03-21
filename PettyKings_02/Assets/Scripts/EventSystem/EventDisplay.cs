using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void ButtonDel(int choice);


public class EventDisplay : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static EventDisplay eventDisplay;


    // List of buttons of choices
    private List<Button> buttons_ = new List<Button>();


    public GameObject prefabButton;



    // References to parts of event screen for displaying data
    public Text nameText_;

    public Text descriptionText_;
    public RawImage artworkImage_;
    public RawImage decisionTypeLogo_;
    public Image timerBar_;

    // Button variable
    public float btnSizeX = 500;
    public float btnSizeY = 50;
    public float btnPosY  = -300;
    public float btnPosX  = 0;

    // Default timer setting
    public float defaultTimerLength_;

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

        buttons_.Add(GameObject.Find("DecisionBoxLeft").GetComponent<Button>());
        buttons_.Add(GameObject.Find("DecisionBoxRight").GetComponent<Button>());

        // Event display is not active when created
        gameObject.SetActive(false);
	}


    public void Display(EventDisplayData displayData)
    {
        // Set all display elements with data from event
        nameText_.text = displayData.name_;
        descriptionText_.text = displayData.description_;
        artworkImage_.texture = displayData.artwork_;
        decisionTypeLogo_.texture = displayData.decisionLogo_;

        // Create the buttons
        CreateButtons(displayData);

    }


    // Creates buttons for event display
    private void CreateButtons(EventDisplayData displayData)
    {

        // Destroy previous buttons
        //DestroyButtons();

        // If there are buttons to create
        if (displayData.btnFunctions_.Length > 0)
        {

            // Loop for each button to create
            for (int i = 0; i < displayData.btnFunctions_.Length; i++)
            {
                /*
                // Create new button
                GameObject newButton = (GameObject)Instantiate(prefabButton);

                // Get button's transform componenet
                RectTransform btnTransform = newButton.GetComponent<RectTransform>();

                // Setup button's transform position/size/anchor/etc...
                btnTransform.SetParent(GetComponent<RectTransform>(), false);
                btnTransform.localScale = new Vector3(1, 1, 1);
                btnTransform.sizeDelta = new Vector2(btnSizeX / displayData.btnFunctions_.Length, btnSizeY);
                btnTransform.anchorMax = new Vector2(0, 1);
                btnTransform.anchorMin = new Vector2(0, 1);
                btnTransform.pivot = new Vector2(0, 1);
                btnTransform.anchoredPosition = new Vector3(btnPosX + i * btnTransform.sizeDelta.x, btnPosY, 0);
                */
                // Set button text

                if (i < 2)
                {
                    if (i < displayData.btnText_.Length)
                    {
                        buttons_[i].GetComponentInChildren<Text>().text = displayData.btnText_[i];
                    }
                    else
                    {
                        buttons_[i].GetComponentInChildren<Text>().text = "option " + (i + 1);
                    }
                    // Create method for button OnClick from function pointer in display data
                    buttons_[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    int tempInt = i;
                    ButtonDel tempButtonDel = displayData.btnFunctions_[i];
                    buttons_[i].GetComponent<Button>().onClick.AddListener(() => tempButtonDel(tempInt));

                    // Add new button to button list
                    //buttons_.Add(newButton.GetComponent<Button>());
                }
                
            }
        }
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

    public void UpdateTimerBar(float percentage)
    {
        timerBar_.fillAmount = percentage;
    }

}
