using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public SoundManager SoundManager;
    public EnviromentGeneration EnviromentGeneration;

    //Utility
    public bool playerHasControl; //should player be able to control the vehicle?
    public bool isGameLevel; //Determines if we should treat this scene as a level
    public bool cruiseMode; //stops Obstacles from spawning
    public int curDist;

    //Level Start
    [System.NonSerialized]
    public int levelSize; 
    public int levelSizeRange;
    [System.NonSerialized]
    public bool gameStart; //Starts everything

    

    //Level End
    [System.NonSerialized]
    public bool prepareEnd, wasVictorious, gameOver;

    //Misc
    bool soundsStarted;

    void Awake()
    {
        GenerateLevel();

    }
    void Update()
    {
        curDist = EnviromentGeneration.curSize*50-100;
        if (gameStart)
        {
            StartLevel();
        }
        if (prepareEnd)
        {
            SoundManager.StopSound("CarNoise");
            SoundManager.StopSound("EngineNoise");
            SoundManager.StopMusicMix();
            EndLevel();
        }


    }
    void GenerateLevel()
    {
        levelSize = Random.Range(50,levelSizeRange);
    }
    void EndLevel()
    {
        playerHasControl = false;

    }
    void StartLevel()
    {
        if(!soundsStarted) { BeginSounds(); soundsStarted = true; }
        
    }
    void BeginSounds()
    {
        SoundManager.PlaySound("CarNoise");
        SoundManager.PlaySound("EngineNoise");
        SoundManager.PlayMusicMix();
    }


}
