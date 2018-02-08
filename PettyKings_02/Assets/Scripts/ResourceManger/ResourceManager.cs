using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    // Public static reference to itself
    public static ResourceManager resourceManager;

    // Resource variables
    private int wood_, food_, men_;

    // Default values for resources when resources are reset
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


    // Called when script is destroyed
    void OnDestroy()
    {

        // when destroyed remove static reference to itself
        resourceManager = null;
    }


    // Use this for initialization
    void Start() {

        Reset();
    }

    // Uset to reset resources to default
    void Reset()
    {

        // Set each resource to default
        SetResources(defaultFood, defaultWood, defaultMen);
    }

    void SetResources(int newFood, int newWood, int newMen)
    {
        food_ = newFood;
        wood_ = newWood;
        men_ = newMen;
        ResourceDisplay.resourceDisplay.UpdateDisplay();
    }

    // Getter methods for resources
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

    public int GetResource(Resource resource)
    {
        switch (resource)
        {
            case Resource.FOOD:
                return food_;
            case Resource.WOOD:
                return wood_;
            case Resource.MEN:
                return men_;
            default:
                return 0;
        }
    }

    public int[] GetResources()
    {
        int[] resources = new int[3] { food_, wood_, men_ };
        return resources;
    }

    // Update methods for resources
    public void UpdateFood(int newFood)
    {
        // Update food value and display
        food_ += newFood;
        ResourceDisplay.resourceDisplay.UpdateDisplay();
    }

    public void UpdateWood(int newWood)
    {
        // Update wood value and display
        wood_ += newWood;
        ResourceDisplay.resourceDisplay.UpdateDisplay();
    }

    public void UpdateMen(int newMen)
    {
        // Update men value and display
        men_ += newMen;
        ResourceDisplay.resourceDisplay.UpdateDisplay();
    }

    public void UpdateResources(int newFood = 0, int newWood = 0, int newMen = 0)
    {
        // Update resource values and display
        food_ += newFood;
        wood_ += newWood;
        men_ += newMen;
        ResourceDisplay.resourceDisplay.UpdateDisplay();
    }

    public void UpdateResources(int[] newResources)
    {
        // Update resource values and display
        food_ += newResources[0];
        if (newResources.Length > 1)
        {
            wood_ += newResources[1];
            
            if (newResources.Length > 2)
            {
                men_ += newResources[2];
            }
        }
        ResourceDisplay.resourceDisplay.UpdateDisplay();
    }

    public void UpdateResource(Resource resource, int value)
    {
        // Check which resource is being updated
        // Update that resource and display
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
        ResourceDisplay.resourceDisplay.UpdateDisplay();
    }



    // Check if purchase can be made with cost
    public bool CanPurchase(int[] cost)
    {

        // Check if cost array has all the resources
        if (cost.Length >= 3)
        {

            // Check if there are enough resources for the cost and return the result
            if (cost[0] <= food_ && cost[1] <= wood_ && cost[2] <= men_)
            {
                return true;
            }
        }

        return false;
    }
}
