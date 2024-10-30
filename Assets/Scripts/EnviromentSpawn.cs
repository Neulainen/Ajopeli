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
        goal = GameObject.Find("SpawnGoal").transform;
        generation = GameObject.Find("Enviroment Scripts").GetComponent<EnviromentGeneration>();
    }

   
    void Update()
    {
        speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z<goal.position.z) 
        {
            generation.DoGeneration();
            Destroy(gameObject);
        }
    }
}
