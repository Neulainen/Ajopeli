using UnityEngine;

public class EnviromentGeneration : MonoBehaviour
{
    //Other Scripts and var determined by them   
    public GameObject LevelScript;
    LevelManager LevelManager;
    bool CruiseMode; 
    int difficulty;
    bool gameOver;
    bool playerHasControl;
    int levelSize;
        int curSize;
    bool allSpawned;

    /*
    Prefab spawnables. Buildings, road and Obstacle template spawn enviroment objects that also generate
    their own subobjects. 
    Marks are used to determine certain event in game. SpawnMark determines when to spawn a new enviroment set,
    level mark marks the beginning and end of level. First level mark is spawned along with the first enviroment generation set, 
    second is spawned by LevelManager once it determines the level is over. These also take away the players ability to control
    the vehicle.
    */
    public GameObject[] Buildings;
    public GameObject Road, ObstacleTemplate, SpawnMark, levelMark;
    /*
     Used for determining the spawnlocations of these gameobjects relative to the world. Obstacle points are determined trough
    LevelManager difficulty. Each level of difficulty adds one ObstacleTemplate / enviromentGeneration. 
    Building slots are automatically filled with the Buildings of the Buildings array.
    */
    public Transform SpawnDeterminatorSpawn, levelMarkPoint, RoadSpawnPos;
    public Transform[] BuildingSlots, ObstaclePoints;

    //Used for determination of level length
    void Start()
    {
        LevelManager = LevelScript.GetComponent<LevelManager>();
        levelSize = LevelManager.levelSize;

        DoGeneration();
        Instantiate(levelMark,levelMarkPoint.position,levelMarkPoint.rotation);
    }

    void Update()
    {
        playerHasControl = LevelManager.playerHasControl;
        CruiseMode = LevelManager.cruiseMode;
        gameOver = LevelManager.gameOver;
        difficulty = LevelManager.Difficulty;
        if(levelSize < curSize)
        {
            CruiseMode = true;
            allSpawned = true;//aloita toimenpiteet voiton merkitsemiseksi
            if (curSize > levelSize + 2)
            {

            }
        }
        
    }
    public void DoGeneration()
    {
        if (!gameOver)
        {
            curSize++;
            GenerateBuildings();
            GenerateRoad();
            if (!CruiseMode) { GenerateObstacle(); }
            Instantiate(SpawnMark, SpawnDeterminatorSpawn.position, SpawnDeterminatorSpawn.rotation);
        }
    }
    public void GenerateBuildings()
    {
        for (int i = 0; i < BuildingSlots.Length; i++)
        {
            Instantiate(Buildings[Random.Range(0, Buildings.Length)], BuildingSlots[i].transform);
        }
    }
    void GenerateRoad()
    {
        Instantiate(Road, RoadSpawnPos);
    }
    void GenerateObstacle()
    {
        if (playerHasControl)
        {
            for (int i = 0; i < ObstaclePoints.Length && i < difficulty + 1; i++)
            {
                Instantiate(ObstacleTemplate, ObstaclePoints[i]);
            }
        }
    }
}
