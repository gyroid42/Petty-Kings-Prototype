using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour {

    // Use this for initialization
    Object[] buildings;
    public Transform[] positions;

    GameObject[] test;
    void Start() {

        buildings = new Object[5];
        positions = new Transform[8];
        test = new GameObject[8];
        //load buildings that can be spawned
        buildings[0] = Resources.Load("Model Prefabs/Palisade");
        buildings[1] = Resources.Load("Model Prefabs/GateHouse");
        buildings[2] = Resources.Load("Model Prefabs/ChiefHall");
        buildings[3] = Resources.Load("Model Prefabs/HuntersHut");
        buildings[4] = Resources.Load("Model Prefabs/WoodcuttersHut");

        //load transforms of build tiles
        GetBuildTiles();

        //spawn buildings
        WorldSpawn();

    }

    private void GetBuildTiles()
    {
        test = GameObject.FindGameObjectsWithTag("BuildLocation");

        for (int i = 0; i < 8; i++)
        {
            positions[i] = test[i].transform;
        }
    }

    private int GenerateNum()
    {
        int num = Random.Range(0, 8);

        return num;
    }
        
	
    private void WorldSpawn()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(buildings[i], test[GenerateNum()].transform);
        }
    }
}
