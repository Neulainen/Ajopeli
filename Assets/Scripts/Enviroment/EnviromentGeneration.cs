using UnityEngine;

public class EnviromentGeneration : MonoBehaviour
{
    //Other Scripts and var determined by them   
    public GameObject LevelScript;
    LevelManager LevelManager;
    bool CruiseMode;
    bool gameOver;
    bool playerHasControl;
    int levelSize;
    public int curSize;

    /*
    Prefab spawnables. Buildings, road and Obstacle template spawn enviroment objects that also generate
    their own subobjects. 
    Marks are used to determine certain event in game. SpawnMark determines when to spawn a new enviroment set,
    level mark marks the beginning and end of level. A LevelMark is spawned with the first generation. 
    It is used to give the player control when they enter the generated area.
    */
    public GameObject[] Buildings;
    public GameObject Road, ObstacleTemplate, SpawnMark, levelMark;
    /*
     Used for determining the spawnlocations of these gameobjects relative to the world. Obstacle points are determined trough
    LevelManager difficulty. Each level of difficulty adds one ObstacleTemplate / enviromentGeneration. 
    Building slots are automatically filled with the Buildings of the Buildings array.
    */
    public Transform SpawnDeterminatorSpawn, levelMarkPoint, RoadSpawnPos, ObstaclePoint;
    public Transform[] BuildingSlots;



    //Used for determination of level length
    void Start()
    {
        LevelManager = LevelScript.GetComponent<LevelManager>();
        levelSize = LevelManager.levelSize;
        DoGeneration();
        Instantiate(levelMark, levelMarkPoint.position, levelMarkPoint.rotation);

    }

    void LateUpdate()
    {
        playerHasControl = LevelManager.playerHasControl;
        CruiseMode = LevelManager.cruiseMode;
        gameOver = LevelManager.gameOver;

        CheckLevelProgress();
    }
    public void DoGeneration()
    {
        //If game is not over, generate enviroment. Keep count of generated segments trough curSize. If we are in cruisemode,
        //obstacles should not be spawned.
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
        //generate a building from array to every slot
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
        //generate obstacle template
        if (playerHasControl)
        {
            Instantiate(ObstacleTemplate, ObstaclePoint);
        }
    }
    void CheckLevelProgress()
    {
        //Keep up with level progress. Once the size of the level has been reached, raise appropriate flags and 
        //prepare for player victory
        if (levelSize < curSize)
        {
            CruiseMode = true;
            if (levelSize + 15 < curSize)
            {
                LevelManager.prepareEnd = true;
            }

            if (levelSize + 25 < curSize)
            {
                LevelManager.gameOver = true;
                LevelManager.wasVictorious = true;
            }
        }
    }
}
