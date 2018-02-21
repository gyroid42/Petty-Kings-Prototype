using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragDropBuilding : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private GameObject modelClone; //variable to hold clone of desired gameobject 
    private ParticleSystem smoke;
    private BuildingController buildingController_;
    private SeasonController seasonController;
    private TileMap tileMapManager;

    //array of gameobjects to enable the shader when dragging
    GameObject[] walkableTiles;
    GameObject[] notwalkableTiles;

    //raycast variables
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        buildingController_ = GetComponent<BuildingController>();
        seasonController = SeasonController.seasonController;
        tileMapManager = TileMap.tileMapManager;
    }

    public void OnBeginDrag(PointerEventData eventData) //called when player begins to drag
    {
        modelClone = Instantiate(buildingController_.building_.buildingModel_); //instantiate a clone of desired gameobject
        modelClone.tag = "Building";

        if (buildingController_.building_.buildParticle_)
        {
            smoke = buildingController_.building_.buildParticle_.GetComponent<ParticleSystem>();
        }
       


        tileMapManager.ShowTileMap();


        seasonController.PauseTimer();

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
            if (hit.collider.gameObject.GetComponent<GroundTileMesh>().gameObject.tag == "Walkable" && buildingController_.Purchase() && tileMapManager.CanPlacebuilding(hit.collider.gameObject.GetComponent<GroundTileMesh>().GetMapPosition(), buildingController_.building_.size))
            {
         
                modelClone.transform.position = new Vector3(hit.collider.transform.position.x, Terrain.activeTerrain.SampleHeight(hit.collider.transform.position) + (modelClone.transform.lossyScale.y / 2), hit.collider.transform.position.z);//terrain height is taken into account allowing for building ontop of mounds

                tileMapManager.SetTilesWalkable(hit.collider.gameObject.GetComponent<GroundTileMesh>().GetMapPosition(), buildingController_.building_.size, false);
                if (smoke)
                {
                    Instantiate(smoke, modelClone.transform);
                }
                Debug.Log("it's been build");

            }
            
            else
            {
                Destroy(modelClone);
                Debug.Log("it's not been build");
            }
        }
        else
        {
            Destroy(modelClone); //destroy the gameobject being dragged
        }


        tileMapManager.HideTileMap();




        seasonController.StartTimer();


    }


}
