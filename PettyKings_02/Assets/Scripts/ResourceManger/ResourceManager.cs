using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {


    public static ResourceManager resourceManager;

    private int wood_, food_, men_;

    public int defaultWood, defaultFood, defaultMen;


    // When object is created
    void Awake()
    {

        // Check if an resourceManager already exists
        if (resourceManager == null)
        {

            // If not set the static reference to this object
            resourceManager = this;
        }
        else if (resourceManager != this)
        {

            // Else a different resourceManager already exists destroy this object
            Destroy(gameObject);
        }
    }


    // Use this for initialization
    void Start() {


    }

    void Reset()
    {
        wood_ = defaultWood;
        food_ = defaultFood;
        men_ = defaultMen;
    }


    public int GetFood()
    {
        return food_;
    }

    public int GetWood()
    {
        return wood_;
    }

    public int GetMen()
    {
        return men_;
    }

    public void UpdateFood(int newFood)
    {
        food_ += newFood;
    }

    public void UpdateWood(int newWood)
    {
        wood_ += newWood;
    }

    public void UpdateMen(int newMen)
    {
        men_ += newMen;
    }

    public void UpdateResources(int newFood = 0, int newWood = 0, int newMen = 0)
    {
        food_ += newFood;
        wood_ += newWood;
        men_ += newMen;
    }

    public void UpdateResources(int[] newResources)
    {
        food_ += newResources[0];
        if (newResources.Length > 1)
        {
            wood_ += newResources[1];
            
            if (newResources.Length > 2)
            {
                men_ += newResources[2];
            }
        }
    }

    public void UpdateResource(Resource resource, int value)
    {
        switch (resource)
        {
            case Resource.FOOD:
                food_ += value;
                break;
            case Resource.WOOD:
                wood_ += value;
                break;
            case Resource.MEN:
                men_ += value;
                break;
            default:
                break;
        }
    }


}
