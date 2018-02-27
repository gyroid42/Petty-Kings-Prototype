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
    List<GameObject> wallsLeft = new List<GameObject>();
    List<GameObject> wallsRight = new List<GameObject>();

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
        
        //load buildings that can be spawned
        resources[0] = Resources.Load("Model Prefabs/Palisade") as GameObject;
        resources[1] = Resources.Load("Model Prefabs/GateHouse") as GameObject;
        resources[2] = Resources.Load("Model Prefabs/ChiefHall") as GameObject;
        resources[3] = Resources.Load("Model Prefabs/HuntersHut") as GameObject;
        resources[4] = Resources.Load("Model Prefabs/WoodcuttersHut") as GameObject;

        if(wallsLeft == null)
        {
            Debug.Log("NULL LEFT");
        }

        if(wallsRight == null)
        {
            Debug.Log("NULL RIGHT");
        }

        //load transforms of build tiles
        positions = GameObject.FindGameObjectsWithTag("BuildLocation");

        //spawn buildings
        WorldSpawn();

        //spawn Gate house
        SpawnGateHouse();

    }

    // ERROR, CAN RETURN 2 NUMBERS THE SAME, NEEDS FIXED
    private int GenerateNum() //generate a random number
    {
        int num_; //initialise to number that cant be in list
        bool completed = false;
        
        while(!completed) //check whether num is in list (always false on 1st run)
        {
            num_ = Random.Range(0, positions.Length); //generate number
            Debug.Log("generated num : " + num_);
            if (!lastNum.Contains(num_)) //if its not in the list, add it
            {
                lastNum.Add(num_);
                Debug.Log(num_);
                completed = true;
            }
            //Debug.Log(num_);
        }
       return lastNum[lastNum.Count - 1]; //return the last number put in the list
    }
        

    private void WorldSpawn()
    {
        for (int i = 2; i < buildings.Length; i++)
        {
            buildings[i] = Instantiate(resources[i], positions[GenerateNum()].transform);
            buildings[i].transform.LookAt(lookAt);
        }
    }


    private void SpawnGateHouse()
    {
        buildings[1] = Instantiate(resources[1], startPos);
    }


    private void Update()
    {
            if (!rightCompleted)
            {
                BuildWallRight();
            }

            if (!leftCompleted)
            {
                BuildWallLeft();
         
            }
            
    }


    void GetCenter(Vector3 direction)
    {
        centerPoint = (startPos.position + endPos.position) * .5f;
        centerPoint -= direction;
        startCenter = startPos.position - centerPoint;
        endCenter = endPos.position - centerPoint;
    }


    void BuildWallRight()
    {
        GetCenter(Vector3.right);
        float fracComplete = (Time.time - startTime);
        wallsRight.Add(Instantiate(resources[0], Vector3.Slerp(startCenter, endCenter, (fracComplete / 4.0f)), Quaternion.Euler(Random.Range(-5.0f, 5.0f), Random.Range(0.0f, 180.0f), Random.Range(-5.0f, 5.0f))));
        wallsRight[wallsRight.Count - 1].transform.position += centerPoint;
        if (wallsRight.Count > 2)
        {

            if (wallsRight[wallsRight.Count - 1].transform.position == wallsRight[wallsRight.Count - 2].transform.position)
            {
                rightCompleted = true;
            }
        }
    }


    void BuildWallLeft()
    {
        GetCenter(Vector3.left);
        float fracComplete = (Time.time - startTime);
        wallsLeft.Add(Instantiate(resources[0], Vector3.Slerp(startCenter, endCenter, (fracComplete / 4.0f)), Quaternion.Euler(Random.Range(-5.0f, 5.0f), Random.Range(0.0f, 180.0f), Random.Range(-5.0f, 5.0f))));
        wallsLeft[wallsLeft.Count - 1].transform.position += centerPoint;
        if (wallsLeft.Count > 2)
        {

            if (wallsLeft[wallsLeft.Count - 1].transform.position == wallsLeft[wallsLeft.Count - 2].transform.position)
            {
                leftCompleted = true;
            }
        }
    }
}
