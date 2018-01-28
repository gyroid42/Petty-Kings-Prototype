using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //make the object visible in edit mode

public class GroundTileMesh : MonoBehaviour {

    public float tileWidth = 1; //width of ground tile  
    public float tileLength = 1; //length on ground tile
    public bool isWalkable; //can the player walk on this ground tile 
    public Material materialGreen;
    public Material materialRed;
	// Use this for initialization
	void Start () {
        Mesh mesh = new Mesh();
       
        Vector3[] vertices = new Vector3[4];

        //define vertices
        vertices[0] = new Vector3(-tileWidth, 0, -tileLength);
        vertices[1] = new Vector3(-tileWidth, 0, tileLength);
        vertices[2] = new Vector3(tileWidth, 0, tileLength);
        vertices[3] = new Vector3(tileWidth, 0, -tileLength);

        //assign the vertices to the mesh
        mesh.vertices = vertices;
        //define traingle order
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        //assign coded mesh to game object
        GetComponent<MeshFilter>().mesh = mesh;

        //assign a colour to tiles
        GetComponent<Renderer>().material = materialRed;
        
        //change colour of walkable tiles 
        if (isWalkable)
        {
            GetComponent<Renderer>().material = materialGreen;
        }
	}

}
