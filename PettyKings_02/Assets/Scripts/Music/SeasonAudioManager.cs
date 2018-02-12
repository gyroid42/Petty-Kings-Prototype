using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonAudioManager : MonoBehaviour {

    //audio sources for each season
    public AudioClip spring;
    public AudioClip summer;
    public AudioClip autumn;
    public AudioClip winter;
    public AudioClip harvest;
    public AudioSource audioSource;
    public EventController season;
    
    private int currentSeason_;
    private Season[] seasonList_ = new Season[7] { Season.INTRO, Season.SPRING, Season.SUMMER, Season.AUTUMN, Season.HARVEST, Season.WINTER, Season.SPRING2 };

    void Start () {
        audioSource = GetComponent<AudioSource>();//reference to audio source
        UpdateAudio();
    }
	
	// Update is called once per frame

	public void UpdateAudio () {

        currentSeason_ = season.CurrentSeason(); //get the current season
        switch (seasonList_[currentSeason_]) //decide which music to play
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
