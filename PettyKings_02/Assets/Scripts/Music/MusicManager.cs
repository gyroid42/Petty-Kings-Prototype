using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager musicManager;


    [FMODUnity.EventRef]
    public string music_;

    [FMODUnity.EventRef]
    public string splashMusic_;

    public float splashFadeTime_;

    private FMOD.Studio.EventInstance splashMusicPlayer_;

    private FMOD.Studio.ParameterInstance transition_;
    private FMOD.Studio.EventInstance musicPlayer_;
    private bool isPlaying_ = false;

    // When object is created
    void Awake()
    {

        

        // Check if a musicManager already exists
        if (musicManager == null)
        {
            // If not set the static reference to this object
            musicManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (musicManager != this)
        {
            // Else if a different musicManager already exists destroy this object
            Destroy(gameObject);
            return;
        }


        splashMusicPlayer_ = FMODUnity.RuntimeManager.CreateInstance(splashMusic_);

        splashMusicPlayer_.setParameterValue("fade", 1.0f);
        splashMusicPlayer_.start();
        
        musicPlayer_ = FMODUnity.RuntimeManager.CreateInstance(music_);

        musicPlayer_.start();

        isPlaying_ = true;
    }

    // Called when script is destroyed
    void OnDestroy()
    {

        // When destroyed remove static reference to itself
        if (musicManager == this)
        {
            musicManager = null;
        }
    }


    // Use this for initialization
    void Start () {


        if (musicManager != null)
        {

        }
        
	}
	
	public void ChangeMusic(string newMusic)
    {

        musicPlayer_.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicPlayer_.release();
        musicPlayer_ = FMODUnity.RuntimeManager.CreateInstance(newMusic);

        musicPlayer_.start();
    }


    public void FadeSplashScreen(bool newState)
    {

        StartCoroutine(FadeMeh(newState));
    }


    private IEnumerator FadeMeh(bool newState)
    {

        FMOD.Studio.ParameterInstance fadeParameter;

        splashMusicPlayer_.getParameter("fade", out fadeParameter);

        float fade;

        fadeParameter.getValue(out fade);

        while ((newState && fade < 1) || (!newState && fade > 0))
        {

            if (newState)
            {
                fade += Time.deltaTime/splashFadeTime_;
            }
            else
            {
                fade -= Time.deltaTime / splashFadeTime_;
            }

            fadeParameter.setValue(fade);
            yield return null;
        }

    }


    public FMOD.Studio.ParameterInstance GetStarRatingParameter()
    {

        FMOD.Studio.ParameterInstance starPar;
        musicPlayer_.getParameter("starrating", out starPar);

        return starPar;
    }

}
