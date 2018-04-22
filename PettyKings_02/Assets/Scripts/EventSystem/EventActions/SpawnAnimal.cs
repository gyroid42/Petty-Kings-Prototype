using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalChoice
{
   Sheep, 
   Cow
}

[CreateAssetMenu(fileName = "Spawn Animal Action", menuName = "EventActions/SpawnAnimal")]
public class SpawnAnimal : BaseAction {

    private WorldManager worldController;

    public AnimalChoice animal;
    public int numberSpawned_;

    // Use this for initialization
    public override void Begin(NarrativeEvent newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.SPAWNANIMAL;

        worldController = Terrain.activeTerrain.GetComponent<WorldManager>(); //get script

        
            if (animal == AnimalChoice.Sheep) //spawn sheep
            {
            worldController.SpawnSheep(numberSpawned_);
            }
            else if (animal == AnimalChoice.Cow) //spawn coo
            {
            worldController.SpawnCow(numberSpawned_);
            }
        
       
        actionRunning_ = false;
    }
}
