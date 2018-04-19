using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour {


    [FMODUnity.EventRef]
    public string sound_;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Play(int meh = 0)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound_);
    }

}
