using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentGeneration : MonoBehaviour
{
    public int Difficulty;
    public Transform[] BuildingSlots;
    public GameObject[] Buildings;

    public Transform RoadPos;
    public GameObject Road;

    public Transform[] ObstaclePoints;
    public GameObject ObstacleTemplate;
    
    //Used for spawning the buildings in correct intervals
    public Transform SpawnDeterminatorSpawn;
    public GameObject SpawnDeterminator;
    // Start is called before the first frame update
    void Start()
    {
        DoGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void DoGeneration()
    {
        GenerateBuilding();
        GenerateRoad();
        GenerateObstacle();
        Instantiate(SpawnDeterminator, SpawnDeterminatorSpawn.position, SpawnDeterminatorSpawn.rotation);
    }
    public void GenerateBuilding()
    {
      for (int i = 0; i < BuildingSlots.Length; i++)
        {
            Instantiate(Buildings[Random.Range(0, Buildings.Length)], BuildingSlots[i].transform);
        }
        
       
    }
    void GenerateRoad()
    {
        Instantiate(Road, RoadPos);
    }
    void GenerateObstacle()
    {
        for (int i = 0;i < ObstaclePoints.Length && i < Difficulty+1; i++)
        {
            Instantiate(ObstacleTemplate, ObstaclePoints[i]);
        }
    }

}
