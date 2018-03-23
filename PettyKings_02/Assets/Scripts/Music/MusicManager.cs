using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {


    [FMODUnity.EventRef]
    public string music_;

    private FMOD.Studio.ParameterInstance transition_;
    private FMOD.Studio.EventInstance musicPlayer_;

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
