using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour {

    private WorldManager worldController;

    private GameObject wallFlames;
    private GameObject smokeEffect;

    private GameObject wallFlamesClone;
    private GameObject smokeEffectClone;

    private float timer, timeToNexteffect, endEffect;

    private bool isChanged = false;

    Collider coll;
    
	// Use this for initialization
	void Start () {

        timer = 0;
        timeToNexteffect = 5.0f;
        endEffect = 10.0f;

		wallFlames = Resources.Load("Particle Effects/WallFlames") as GameObject; //0
        smokeEffect = Resources.Load("Particle Effects/SmokeEffect") as GameObject; //1
        wallFlamesClone = Instantiate(wallFlames, transform);
        wallFlamesClone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

       coll = GetComponentInParent<Collider>();

        isChanged = false;

       

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer >= endEffect)
        {
            EndEffect();
        }
        else if(timer >= timeToNexteffect)
        {
            ChangeParticle();
            
        }
        if(wallFlamesClone)
        {
            wallFlamesClone.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }
        if(smokeEffectClone)
        {
            smokeEffectClone.transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
        }
        //transform.parent.position += new Vector3(0, -0.5f, 0);

        Debug.Log(timer);
	}

    private void ChangeParticle()
    {
        if(!isChanged)
        {
            //smokeEffectClone = Instantiate(smokeEffect);
            smokeEffectClone.transform.position = this.transform.position;
            smokeEffectClone.transform.localScale = new Vector3(8, 8, 8);
            Destroy(wallFlamesClone);
            coll.enabled = false;
            isChanged = true;
        }
        else
        {
            return;
        }
    }

    private void EndEffect()
    {
        Destroy(smokeEffectClone);
        Destroy(transform.parent.gameObject);
        Destroy(this);
    }
}
