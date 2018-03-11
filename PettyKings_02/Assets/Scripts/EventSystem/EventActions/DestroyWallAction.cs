using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Destroy Wall Action", menuName = "EventActions/DestroyWall")]
public class DestroyWallAction : BaseAction
{
    //variables for use by designers, note - if startpos is required to be random, enter -1, if number to remove is to be random enter 0
    public bool leftWall;
    public int startPos;
    public int numberToRemove;

    public int starRating;

    private WorldManager worldController;

    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.DESTROYWALL;

        worldController = Terrain.activeTerrain.GetComponent<WorldManager>(); //get script

        worldController.DestroyWall(leftWall, startPos, numberToRemove);

        actionRunning_ = false;
    }

    public override void End()
    {
        //Modify Star rating value
        worldController.starRating += starRating;
        Debug.Log("STAR RATING = " + worldController.starRating);
    }
}