using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Other scripts and var determined by them

    public bool playerHasControl;
    public bool giveControl;
    public bool isGameLevel;
    public bool cruiseMode;
    public bool gameOver;
    public bool wasVictorious;
    public int Difficulty;
    public int levelSize;
    
   
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        playerHasControl = giveControl;
        
    }
    
    
}
