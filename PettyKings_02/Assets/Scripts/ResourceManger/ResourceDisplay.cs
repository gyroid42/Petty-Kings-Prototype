using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour {


    public Text foodText, woodText, menText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        foodText.text = ResourceManager.resourceManager.GetFood().ToString();
        woodText.text = ResourceManager.resourceManager.GetWood().ToString();
        menText.text = ResourceManager.resourceManager.GetMen().ToString();
	}


}
