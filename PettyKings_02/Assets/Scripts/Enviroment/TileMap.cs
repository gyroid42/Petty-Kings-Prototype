using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public static TileMap tileMapManager;

    public GameObject tile_;

    public int sizeX, sizeY;
    public float maxBuildHeight_;

    private bool[][] walkAbleMap_;
    private GroundTileMesh[,] tileMap_;

    // When object is created
    void Awake()
    {

        // Check if a tileMapManager already exists
        if (tileMapManager == null)
        {

            // If not set the static reference to this object
            tileMapManager = this;
        }
        else if (tileMapManager != this)
        {

            // Else if a different tileMapManager already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {

        // When destroyed remove static reference to itself
        tileMapManager = null;
    }

    // Use this for initialization
    void Start ()
    {
        tileMap_ = new GroundTileMesh[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {

                tileMap_[i, j] = ((GameObject)Instantiate(tile_, new Vector3((float)i * 2.05f - 49, 0, (float)j * 2.05f - 49), Quaternion.identity)).GetComponent<GroundTileMesh>();
                tileMap_[i, j].transform.position = new Vector3((float)i * 2.05f - 50, Terrain.activeTerrain.SampleHeight(tileMap_[i, j].transform.position) + 0.1f, (float)j * 2.05f - 50);
                tileMap_[i, j].transform.SetParent(transform);

                tileMap_[i, j].SetMapPosition(i, j);


                if (tileMap_[i, j].transform.position.y >= maxBuildHeight_)
                {
                    tileMap_[i, j].isWalkable = false;
                }
                else if (unchecked( tileMap_[i, j].transform.position.y + 0.1f < (int)(tileMap_[i, j].transform.position.y)) || unchecked(tileMap_[i, j].transform.position.y - 0.2f > (int)(tileMap_[i, j].transform.position.y - 0.1f)))
                {
                    tileMap_[i, j].gameObject.SetActive(false);
                }
                else
                {
                    tileMap_[i, j].isWalkable = true;
                }
                tileMap_[i, j].UpdateMat();
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideTileMap()
    {

        for (int i = 0; i < tileMap_.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap_.GetLength(1); j++)
            {
                tileMap_[i, j].Hide();
            }
        }
    }

    public void ShowTileMap()
    {
        for (int i = 0; i < tileMap_.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap_.GetLength(1); j++)
            {
                tileMap_[i, j].Show();
            }
        }
    }

    public bool CanPlacebuilding(int[] position, int[] size)
    {

        if (position[0] + size[0] - 1 > tileMap_.GetLength(0) || position[1] + size[1] - 1 > tileMap_.GetLength(1))
        {
            Debug.Log("cannot place number 1");
            return false;
        }

        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                if (!tileMap_[position[0] + i, position[1] + j].isWalkable)
                {
                    Debug.Log("cannot place number 2 position = " + position[0] + position[1] + " | i = " + i + " | j = " + j);
                    return false;
                }
            }
        }

        Debug.Log("can place building");

        return true;
    }

    public bool SetTilesWalkable(int[] position, int[] size, bool walkable)
    {
        if (position[0] + size[0] - 1 > tileMap_.GetLength(0) || position[1] + size[1] - 1 > tileMap_.GetLength(1))
        {
            return false;
        }

        string newTag = "";

        if (walkable)
        {
            newTag = "Walkable";
        }
        else
        {
            newTag = "NotWalkable";
        }

        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                GroundTileMesh tile = tileMap_[position[0] + i, position[1] + j];
                tile.isWalkable = walkable;
                tile.gameObject.tag = newTag;
                tile.UpdateMat();
            }
        }

        return true;
    }
}
