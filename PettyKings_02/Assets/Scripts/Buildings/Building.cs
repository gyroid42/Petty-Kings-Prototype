using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject {

    // All data required for a building

    public string name_;
    public Texture artwork_;
    public GameObject buildingModel_;

    public int foodCost_;
    public int woodCost_;
    public int menCost_;



    public void Print()
    {
        Debug.Log(name_);
    }


    public int[] GetCost()
    {
        int[] resources = new int[3] { foodCost_, woodCost_, menCost_ };
        return resources;
    }
}
