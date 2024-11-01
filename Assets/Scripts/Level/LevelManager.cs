using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Other scripts and var determined by them
    public EnviromentGeneration enviromentGeneration;

    public bool playerHasControl;
    public bool isGameLevel;
    public bool cruiseMode;
    public bool gameOver;
    public bool wasVictorious;
    public int Difficulty;
    
   
    // Start is called before the first frame update
    void Start()
    {
        if (isGameLevel)
        {
            enviromentGeneration = GetComponent<EnviromentGeneration>();
        }

    }

    // Update is called once per frame
    void Update()
    {
       if(enviromentGeneration != null&&isGameLevel)
        {


        } 
        
    }
    
    
}
