using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour {

    public Text headTextBox;
    public Text bodyTextBox;
    public Text acceptTextBox;
    public Text declineTextBox;

    public int woodDecline, menDecline, foodDecline;
    public int woodAccept, menAccept, foodAccept;
    public int poopsicles = 7;

    public string headText;
    public string bodyText;
    public string acceptText;
    public string declineText;

    private GameObject controller;
    private ResourceManager rmScript;

    // Use this for initialization
    void Start () {
        headTextBox.text = headText;
        bodyTextBox.text = bodyText;
        if (acceptText != "")
            acceptTextBox.text = acceptText;
        if (declineText != "")
            declineTextBox.text = declineText;

        // locate resource manager to update values later
        FindResourceManager();


        // Testing Merging
        Debug.Log("It works");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // finds script for updating resources
    void FindResourceManager()
    {
        controller = GameObject.Find("GameController");

        rmScript = controller.GetComponent<ResourceManager>();
    }

    public void DeclineEvent()
    {
        // do code for accepting game event
        rmScript.changeResources(foodDecline, woodDecline, menDecline);
        // close message box
        CloseEvent();
    }

    public void AcceptEvent()
    {
        // do code for accepting game event
        rmScript.changeResources(foodAccept, woodAccept, menAccept);
        // close message box
        CloseEvent();
    }
    
    void CloseEvent()
    {
        DestroyObject(gameObject);
    }
}
