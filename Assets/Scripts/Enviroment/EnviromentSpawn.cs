using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSpawn : MonoBehaviour
{
    Transform goal;
    EnviromentGeneration generation;
    float speed;
    
    void Start()
    {
        //Find position of goal and generation script
        goal = GameObject.Find("SpawnGoal").transform;
        generation = GameObject.Find("Enviroment Scripts").GetComponent<EnviromentGeneration>();
    }

   
    void LateUpdate()
    {
        //Move towards player at speed derived from player, when we hit goal, do generation
        speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
        transform.Translate(speed * Time.deltaTime * Vector3.back);
        if (transform.position.z<goal.position.z) 
        {
            generation.DoGeneration();
            Destroy(gameObject);
        }
    }
}
