using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //make the object visible in edit mode

public class GroundTileMesh : MonoBehaviour {

 
    public bool isWalkable; //can the player walk on this ground tile 
    public Material materialGreen;
    public Material materialRed;
	// Use this for initialization
	void Start () {
        Mesh mesh = new Mesh();
       
        Vector3[] vertices = new Vector3[4];

        //define vertices
        vertices[0] = new Vector3(-1, 0, -1);
        vertices[1] = new Vector3(-1, 0, 1);
        vertices[2] = new Vector3(1, 0, 1);
        vertices[3] = new Vector3(1, 0, -1);

        //assign the vertices to the mesh
        mesh.vertices = vertices;
        
        //define UV's
        Vector2[] uv = new Vector2[4];

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        mesh.uv = uv;

        //define normals
        Vector3[] normals = new Vector3[4];

        normals[0] = Vector3.up;
        normals[1] = Vector3.up;
        normals[2] = Vector3.up;
        normals[3] = Vector3.up;

        mesh.normals = normals;

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

        //set thickness of highlight to 0 at run time, this allows for easier editing
        //GetComponent<Renderer>().material.SetFloat("_Thickness", 0.0f);
    }

    //**COMMENTED OUT FOR FURTHER DESIGN DISCUSSIONS**//

    /*private void OnMouseOver() //highlight the tile when mouse is over the tile
    {
        
        GetComponent<Renderer>().material.SetFloat("_Thickness", 4.0f);
        
    }

    private void OnMouseExit() //un-highlight tile when mouse leaves the tile
    {
        GetComponent<Renderer>().material.SetFloat("_Thickness", 0.0f);
    }*/

}
