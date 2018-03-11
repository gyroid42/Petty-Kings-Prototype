using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buildingchoice
{
    HuntersHut,
    WoodHut,
    ChiefsHall
    
}

[CreateAssetMenu(fileName = "Create Building Action", menuName = "EventActions/CreateBuilding")]
public class BuildBuildingAction : BaseAction
{
    public int starRating;
    private WorldManager worldController;

    public Buildingchoice building;

    // Use this for initialization
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);


        type_ = ACTIONTYPE.BUILDBUILDING;

        worldController = Terrain.activeTerrain.GetComponent<WorldManager>(); //get script

        if (building == Buildingchoice.HuntersHut) //destroy hunters hut
        {
            worldController.SpawnHuntersHut();
        }
        else if (building == Buildingchoice.WoodHut) //destroy woodhut
        {
            worldController.SpawnWoodHut();
        }
        else if (building == Buildingchoice.ChiefsHall) //destroy chiefs hall
        {
            worldController.SpawnChiefHut();
        }
        

        actionRunning_ = false;
    }

    // End method called when action finishes
    public override void End()
    {
        //Modify Star rating value
        worldController.UpdateStars(starRating);
        Debug.Log("STAR RATING = " + worldController.starRating);
    }
}


