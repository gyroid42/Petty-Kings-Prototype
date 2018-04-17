using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    // Use this for initialization
    GameObject[] resources; //To hold the loaded prefabs
    GameObject destroyParticles;
    List<GameObject> positions = new List<GameObject>(); //to hold data on the tiles that define building positions
    List<GameObject> buildings = new List<GameObject>();//To hold data for the spawned building
    List<GameObject> activeParticles = new List<GameObject>();
    public Transform lookAt; //make building face center
    List<int> lastNum = new List<int>(); //used to ensure objects dont spawn ontop of eachother
    List<GameObject> wallsLeft = new List<GameObject>();
    List<GameObject> wallsRight = new List<GameObject>();

    public GameObject farmSpawn;

    [FMODUnity.EventRef]
    public string soundUp_;

    [FMODUnity.EventRef]
    public string soundDown_;

    FMOD.Studio.ParameterInstance starRatingAudioPar_;

    //Variables for wall creation
    public Transform startPos;
    public Transform endPos;
    bool rightCompleted;
    bool leftCompleted;
    Vector3 centerPoint;
    Vector3 startCenter;
    Vector3 endCenter;
    float startTime;

    //Star rating variables
    public int starRating;
    private StarRating starManager; //used to update image of stars on screen
    //public GameObject starImageObject;

    public float buildingFallSpeed_;



    void Awake() {

       
        resources = new GameObject[7];
        
        //load buildings that can be spawned
        resources[0] = Resources.Load("Model Prefabs/Palisade") as GameObject; //0
        resources[1] = Resources.Load("Model Prefabs/GateHouse") as GameObject; //1
        resources[2] = Resources.Load("Model Prefabs/ChiefHall") as GameObject; //2
        resources[3] = Resources.Load("Model Prefabs/HuntersHut") as GameObject; //3
        resources[4] = Resources.Load("Model Prefabs/WoodcuttersHut") as GameObject; //4
        resources[5] = Resources.Load("Farm/Cow") as GameObject; //5
        resources[6] = Resources.Load("Farm/Sheep") as GameObject; //6


        //load particle effects
        destroyParticles = Resources.Load("Particle Effects/DestroyParticles") as GameObject;

        if (wallsLeft == null)
        {
            Debug.Log("NULL LEFT");
        }

        if(wallsRight == null)
        {
            Debug.Log("NULL RIGHT");
        }

        //get star rating script
        starManager = GameObject.Find("StarRating").GetComponent<StarRating>(); //GameObject.FindGameObjectWithTag("StarRatingUI").GetComponent<StarRating>();

        //load transforms of build tiles
        positions.AddRange(GameObject.FindGameObjectsWithTag("BuildLocation"));
        
        //spawn Gate house
        SpawnGateHouse();

        //spawn Chiefs Hut
        SpawnChiefHut();

        //spawn buildings
        WorldSpawn();

        //lookAt.position = new Vector3(lookAt.position.x, Terrain.activeTerrain.SampleHeight(lookAt.position), lookAt.position.z);

        foreach(GameObject i in positions)
        {
            i.transform.position = new Vector3(i.transform.position.x, Terrain.activeTerrain.SampleHeight(i.transform.position), i.transform.position.z);
        }


        starRatingAudioPar_ = MusicManager.musicManager.GetStarRatingParameter();

        starRatingAudioPar_.setValue(starRating);
        
    }

    public void ReGenerateWorld()
    {
        //spawn Gate house
        SpawnGateHouse();

        //spawn Chiefs Hut
        SpawnChiefHut();

        //spawn buildings
        WorldSpawn();
    }


    private int GenerateNum() //generate a random number
    {
        int num_; //initialise to number that cant be in list
        bool completed = false;
        
        while(!completed) //check whether num is in list (always false on 1st run)
        {
            num_ = Random.Range(0, positions.Count - 1); //generate number
            //Debug.Log("generated num : " + num_);
            if (!lastNum.Contains(num_)) //if its not in the list, add it
            {
                lastNum.Add(num_);
                //Debug.Log(num_);
                completed = true;
            }
            //Debug.Log(num_);
        }
       return lastNum[lastNum.Count - 1]; //return the last number put in the list
    }
        

    private void WorldSpawn()
    {
            for (int x = 0; x <= Random.Range(1,3); x++) //spawn between 1 and 3 huts
            {
                SpawnHuntersHut();
            }

            for (int x = 0; x <= Random.Range(1, 2); x++) //spawn between 1 and 2 wood huts
            {
                SpawnWoodHut(); 
            }

    }

    //SPAWN ALL BUILDINGS TAKING TERRAIN HEIGHT INTO ACCOUNT:

    public void SpawnHuntersHut()
    {
        if ((buildings.Count) < (positions.Count))
        {
            buildings.Add(Instantiate(resources[3], positions[GenerateNum()].transform));
            buildings[buildings.Count - 1].transform.position = new Vector3(buildings[buildings.Count - 1].transform.position.x, Terrain.activeTerrain.SampleHeight(buildings[buildings.Count - 1].transform.position) + 20, buildings[buildings.Count - 1].transform.position.z);
            buildings[buildings.Count - 1].transform.LookAt(lookAt);
            buildings[buildings.Count - 1].GetComponent<Rigidbody>().velocity = new Vector3(0, -buildingFallSpeed_, 0);
        }
        
    }

    public void SpawnWoodHut()
    {
        if ((buildings.Count) < (positions.Count))
        {
            buildings.Add(Instantiate(resources[4], positions[GenerateNum()].transform));
            buildings[buildings.Count - 1].transform.position = new Vector3(buildings[buildings.Count - 1].transform.position.x, Terrain.activeTerrain.SampleHeight(buildings[buildings.Count - 1].transform.position) + 20, buildings[buildings.Count - 1].transform.position.z);
            buildings[buildings.Count - 1].transform.LookAt(lookAt);
            buildings[buildings.Count - 1].GetComponent<Rigidbody>().velocity = new Vector3(0, -buildingFallSpeed_, 0);
        }
    }


    private void SpawnGateHouse()
    {
        if ((buildings.Count) < (positions.Count))
        {
            buildings.Add(Instantiate(resources[1], startPos.transform));
            buildings[buildings.Count - 1].transform.position = new Vector3(startPos.transform.position.x, Terrain.activeTerrain.SampleHeight(buildings[buildings.Count - 1].transform.position), startPos.transform.position.z);
        }
    }

    public void SpawnChiefHut()
    {
        if ((buildings.Count) < (positions.Count))
        {
            buildings.Add(Instantiate(resources[2], positions[GenerateNum()].transform));
            buildings[buildings.Count - 1].transform.position = new Vector3(buildings[buildings.Count - 1].transform.position.x, Terrain.activeTerrain.SampleHeight(buildings[buildings.Count - 1].transform.position) + 20, buildings[buildings.Count - 1].transform.position.z);
            buildings[buildings.Count - 1].transform.LookAt(lookAt);
            buildings[buildings.Count - 1].GetComponent<Rigidbody>().velocity = new Vector3(0, -buildingFallSpeed_, 0);
        }
    }

    public void SpawnSheep()
    {
        Instantiate(resources[6], farmSpawn.transform);
    }

    public void SpawnCow()
    {
        Instantiate(resources[5], farmSpawn.transform);
    }

    private void Update()
    {

            if (!rightCompleted) //check to see whether either side is complete then stop building them
            {
                BuildWallRight();
            }

            if (!leftCompleted)
            {
                BuildWallLeft();
         
            }   

          /*  if(Input.GetKeyDown("e"))
        {
            DestroyBuilding(0);
        }
        */

    }


    void GetCenter(Vector3 direction)
    {
        centerPoint = (startPos.position + endPos.position) * .5f;
        centerPoint -= direction;
        startCenter = startPos.position - centerPoint;
        endCenter = endPos.position - centerPoint;
    }


    void BuildWallRight() //build right side of wall and add game objects to right list
    {
        GetCenter(Vector3.right);
        float fracComplete = (Time.time - startTime);
        wallsRight.Add(Instantiate(resources[0], Vector3.Slerp(startCenter, endCenter, (fracComplete / 4.0f)), Quaternion.Euler(Random.Range(-5.0f, 5.0f), Random.Range(0.0f, 180.0f), Random.Range(-5.0f, 5.0f))));
        wallsRight[wallsRight.Count - 1].transform.position += centerPoint;
        wallsRight[wallsRight.Count - 1].transform.position = new Vector3(wallsRight[wallsRight.Count - 1].transform.position.x, Terrain.activeTerrain.SampleHeight(wallsRight[wallsRight.Count - 1].transform.position), wallsRight[wallsRight.Count - 1].transform.position.z);
        DontDestroyOnLoad(wallsRight[wallsRight.Count - 1]);
        if (wallsRight.Count > 2)
        {

            if (wallsRight[wallsRight.Count - 1].transform.position == wallsRight[wallsRight.Count - 2].transform.position)
            {
                rightCompleted = true;
            }
        }
    }


    void BuildWallLeft() //build left side of wall and add Game objects to left list
    {
        GetCenter(Vector3.left);
        float fracComplete = (Time.time - startTime);
        wallsLeft.Add(Instantiate(resources[0], Vector3.Slerp(startCenter, endCenter, (fracComplete / 4.0f)), Quaternion.Euler(Random.Range(-5.0f, 5.0f), Random.Range(0.0f, 180.0f), Random.Range(-5.0f, 5.0f))));
        wallsLeft[wallsLeft.Count - 1].transform.position += centerPoint;
        wallsLeft[wallsLeft.Count - 1].transform.position = new Vector3(wallsLeft[wallsLeft.Count - 1].transform.position.x, Terrain.activeTerrain.SampleHeight(wallsLeft[wallsLeft.Count - 1].transform.position), wallsLeft[wallsLeft.Count - 1].transform.position.z);
        DontDestroyOnLoad(wallsLeft[wallsLeft.Count - 1]);
        if (wallsLeft.Count > 2)
        {

            if (wallsLeft[wallsLeft.Count - 1].transform.position == wallsLeft[wallsLeft.Count - 2].transform.position)
            {
                leftCompleted = true;
            }
        }
    }


    public void DestroyBuilding(int index_) //destroy a building defined by the index
    {
        activeParticles.Add(Instantiate(destroyParticles, buildings[index_].transform));
        activeParticles[activeParticles.Count - 1].transform.position = buildings[index_].transform.position;
    }


    public void DestroyWall(bool side_, int startPos_, int numberToRemove_) //bool to decide side, startpos of destruction, number of pillars to remove
    {
        //if no info has been passed into function then assign info
        if (startPos_ == -1) //if designer wishes can make destruction point random
        {
            startPos_ = Random.Range(0, wallsLeft.Count - 1);
        }

        if(numberToRemove_ <= 0)//if designer wishes can make destruction amount random
        {
            numberToRemove_ = Random.Range(10, 30); //minimum 10 blocks removed, max 30
        }

        if (side_ == true) //true for left side
        {
            for (int i = startPos_; i < startPos_ + numberToRemove_; i++) //destroy number of pieces of wall requested, maybe make it a random rotation rather than remove?
            {
                Destroy(wallsLeft[i]);
            }
        }
        else if (side_ == false)//false for right side of wall
        {
            for (int i = startPos_; i < startPos_ + numberToRemove_; i++)
            {
                Destroy(wallsRight[i]);
            }
        }
    }

    public void UpdateStars(int starUpdate)
    {

        if (starUpdate > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(soundUp_);
        }
        else if (starUpdate < 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(soundDown_);
        }


        starRating += starUpdate;
        starManager.UpdateStars(starRating);

        starRatingAudioPar_.setValue(starRating);


    }


    private IEnumerator UpdateStarAudioPar(int starUpdate)
    {
        float starTemp = starRating;

        
        while (starTemp <= starRating)
        {

            starTemp += Time.deltaTime;

            starRatingAudioPar_.setValue(starTemp);

            yield return null;
        }

        starRatingAudioPar_.setValue(starRating);

    }
}
