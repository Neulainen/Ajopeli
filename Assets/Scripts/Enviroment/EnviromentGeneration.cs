using UnityEngine;

public class EnviromentGeneration : MonoBehaviour
{
    bool gameOver;
    bool playerHasControl;
    public GameObject player;


    public bool CruiseMode;

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
    //Used for marking level start and end
    public Transform levelMarkPoint;
    public GameObject levelMark;

    void Start()
    {
        DoGeneration();
        Instantiate(levelMark,levelMarkPoint.position,levelMarkPoint.rotation);
    }

    void Update()
    {
        gameOver = player.GetComponent<PlayerManager>().gameOver;
        playerHasControl = player.GetComponent<PlayerManager>().hasControl;

    }
    public void DoGeneration()
    {
        if (!gameOver)
        {
            GenerateBuilding();
            GenerateRoad();
            if (!CruiseMode) { GenerateObstacle(); }
            Instantiate(SpawnDeterminator, SpawnDeterminatorSpawn.position, SpawnDeterminatorSpawn.rotation);
        }
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
        if (playerHasControl)
        {
            for (int i = 0; i < ObstaclePoints.Length && i < Difficulty + 1; i++)
            {
                Instantiate(ObstacleTemplate, ObstaclePoints[i]);
            }
        }

    }

}
