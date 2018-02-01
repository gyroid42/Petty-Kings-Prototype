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
}
