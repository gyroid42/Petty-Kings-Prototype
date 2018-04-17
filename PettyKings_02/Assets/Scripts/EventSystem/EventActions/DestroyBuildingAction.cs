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
    private WorldManager worldController;

   public BuildingChoice building;
    
    // Use this for initialization
    public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);
        

        type_ = ACTIONTYPE.DESTROYBUILDING;

        worldController = Terrain.activeTerrain.GetComponent<WorldManager>(); //get script


        if (building == BuildingChoice.HuntersHut) //destroy hunters hut
        {
            worldController.DestroyBuilding(2);
        }
        else if (building == BuildingChoice.WoodHut) //destroy woodhut
        {
            worldController.DestroyBuilding(3);
        }
        else if (building == BuildingChoice.ChiefsHall) //destroy chiefs hall
        {
            worldController.DestroyBuilding(1);
        }
        else if(building == BuildingChoice.GateHouse) //destroy gatehouse
        {
            worldController.DestroyBuilding(0);
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
