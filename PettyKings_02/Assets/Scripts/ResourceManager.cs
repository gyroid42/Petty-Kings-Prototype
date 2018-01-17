using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    public int wood, food, men;
    public Text woodText, foodText, menText;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        updateUI();
	}

    // Update the UI to display resources
    private void updateUI()
    {
        string w, f, m;

        w = "Wood: " + wood;
        f = "Food: " + food;
        m = "Men: " + men;

        woodText.text = w;
        foodText.text = f;
        menText.text = m;
    }

    // Getters
    public int getFood()
    {
        return food;
    }
    public int getWood()
    {
        return wood;
    }
    public int getIron()
    {
        return men;
    }

    // Setters
    public void setFood(int uFood)
    {
        food = uFood;
    }
    public void setWood(int uWood)
    {
        wood = uWood;
    }
    public void setIron(int uMen)
    {
        men = uMen;
    }

    // Increment/decrement resources (inputing negative values to reduce)
    public void changeResources(int deltaFood, int deltaWood, int deltaMen)
    {
        food += deltaFood;
        wood += deltaWood;
        men += deltaMen;
        string dbgOut = "Food: " + food + " Wood: " + wood + " Men: " + men;
        Debug.Log(dbgOut);
    }


    // TEST
    public void testFunc()
    {
        changeResources(20, 10, 3);
        string dbgOut = "Food: " + food + " Wood: " + wood + " Men: " + men;
        Debug.Log(dbgOut);
    }
}
