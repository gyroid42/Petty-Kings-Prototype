using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisplayPanel
{
    NONE,
    BUILDINGS,
    UNITS
}


public class ConstructionDisplayManager : MonoBehaviour {

    

    //private DisplayPanel currentPanel_;


    public GameObject buildingsPanel_;
    public GameObject unitsPanel_;
    public GameObject quitMenuButton_;

	// Use this for initialization
	void Start () {


        //currentPanel_ = DisplayPanel.NONE;
	}
	
	
    public void ChangePanel(DisplayPanel newPanel)
    {

        //currentPanel_ = newPanel;

        switch (newPanel)
        {
            case DisplayPanel.NONE:
                buildingsPanel_.SetActive(false);
                unitsPanel_.SetActive(false);
                quitMenuButton_.SetActive(false);
                break;
            case DisplayPanel.BUILDINGS:
                buildingsPanel_.SetActive(true);
                unitsPanel_.SetActive(false);
                quitMenuButton_.SetActive(true);
                break;
            case DisplayPanel.UNITS:
                buildingsPanel_.SetActive(false);
                unitsPanel_.SetActive(true);
                quitMenuButton_.SetActive(true);
                break;
            default:
                break;
        }
    }
}
