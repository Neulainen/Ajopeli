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
    [Range(40, 200)]
    public int levelSize;
    [System.NonSerialized]
    public bool gameStart; //Starts everything

    

    //Level End
    [System.NonSerialized]
    public bool prepareEnd, wasVictorious, gameOver;

    //Misc
    bool soundsStarted;
    void Update()
    {
        curDist = EnviromentGeneration.curSize*40-100;
        if (gameStart)
        {
            StartLevel();
        }
        if (prepareEnd)
        {     
            EndLevel();
        }
    }
    void EndLevel()
    {
        playerHasControl = false;
        SoundManager.StopSound("CarNoise");
        SoundManager.StopSound("EngineNoise");
        SoundManager.StopMusicMix();
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
