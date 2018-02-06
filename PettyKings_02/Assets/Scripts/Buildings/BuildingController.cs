using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour {


    // Scriptable Object containing building info
    public Building building_;

    // UI elements
    public Text nameText_, foodText_, woodText_, menText_;
    public Image artwork_;

    // Use this for initialization
    void Start () {

        // Set the UI elements to the info in the building scriptable object
        artwork_.sprite = building_.artwork_;

        nameText_.text = building_.name_;
        foodText_.text = building_.foodCost_.ToString();
        woodText_.text = building_.woodCost_.ToString();
        menText_.text = building_.menCost_.ToString();
        
    }


    // Returns the cost of the building
    public int[] GetCost()
    {
        return building_.GetCost();
    }

    // Purchases building. Returns false if unable to buy
    public bool Purchase()
    {

        // Check if building can be purchased
        if (ResourceManager.resourceManager.CanPurchase(building_.GetCost()))
        {

            // Update resources in manager by the buildings cost
            ResourceManager.resourceManager.UpdateResources(building_.GetCostNegative());

            return true;
        }
        return false;
    }
}
