using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour {

    // Use this for initialization
    GameObject[] resources; //To hold the loaded prefabs
    GameObject[] positions; //to hold data on the tiles that define building positions
    GameObject[] buildings;//To hold data for the spawned building
    public Transform lookAt; //make building face center
    List<int> lastNum = new List<int>(); //used to ensure objects dont spawn ontop of eachother
    List<GameObject> walls = new List<GameObject>();


    //Variables for wall creation
   public Transform startPos;
    public Transform endPos;
    bool rightCompleted;
    bool leftCompleted;
    Vector3 centerPoint;
    Vector3 startCenter;
    Vector3 endCenter;
    float startTime;
    void Start() {

        resources = new GameObject[5];
        buildings = new GameObject[5];
        positions = new GameObject[8];

       // if (lastNum == null)
       // {
       //     Debug.Log("NUUULLLLL");
       // }
        
        //load buildings that can be spawned
        resources[0] = Resources.Load("Model Prefabs/Palisade") as GameObject;
        resources[1] = Resources.Load("Model Prefabs/GateHouse") as GameObject;
        resources[2] = Resources.Load("Model Prefabs/ChiefHall") as GameObject;
        resources[3] = Resources.Load("Model Prefabs/HuntersHut") as GameObject;
        resources[4] = Resources.Load("Model Prefabs/WoodcuttersHut") as GameObject;

        //load transforms of build tiles
        positions = GameObject.FindGameObjectsWithTag("BuildLocation");

        //spawn buildings
        WorldSpawn();

    }
    // ERROR, CAN RETURN 2 NUMBERS THE SAME, NEEDS FIXED
    private int GenerateNum() //generate a random number
    {
        int num_ = -1;//Random.Range(0, positions.Length);
        
        while(!lastNum.Contains(num_))
        {
            num_ = Random.Range(0, positions.Length);
            if (!lastNum.Contains(num_))
            {
                lastNum.Add(num_);
                Debug.Log(num_);
            }
            //Debug.Log(num);
        }

        //lastNum.Add(num);
        //Debug.Log(lastNum[lastNum.Count -1]);
       return lastNum[lastNum.Count - 1];
    }
        
	
    private void WorldSpawn()
    {
        for (int i = 1; i < buildings.Length; i++)
        {
            buildings[i] = Instantiate(resources[i], positions[GenerateNum()].transform);
            buildings[i].transform.LookAt(lookAt);
        }
    }

    private void WallSpawn()
    {
       
        
    }

    private void Update()
    {
        
        
        if(!rightCompleted || !leftCompleted)
        {
            if (!rightCompleted)
            {
                GetCenter(Vector3.right);
                float fracComplete = (Time.time - startTime);
                walls.Add(Instantiate(resources[0]));
                walls[walls.Count - 1].transform.position = Vector3.Slerp(startCenter, endCenter, (fracComplete / 5.0f));
                // walls[walls.Count - 1].transform.rotation =
                walls[walls.Count - 1].transform.position += centerPoint;
                if (walls.Count > 2)
                {

                     if (walls[walls.Count - 1].transform.position == walls[walls.Count - 2].transform.position)
                    {
                         rightCompleted = true;
                    }
                }
            }

            if (!leftCompleted)
            {
                GetCenter(Vector3.left);
                float fracComplete = (Time.time - startTime);
                walls.Add(Instantiate(resources[0]));
                walls[walls.Count - 1].transform.position = Vector3.Slerp(startCenter, endCenter, (fracComplete / 5.0f));
               // walls[walls.Count - 1].transform.rotation = Random.rotation;
                walls[walls.Count - 1].transform.position += centerPoint;
                if (walls.Count > 600) //FIX THIS CONDITION!!
                {
                    leftCompleted = true;
                    
                }
            }

        }

        
            
    }

    void GetCenter(Vector3 direction)
    {
        centerPoint = (startPos.position + endPos.position) * .5f;
        centerPoint -= direction;
        startCenter = startPos.position - centerPoint;
        endCenter = endPos.position - centerPoint;
    }

}
