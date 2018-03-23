using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Action", menuName = "EventActions/Audio")]
public class AudioAction : BaseAction
{
   


    // properties
    [FMODUnity.EventRef]
    public string audioClip_;

    private FMOD.Studio.EventInstance audioEv_;

    // Begin method called when action starts
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.AUDIO;

        audioEv_ = FMODUnity.RuntimeManager.CreateInstance(audioClip_);
        

        
        audioEv_.start();


        //FMODUnity.RuntimeManager.PlayOneShot(audioClip_, Camera.main.transform.position);
        /*
        audioSource_ = EventController.eventController.gameObject.GetComponent<AudioSource>();

        audioSource_.PlayOneShot(audioClip_, volumeScale_);
        */
    }


    // End method called when action finishes
    public override void End()
    {

    }


    // Called every frame the action is happening
    public override bool Update()
    {

        FMOD.Studio.PLAYBACK_STATE audioState;
        audioEv_.getPlaybackState(out audioState);

        if (audioState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {

            audioEv_.release();
            actionRunning_ = false;
        }

        return actionRunning_;
    }

}
