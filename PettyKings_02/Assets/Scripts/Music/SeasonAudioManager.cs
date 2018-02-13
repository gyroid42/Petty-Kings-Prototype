using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonAudioManager : MonoBehaviour {

    public static SeasonAudioManager seasonAudioManager;

    //audio sources for each season
    public AudioClip spring;
    public AudioClip summer;
    public AudioClip autumn;
    public AudioClip winter;
    public AudioClip harvest;
    public AudioSource audioSource;
    private EventController season;
    
    private Season currentSeason_;

    // When object is created
    void Awake()
    {

        // Check if a seasonAudioManager already exists
        if (seasonAudioManager == null)
        {

            // If not set the static reference to this object
            seasonAudioManager = this;
        }
        else if (seasonAudioManager != this)
        {

            // Else if a different seasonAudioManager already exists destroy this object
            Destroy(gameObject);
        }
    }

    // Called when script is destroyed
    void OnDestroy()
    {

        // When destroyed remove static reference to itself
        seasonAudioManager = null;
    }


    void Start () {
        audioSource = GetComponent<AudioSource>();//reference to audio source
        season = EventController.eventController;
        UpdateAudio();
    }
	
	// Update is called once per frame

	public void UpdateAudio () {

        
        currentSeason_ = season.CurrentSeason(); //get the current season

        switch (currentSeason_) //decide which music to play
        {
            case Season.SPRING:
                 audioSource.clip = spring; audioSource.Play();
                break;
            case Season.SUMMER:
                audioSource.clip = summer; audioSource.Play();
                break;
            case Season.AUTUMN:
                audioSource.clip = autumn; audioSource.Play();
                break;
            case Season.HARVEST:
                audioSource.clip = harvest; audioSource.Play();
                break;
            case Season.WINTER:
                audioSource.clip = winter; audioSource.Play();
                break;
            case Season.SPRING2:
                audioSource.clip = spring; audioSource.Play();
                break;
            case Season.INTRO:
                audioSource.clip = spring; audioSource.Play();

                break;

        }

    }
}
