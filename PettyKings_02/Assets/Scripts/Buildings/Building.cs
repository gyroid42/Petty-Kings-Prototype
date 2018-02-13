using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject {

    // All data required for a building

    // Information about building
    public string name_;
    public Texture artwork_;
    public GameObject buildingModel_;

    // Cost of the building
    public int foodCost_;
    public int woodCost_;
    public int menCost_;

    // Building size
    public int[] size = new int[2];


    Building()
    {
      //  if (buildingModel_ != null)
      //  {
            buildingModel_.tag = "Building";
      //  }
    }

    // Print method for debugging
    public void Print()
    {
        Debug.Log(name_);
    }


    // Returns the cost of the object
    public int[] GetCost()
    {
        // Store costs in an array then return it
        int[] resources = new int[3] { foodCost_, woodCost_, menCost_ };
        return resources;
    }

    // Returns the cost of the object with negative values
    public int[] GetCostNegative()
    {
        // Store costs in an array then return it
        int[] resources = new int[3] { -foodCost_, -woodCost_, -menCost_ };
        return resources;
    }
}
