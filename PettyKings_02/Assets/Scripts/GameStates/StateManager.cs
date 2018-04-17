using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;



// Enums
public enum GAMESTATE
{
    MENU,
    STAGEONE,
    STAGETWO,
    STAGETHREE,
    GAMEOVER,
    IDLE
};

public class StateManager : MonoBehaviour {


    // A static reference to itself so other scripts can access it
    public static StateManager stateManager;

    // Variables
    private GAMESTATE CurrentState_;
    private bool OverlayActive_;

    // Script references
    private HUDToggle GameUI_;
    private CameraController CameraController_;
    private SplineController SplineController_;
    private EventController eventController_;
    private MusicManager musicManager;


    private Camera mainCam;
    private Camera previewCam;

    private Vector3 mainCamMenuPos;
    private Quaternion mainCamMenuRot;

    // CameraMoveEvents
    private Event camMenuToGame_;


    // When object is created
    void Awake()
    {
        // Check if a stateManager already exists
        if (stateManager == null)
        {
            // If not set the static reference to this object
            stateManager = this;
        }
        else if (stateManager != this)
        {
            // Else if a different stateManager already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {
        if (stateManager == this)
        {
            // When destroyed remove static reference to itself
            stateManager = null;
        }
    }


	// Use this for initialization
	void Start () {
        CurrentState_ = GAMESTATE.IDLE;

        // Locate scripts
        CameraController_ = Camera.main.GetComponent<CameraController>();
        SplineController_ = Camera.main.GetComponent<SplineController>();
        eventController_ = EventController.eventController;
        musicManager = MusicManager.musicManager;

        foreach (Camera c in Camera.allCameras)
        {
            if(c.gameObject.name == "Main Camera")
            {
                mainCam = c;
            }
            else if (c.gameObject.name == "SplashScreenCam")
            {
                previewCam = c;
            }
        }

        // Get transform for main camera, used in resetting camera later
        // NOTE: storing the transform itself does not work, as this is a pointer to a variable, rather than storing an instance
        mainCamMenuPos = mainCam.transform.position;
        mainCamMenuRot = mainCam.transform.rotation;
        
        GameUI_ = GetComponent<HUDToggle>();

        // Set UI to be inactive at start
        //OverlayActive_ = false;
        //GameUI_.UIVisible(OverlayActive_);
        GameUI_.UpdateUI();

        // NO LONGER USED FOR CAMERA MOVEMENT
        //camMenuToGame_ = Resources.Load("Events/GameStateController/GotoGameStart") as Event;

        // Set Active cameras
        mainCam.enabled = false;
        previewCam.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        // Allow manual reset from game state to menu state
        if (CurrentState_ == GAMESTATE.STAGEONE)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
	}

    // Get State
    public GAMESTATE CurrentState()
    {
        return CurrentState_;
    }

    // Change the game's state
    public void ChangeState(GAMESTATE newState)
    {
        CurrentState_ = newState;

        // Set UI off if in the menu or end state
        if ((newState == GAMESTATE.MENU) || (newState == GAMESTATE.GAMEOVER))
        {
            OverlayActive_ = false;
        }
        else
        {
            OverlayActive_ = true;
        }
        //GameUI_.UIVisible(OverlayActive_);
        
        if(CurrentState_ == GAMESTATE.IDLE)
        {
            mainCam.enabled = false;
            previewCam.enabled = true;
        }
        else
        {
            previewCam.enabled = false;
            musicManager.StartGame(true);
            mainCam.enabled = true;
        }

        // Update game UI
        GameUI_.UpdateUI();
    }

    public void ReturnToMenu()
    {
        // Reset camera to main menu rotation and position
        //mainCam.transform.position = mainCamMenuPos;
        //mainCam.transform.rotation = mainCamMenuRot;

        // Reset Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Update state machine
        // ChangeState(GAMESTATE.MENU);
    }

    // Set game to be playing
    public void ActivateStageOne()
    {
        ChangeState(GAMESTATE.STAGEONE);
        Debug.Log("active stage one");
        eventController_.GameStart();
    }
    

    // REDUNDANT FUNCTION, Camera movement handled in another location
    // Handled through events
    // CAMERA MOVEMENTS
    /*void CameraStageOne()
    {
        //SplineController_.FollowSpline();


        /*
        eventController_.StartEvent(camMenuToGame_);*/
        /*
        // Position/Rotation for Stage One
        Vector3 pos, rot;

        // Point 1
        pos = new Vector3(-10.36f, 1.13f, -29.13f);
        rot = new Vector3(0.0f, 0.0f, 0.0f);
        CameraController_.AddGotoPosition(pos, rot, true);

        // Point 2
        pos = new Vector3(-9.536f, 1.474f, -25.708f);
        rot = new Vector3(0.0f, 0.0f, 0.0f);
        CameraController_.AddGotoPosition(pos, rot, true);

        // Point 3
        pos = new Vector3(-7.99f, 1.29f, -23.46f);
        rot = new Vector3(0.0f, 0.0f, 0.0f);
        CameraController_.AddGotoPosition(pos, rot, true);

        // Final Point
        pos = new Vector3(-6.36f, 15.1f, -10.0f);
        rot = new Vector3(55.59f, 36.736f, 0.0f);
        CameraController_.AddGotoPosition(pos, rot, true);
        */
    /*}
    */
 }
