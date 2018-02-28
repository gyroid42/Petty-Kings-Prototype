using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum BuildingChoice
    {
        HuntersHut,
        WoodHut,
        ChiefsHall,
        GateHouse
    }

[CreateAssetMenu(fileName = "Destroy Building Action", menuName = "EventActions/DestroyBuilding")]
public class DestroyBuildingAction : BaseAction
{
       public int starRating;
    private GenerateWorld buildingDestroy;

   public BuildingChoice building;
    
    // Use this for initialization
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);
        

        type_ = ACTIONTYPE.DESTROYBUILDING;

        buildingDestroy = Terrain.activeTerrain.GetComponent<GenerateWorld>(); //get script


        if (building == BuildingChoice.HuntersHut) //destroy hunters hut
        {
            buildingDestroy.DestroyBuilding(2);
        }
        else if (building == BuildingChoice.WoodHut) //destroy woodhut
        {
            buildingDestroy.DestroyBuilding(3);
        }
        else if (building == BuildingChoice.ChiefsHall) //destroy chiefs hall
        {
            buildingDestroy.DestroyBuilding(1);
        }
        else if(building == BuildingChoice.GateHouse) //destroy gatehouse
        {
            buildingDestroy.DestroyBuilding(0);
        }

        actionRunning_ = false;
    }
    // End method called when action finishes
    public override void End()
    {
        //Modify Star rating value
        buildingDestroy.starRating += starRating;
        Debug.Log(buildingDestroy.starRating);
    }
}
