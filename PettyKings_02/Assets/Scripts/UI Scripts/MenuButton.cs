using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    private Button btn_;
    public ConstructionDisplayManager displayManager_;
    public DisplayPanel panel_;

	// Use this for initialization
	void Start () {
        btn_ = GetComponent<Button>();
        btn_.onClick.AddListener(() => displayManager_.ChangePanel(panel_));
	}
	
	
}
