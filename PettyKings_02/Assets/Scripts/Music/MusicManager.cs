using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager musicManager;

    public bool isWar_;

    [FMODUnity.EventRef]
    public string music_;

    [FMODUnity.EventRef]
    public string splashMusic_;

    public float warFadeTime_;
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

    public void SetWar(bool newState)
    {

        StopAllCoroutines();
        StartCoroutine(WarTransition(newState));
    }

    private IEnumerator WarTransition(bool newState)
    {
        FMOD.Studio.ParameterInstance warParameter;

        musicPlayer_.getParameter("war", out warParameter);

        float warValue;

        warParameter.getValue(out warValue);

        while ((newState && warValue < 1) || (!newState && warValue > 0))
        {

            if (newState)
            {
                warValue += Time.deltaTime / warFadeTime_;
            }
            else
            {
                warValue -= Time.deltaTime / warFadeTime_;
            }

            warParameter.setValue(warValue);

            yield return null;
        }

    }


    public void StartGame(bool newState)
    {

        StopAllCoroutines();
        StartCoroutine(FadeMeh(newState));
    }


    private IEnumerator FadeMeh(bool newState)
    {

        FMOD.Studio.ParameterInstance BackParameter;

        musicPlayer_.getParameter("game start", out BackParameter);

        FMOD.Studio.ParameterInstance splashParameter;

        splashMusicPlayer_.getParameter("game start", out splashParameter);


        float gamestart;

        splashParameter.getValue(out gamestart);

        while ((newState && gamestart < 1) || (!newState && gamestart > 0))
        {

            if (newState)
            {
                gamestart += Time.deltaTime/splashFadeTime_;
            }
            else
            {
                gamestart -= Time.deltaTime / splashFadeTime_;
            }

            BackParameter.setValue(gamestart);
            splashParameter.setValue(gamestart);
            yield return null;
        }

    }


    public FMOD.Studio.ParameterInstance GetStarRatingParameter()
    {

        FMOD.Studio.ParameterInstance starPar;
        musicPlayer_.getParameter("star rating", out starPar);

        return starPar;
    }

}
