using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Action", menuName = "EventActions/Audio")]
public class AudioAction : BaseAction
{

    // properties
    public AudioClip audioClip_;
    [Range(0.0f, 1.0f)]
    public float volumeScale_;

    private AudioSource audioSource_;

    // Begin method called when action starts
    public override void Begin(Event newEvent)
    {
        base.Begin(newEvent);

        type_ = ACTIONTYPE.AUDIO;

        audioSource_ = EventController.eventController.gameObject.GetComponent<AudioSource>();

        audioSource_.PlayOneShot(audioClip_, volumeScale_);
    }


    // End method called when action finishes
    public override void End()
    {

    }


    // Called every frame the action is happening
    public override bool Update()
    {


        if (!audioSource_.isPlaying)
        {
            actionRunning_ = false;
        }

        return actionRunning_;
    }

}
