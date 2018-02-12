using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropBuilding : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private GameObject modelClone; //variable to hold clone of desired gameobject 
    public ParticleSystem smoke;
    private BuildingController buildingController_;

    //array of gameobjects to enable the shader when dragging
    GameObject[] walkableTiles;
    GameObject[] notwalkableTiles;

    //raycast variables
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        buildingController_ = GetComponent<BuildingController>();

    }

    public void OnBeginDrag(PointerEventData eventData) //called when player begins to drag
    {
       modelClone = Instantiate(buildingController_.building_.buildingModel_); //instantiate a clone of desired gameobject

       //add all the tiles to the gameobject arrays
        walkableTiles = GameObject.FindGameObjectsWithTag("Walkable");
        notwalkableTiles = GameObject.FindGameObjectsWithTag("NotWalkable");

        //set thickness of shading line to 4, showing the grid to player
        foreach (GameObject i in walkableTiles)
        {
            i.gameObject.GetComponent<Renderer>().material.SetFloat("_Thickness", 3.0f);
        }

        foreach (GameObject i in notwalkableTiles)
        {
            i.gameObject.GetComponent<Renderer>().material.SetFloat("_Thickness", 6.0f);
        }


    }

    public void OnDrag(PointerEventData eventData) //called while player is dragging
    {
        //cast ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("BuildTiles")) && hit.collider.tag == "Walkable")
        {

            modelClone.transform.position = new Vector3(hit.collider.transform.position.x, Terrain.activeTerrain.SampleHeight(hit.collider.transform.position) + (modelClone.transform.lossyScale.y / 2), hit.collider.transform.position.z);
        }
        else
        {
            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Terrain")))
            {
                modelClone.transform.position = new Vector3(hit.point.x, Terrain.activeTerrain.SampleHeight(hit.point) + (modelClone.transform.lossyScale.y / 2), hit.point.z);
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData) //called when player stops dragging
    {
        //cast ray from mouse position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("BuildTiles")) )
        {
            if (hit.collider.tag == "Walkable" && buildingController_.Purchase())
            {
         
                modelClone.transform.position = new Vector3(hit.collider.transform.position.x, Terrain.activeTerrain.SampleHeight(hit.collider.transform.position) + (modelClone.transform.lossyScale.y / 2), hit.collider.transform.position.z);//terrain height is taken into account allowing for building ontop of mounds
                         
                hit.collider.gameObject.GetComponent<GroundTileMesh>().isWalkable = false;
                hit.collider.gameObject.GetComponent<GroundTileMesh>().UpdateMat(); //change colour of tile after building is placed
                hit.collider.gameObject.tag = "NotWalkable"; //change tag of tile
                Instantiate(smoke, modelClone.transform);   
               
            }
            
            else
            {
                Destroy(modelClone);
            }
        }
        else
        {
            Destroy(modelClone); //destroy the gameobject being dragged
        }


        //reset the thickness of shading line therefor hiding the grid 
        foreach (GameObject i in walkableTiles)
        {
            Debug.Log(i.name);
            i.gameObject.GetComponent<Renderer>().material.SetFloat("_Thickness", 0.0f);
        }

        foreach (GameObject i in notwalkableTiles)
        {
            i.gameObject.GetComponent<Renderer>().material.SetFloat("_Thickness", 0.0f);
        }
       
    }


}
