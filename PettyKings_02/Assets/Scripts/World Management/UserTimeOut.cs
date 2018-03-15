using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserTimeOut : MonoBehaviour {

    // Use this for initialization

    public float timeOutTime;
    float timer;
    private WorldManager worldController;
    private bool canReload; //so the scene cannot reload while on splash screen
	void Awake () {
        timer = timeOutTime;
        canReload = false;
        worldController = Terrain.activeTerrain.GetComponent<WorldManager>();

	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;

        if(Input.anyKey)
        {
            timer = timeOutTime;
            canReload = true;
        }

        if(timer <= 0 && canReload)
        {
            worldController.ResetWalls();
            canReload = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }

        Debug.Log(timer);
	}
}
