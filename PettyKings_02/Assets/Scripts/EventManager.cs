using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to generate the events that appear in the game
/// The script handles when events appear
/// </summary>

public class EventManager : MonoBehaviour {

    public GameObject Event1;
    public GameObject Event2;
    public GameObject Event3;
    public GameObject Event4;

    private GameObject ParentObject;
    private GameObject ActiveEvent;
	private List<GameObject> EventArray = new List<GameObject>();

    private int eventCounter = 0;

	// Use this for initialization
	void Start () {
        // Set up event array
        initEventArray();
        // get parent
        ParentObject = GameObject.Find("UI Canvas");
        Debug.Log("EventManager script is now running...");

        DisplayEvent(eventCounter);
	}
	
	// Update is called once per frame
	void Update () {
        if (ActiveEvent == null && (eventCounter != EventArray.Count))
        {
            Debug.Log(EventArray.Count);
            DisplayEvent(eventCounter);
        }
	}

	void DisplayEvent(int displayEvent)
    {
		ActiveEvent = Instantiate(EventArray[displayEvent], new Vector3(220.0f, 200.0f, 0.0f), Quaternion.identity);
        ActiveEvent.transform.SetParent(ParentObject.transform);
        eventCounter++;
    }

    void initEventArray()
	{
		EventArray.Add (Event1);
		EventArray.Add (Event2);
		EventArray.Add (Event3);
		EventArray.Add (Event4);
	}
}
