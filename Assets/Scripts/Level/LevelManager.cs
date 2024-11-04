using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Other scripts
    public SoundManager SoundManager;
    public EnviromentGeneration EnviromentGeneration;

    //Utility
    public bool playerHasControl; //should player be able to control the vehicle?
    public bool isGameLevel; //Determines if we should treat this scene as a level
    public bool cruiseMode; //stops Obstacles from spawning
    public int curDist;

       
    [Range(40, 200)]
    public int levelSize;
    [System.NonSerialized]
    public bool gameStart; //Startflag, used by other scripts

    

    //Level End
    [System.NonSerialized]
    public bool prepareEnd, wasVictorious, gameOver;

    //Misc
    bool soundsStarted;
    void Update()
    {
        //Determine current distance. Formula is derived from the knowledge that each segment is about 50 meters,
        //and there is about 200 meters of no segments that the player traverses trough before the level starts
        curDist = EnviromentGeneration.curSize*50-200;

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
        //Takes away player control and stops looped sounds
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
        //Start looped sounds
        SoundManager.PlaySound("CarNoise");
        SoundManager.PlaySound("EngineNoise");
        SoundManager.PlayMusicMix();
    }


}
