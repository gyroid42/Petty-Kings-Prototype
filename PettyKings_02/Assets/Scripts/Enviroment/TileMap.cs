using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public GameObject tile_;

    public int sizeX, sizeY;
    public float maxBuildHeight_;

    private bool[][] walkAbleMap_;
    public GameObject[,] tileMap_;

    

	// Use this for initialization
	void Start ()
    {
        tileMap_ = new GameObject[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                tileMap_[i, j] = (GameObject)Instantiate(tile_, new Vector3((float)i * 2.05f - 49, 0, (float)j * 2.05f - 49), Quaternion.identity);
                tileMap_[i, j].transform.position = new Vector3((float)i * 2.05f - 50, Terrain.activeTerrain.SampleHeight(tileMap_[i, j].transform.position) + 0.1f, (float)j * 2.05f - 50);
                tileMap_[i, j].transform.SetParent(transform);


                if (tileMap_[i, j].transform.position.y >= maxBuildHeight_)
                {
                    tileMap_[i, j].GetComponent<GroundTileMesh>().isWalkable = false;
                }
                else if (unchecked( tileMap_[i, j].transform.position.y + 0.1f < (int)(tileMap_[i, j].transform.position.y)) || unchecked(tileMap_[i, j].transform.position.y - 0.2f > (int)(tileMap_[i, j].transform.position.y - 0.1f)))
                {
                    tileMap_[i, j].SetActive(false);
                }
                else
                {
                    tileMap_[i, j].GetComponent<GroundTileMesh>().isWalkable = true;
                }
                tileMap_[i, j].GetComponent<GroundTileMesh>().UpdateMat();
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
