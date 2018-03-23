using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager musicManager;


    [FMODUnity.EventRef]
    public string music_;

    private FMOD.Studio.ParameterInstance transition_;
    private FMOD.Studio.EventInstance musicPlayer_;


    // When object is created
    void Awake()
    {
        // Check if a musicManager already exists
        if (musicManager == null)
        {
            // If not set the static reference to this object
            musicManager = this;
        }
        else if (musicManager != this)
        {
            // Else if a different musicManager already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {
        // When destroyed remove static reference to itself
        musicManager = null;
    }


    // Use this for initialization
    void Start () {


        musicPlayer_ = FMODUnity.RuntimeManager.CreateInstance(music_);

        musicPlayer_.start();
        
	}
	
	public void ChangeMusic(string newMusic)
    {
        musicPlayer_.release();
        musicPlayer_ = FMODUnity.RuntimeManager.CreateInstance(newMusic);

        musicPlayer_.start();
    }
}
