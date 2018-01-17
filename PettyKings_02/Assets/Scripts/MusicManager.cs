using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    public AudioClip musicVillage;
    public AudioClip musicCombat;
    public AudioSource musicSource;

    private bool muted_;

    // Use this for initialization
    void Start()
    {
        setClip(musicVillage);
        musicSource.loop = true;
        playMusic();
        muted_ = false;
    }

    // set music
    public void setCombat()
    {
        setClip(musicCombat);
    }
    public void setVillage()
    {
        setClip(musicVillage);
    }
    // play music
    public void playMusic()
    {
        musicSource.Play();
    }

    // pause music
    public void pauseMusic()
    {
        musicSource.Stop();
    }

    // mute/unmute
    public void muteUnmuteMusic()
    {
        if (!muted_)
            musicSource.Pause();
        else
            musicSource.UnPause();

        // set muted to the opposite value
        muted_ = !muted_;
    }

    // sets the music clip that is desired
    void setClip(AudioClip musicFile)
    {
        musicSource.clip = musicFile;
    }
}
