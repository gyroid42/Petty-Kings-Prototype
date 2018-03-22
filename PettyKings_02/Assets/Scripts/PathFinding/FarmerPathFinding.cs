using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerPathFinding : MonoBehaviour {

    // Use this for initialization
    NavMeshAgent agent;
    public GameObject[] objectsInScene;
    // public static int Range;
    private int rnd;
    bool moving = false;
    float timeLeft;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // objectsInScene = GameObject.FindGameObjectsWithTag("Building");
        Random.InitState(System.DateTime.Now.Millisecond);
        rnd = Random.Range(0, objectsInScene.Length);
        timeLeft = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        objectsInScene = GameObject.FindGameObjectsWithTag("Farm");

        if (Vector3.Distance(objectsInScene[rnd].gameObject.transform.position, agent.transform.position) < 10.5f)
        {
            moving = false;

        }
        else
        {
            moving = true;
            agent.SetDestination(objectsInScene[rnd].gameObject.transform.position);
        }

        if (!moving)
        {

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = Random.Range(1, 4);
                rnd = Random.Range(0, objectsInScene.Length);
                agent.SetDestination(objectsInScene[rnd].gameObject.transform.position);
            }

        }

    }

}
