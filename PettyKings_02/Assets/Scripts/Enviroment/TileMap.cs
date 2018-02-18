using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    // static reference to itself
    public static TileMap tileMapManager;

    // reference to tile prefab
    public GameObject tile_;

    // settings for generating the tilemap
    public int sizeX, sizeY;
    public float maxBuildHeight_;
    public float tileUpperHeightTolerance_;
    public float tileLowerHeightTolerance_;
    public float cornerUpperHeightTolerance_;
    public float cornerLowerHeightTolerance_;
    public int heightLevelSpacing_;
    public int numberOfRequiredNeighbours_;
    public int numberOfNeighbourChecks_;
    public bool setTilesToTileHeight_;

    // 2D array of tileMap
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

        // initialise tileMap array
        tileMap_ = new GroundTileMesh[sizeX, sizeY];

        // Loop for each tile
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {

                // Create a tile and set it's position ontop of the terrain
                tileMap_[i, j] = ((GameObject)Instantiate(tile_, new Vector3((float)i * GroundTileMesh.size_[0] - (sizeX + 1), 0, (float)j * GroundTileMesh.size_[1] - (sizeY + 1)), Quaternion.identity)).GetComponent<GroundTileMesh>();
                tileMap_[i, j].transform.position = new Vector3((float)i * GroundTileMesh.size_[0] - (sizeX + 1), Terrain.activeTerrain.SampleHeight(tileMap_[i, j].transform.position), (float)j * GroundTileMesh.size_[1] - (sizeY + 1));
                tileMap_[i, j].transform.SetParent(transform);

                tileMap_[i, j].SetMapPosition(i, j);

                // determine tiles integer height
                int height = Mathf.RoundToInt(tileMap_[i, j].transform.position.y);
                
                tileMap_[i, j].SetHeight(height);

                // Check if tile is valid, if not disable it
                if (!CheckTileValid(i, j))
                {
                    DisableTile(i, j);
                }
                else
                {
                    // If tile is value make it walkable
                    tileMap_[i, j].isWalkable = true;
                }

                // Update tiles material
                tileMap_[i, j].UpdateMat();
            }
        }


        // Loop for number of neighbour checks
        for (int checkNum = 0; checkNum < numberOfNeighbourChecks_; checkNum++)
        {

            // Loop for each tile
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    // Check for minimum number of valid neighbours
                    if (!CheckTileValidWithNeighbours(i, j))
                    {
                        // If there's not enough neighbours disable the tile
                        DisableTile(i, j);
                    }

                }
            }
        }

        // If setting to set tiles to their integer height is true
        if (setTilesToTileHeight_)
        {

            // Loop for each tile and set their height to their integer height
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    tileMap_[i, j].UpdateHeightTransform();

                }
            }
        }

    }


    // Method for checking if tile is valid
    private bool CheckTileValid(int x, int y)
    {
        // Get tiles' height/position and size
        int height = tileMap_[x, y].GetHeight();
        Vector3 position = tileMap_[x, y].transform.position;
        Vector2 size = GroundTileMesh.size_;

        // Get corner heights by sampling terrain
        float[] cornerHeights = new float[4]
        {
            Terrain.activeTerrain.SampleHeight(new Vector3(position.x - size.x/2, position.y, position.z - size.y/2)),
            Terrain.activeTerrain.SampleHeight(new Vector3(position.x - size.x/2, position.y, position.z + size.y/2)),
            Terrain.activeTerrain.SampleHeight(new Vector3(position.x + size.x/2, position.y, position.z - size.y/2)),
            Terrain.activeTerrain.SampleHeight(new Vector3(position.x + size.x/2, position.y, position.z + size.y/2))
        };


        // If tile's position isn't at the correct height within the height tolerance then tile isn't valid
        if (unchecked(position.y < height - tileLowerHeightTolerance_) || unchecked(position.y > height + tileUpperHeightTolerance_))
        {
            return false;
        }

        // If tile's height isn't within the correct height spacing then tile isn't valid
        else if (height % heightLevelSpacing_ != 0)
        {
            return false;
        }

        // If tile is too high then tile is invalid
        else if (position.y >= maxBuildHeight_)
        {
            return false;
        }


        // Check each corner of the tile
        foreach ( float corner in cornerHeights)
        {

            // If tile corner isn't within corner height tolerance then tile is invalid
            if (unchecked(corner < height - cornerLowerHeightTolerance_) || unchecked(corner > height + cornerUpperHeightTolerance_))
            {
                //Debug.Log("corner too high corner height = " + corner);
                //Debug.Log("tile height = " + height);
                return false;
            }
        }
        
        // Tile passed every check therefore is valid
        return true;
    }


    // Check for neighbours
    private bool CheckTileValidWithNeighbours(int x, int y)
    {
        // If tile's neighbours aren't off edge of tilemap
        if (x - 1 >= 0 && x + 1 < tileMap_.GetLength(0) && y - 1 >= 0 && y + 1 < tileMap_.GetLength(1))
        {

            // Get tile's height
            int height = tileMap_[x, y].GetHeight();

            // Create list of tile's neighbours
            List<GroundTileMesh> neighbourTiles = new List<GroundTileMesh>
            {
                tileMap_[x - 1, y + 1],
                tileMap_[x, y + 1],
                tileMap_[x + 1, y + 1],
                tileMap_[x - 1, y],
                tileMap_[x + 1, y],
                tileMap_[x - 1, y - 1],
                tileMap_[x, y - 1],
                tileMap_[x + 1, y - 1]
            };

            // counter for number of valid neighbours
            int validNeighbourCount = 0;

            // Loop for each neighbour
            for (int i = 0; i < neighbourTiles.Count; i++)
            {

                // If neighbour is active and of the same tile height
                if (neighbourTiles[i].gameObject.activeSelf && neighbourTiles[i].GetHeight() == height)
                {

                    // increment valid neighbour counter
                    validNeighbourCount++;

                    // If counter reached minimum required neighbours then tile is valid
                    if (validNeighbourCount >= numberOfRequiredNeighbours_)
                    {
                        return true;
                    }
                }
            }
        }

        // Tile didn't pass test therefore is invalid
        return false;
    }


    // Method to disable a tile
    public void DisableTile(int x, int y)
    {

        // Set walkable to false, update the material and make it's gameobject not active
        tileMap_[x, y].isWalkable = false;
        tileMap_[x, y].UpdateMat();
        tileMap_[x, y].gameObject.SetActive(false);
    }


    // Method to hide tile map
    public void HideTileMap()
    {

        // Loop for each tile and hide it
        for (int i = 0; i < tileMap_.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap_.GetLength(1); j++)
            {
                tileMap_[i, j].Hide();
            }
        }
    }


    // Method to show tile map
    public void ShowTileMap()
    {

        // Loop for each tile and show it
        for (int i = 0; i < tileMap_.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap_.GetLength(1); j++)
            {
                tileMap_[i, j].Show();
            }
        }
    }


    // Method to check for space for creating building
    public bool CanPlacebuilding(int[] position, int[] size)
    {

        // Set top left corner based on building size for each dimension (different whether size is even or odd)
        if (size[0] % 2 != 0)
        {
            position[0] -= (size[0] - 1) / 2;
        }
        else
        {
            position[0] -= size[0] / 2;
        }

        if (size[1] % 2 != 0)
        {
            position[1] -= (size[1] - 1) / 2;
        }
        else
        {
            position[1] -= size[1] / 2;
        }


        // If building is off boundary of tileMap then building can't be placed
        if (position[0] + size[0] >= tileMap_.GetLength(0) || position[1] + size[1] >= tileMap_.GetLength(1))
        {
            Debug.Log("cannot place number 1");
            return false;
        }

        // Get height of build tile
        int height = tileMap_[position[0], position[1]].GetHeight();

        // Loop for each each tile building is over
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {

                // Check if position isn't walkable
                if (!tileMap_[position[0] + i, position[1] + j].isWalkable)
                {
                    // Position isn't walkable therefore invalid build position
                    Debug.Log("cannot place number 2 position = " + position[0] + position[1] + " | i = " + i + " | j = " + j);
                    return false;
                }

                // Check if all positions are of the same height
                if (tileMap_[position[0] + i, position[1] + j].GetHeight() != height)
                {
                    // All positions not of same height therefore invalid build position
                    return false;
                }
            }
        }

        Debug.Log("can place building");

        // All tests passed building can be placed
        return true;
    }


    // Method to set walkable state for a group of tiles
    public bool SetTilesWalkable(int[] position, int[] size, bool walkable)
    {

        // Check if tiles are on map
        if (position[0] + size[0] >= tileMap_.GetLength(0) || position[1] + size[1] >= tileMap_.GetLength(1))
        {
            // Tiles not on map, couldn't set them to new walkable state
            return false;
        }

        // Create new tag for tiles
        string newTag = "";

        if (walkable)
        {
            newTag = "Walkable";
        }
        else
        {
            newTag = "NotWalkable";
        }

        // Loop for each tile in group
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {

                // Update walkable state of tile and it's material
                GroundTileMesh tile = tileMap_[position[0] + i, position[1] + j];
                tile.isWalkable = walkable;
                tile.gameObject.tag = newTag;
                tile.UpdateMat();
            }
        }

        return true;
    }
}
