using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Other scripts and var derived from them
    public LevelManager LevelManager;
    bool playerHasControl;
    bool gameOver;
    bool wasVictorious;
    bool prepareEnd;
    bool gameStart;

    public PlayerManager PlayerManager;
    float realSpeed;
    int playerLives;

    public SoundManager SoundManager;

    //Speed
    public TMP_Text speedText;
    float fauxSpeed;

    //Dist
    public TMP_Text distance;
    float fauxDist;

    //Lives
    public GameObject live1, live2, live3;

    //Timer
    public TMP_Text timerText;
    int mins, secs, msecs;
    float elapsedTime;
    string timerString;

    //Level screen
    public string[] part_1, part_2;
    string levelTitle;
    public TMP_Text LevelTitleText;
    public TMP_Text LevelLengthText;

    //Game end screens, fadescreen and endbuttons
    public GameObject FadeScreen, WinScreen, LoseScreen, EndButtons;
    public TMP_Text NextButton;
    //StatTexts
    public TMP_Text wRunStats, lRunStats;

    //Utils
    bool hasRunEnd;

    void Start()
    {
        //Activate and deactivate different elements
        live1.SetActive(true);
        live2.SetActive(true);
        live3.SetActive(true);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        EndButtons.SetActive(false);

        //Set hasRunEnd, Determines if endscreen has been shown
        hasRunEnd = false;

        LevelScreen();
    }

    // Update is called once per frame
    void Update()
    {
        realSpeed = PlayerManager.curSpeed;
        playerLives = PlayerManager.PlayerLives;
        playerHasControl = LevelManager.playerHasControl;
        gameOver = LevelManager.gameOver;
        wasVictorious = LevelManager.wasVictorious;
        prepareEnd = LevelManager.prepareEnd;
        gameStart = LevelManager.gameStart;

        //if we are moving and stuff is happening, update time and speed
        if (playerHasControl && !gameOver && PlayerManager.curGear != 0)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimer();
            UpdateSpeedometer();
        }

        UpdateLives();
        UpdateDistance();

        if (gameStart)
        {
            //fade the level screen
            FadeScreen.GetComponent<Animator>().SetBool("StartFadeIn", true);
        }

        if (prepareEnd)
        {
            EndGame();
        }

    }
    void UpdateTimer()
    {
        //update the level timer
        mins = Mathf.FloorToInt(elapsedTime / 60f);
        secs = Mathf.FloorToInt(elapsedTime - mins * 60);
        msecs = Mathf.FloorToInt((elapsedTime * 1000f) - secs * 1000 - mins * 60000);
        timerString = string.Format("{0:00}:{1:00}:{2:000}", mins, secs, msecs);
        timerText.text = timerString;
    }
    void UpdateSpeedometer()
    {
        //create speed reading by generating a faux speed that matches the desired actual speed
        //better than unity speed does. Also change this speed when actual speed is changed in a more believable manner
        //to simulate acceleration
        float speedChange = (fauxSpeed - realSpeed);
        fauxSpeed -= speedChange * Time.deltaTime;

        string speedRead = Mathf.FloorToInt(fauxSpeed * 2).ToString();
        if (gameOver) { speedRead = 0.ToString(); };
        speedText.text = speedRead + "Km/h";
    }
    void UpdateLives()
    {
        //Update the player lives icons
        if (playerLives == 2)
        {
            live1.SetActive(false);
        }
        else if (playerLives == 1)
        {
            live2.SetActive(false);
        }
        else if (playerLives == 0)
        {
            live3.SetActive(false);
        }
    }
    void UpdateDistance()
    {
        //Update distance based on generation segments. To alliviate the problem of them being
        //in 50m integers, we generate a faux distance to simulate them being real meters
        
        float realDist = LevelManager.curDist;

        float distChange = (fauxDist - realDist);
        fauxDist -= distChange * Time.deltaTime;
        distance.text = Mathf.FloorToInt(fauxDist).ToString() + "m";
    }
    void LevelScreen()
    {
        //Generate a name for the level
        string first, last;
        first = part_1[Random.Range(0, part_1.Length - 1)];
        last = part_2[Random.Range(0, part_2.Length - 1)];

        levelTitle = first + " " + last;
        LevelTitleText.text = levelTitle;

        string levelLength = (LevelManager.levelSize * 50).ToString();
        LevelLengthText.text = levelLength + "m";

    }
    void EndGame()
    {
        //determine end screen. Make sure this is only run once by using hasRunEnd. 
        if (!hasRunEnd)
        {
            //Hide game UI elements when we know the game is ending
            timerText.text = "";
            speedText.text = "";
            live1.SetActive(false);
            live2.SetActive(false);
            live3.SetActive(false);
            //Start levelscreen fade in
            FadeScreen.GetComponent<Animator>().SetBool("StartFadeOut", true);
         
            if (gameOver && wasVictorious)
            {
                //if player was succesful, we create the win screen and UI buttons for next level
                gameStart = false;
                SoundManager.PlaySound("WinSound");
                WinScreen.SetActive(true);
                EndButtons.SetActive(true);
                //determine end stats text
                string TimeTaken = timerString;
                float avgSpeed = (LevelManager.levelSize * 50) / elapsedTime;
                string avgSpeedString = Mathf.FloorToInt(avgSpeed*2).ToString();
                wRunStats.text = "Time taken: " + TimeTaken + "\n" + "Average Speed: " + avgSpeedString+"Km/h";
                //If no next level exists, return to main menu instead
                if(SceneManager.GetActiveScene().buildIndex != 4)
                {
                    NextButton.text = "Next Level";
                }
                else
                {
                    NextButton.text = "Return to menu";
                }
                

                hasRunEnd = true;
            }
            if (gameOver && !wasVictorious)
            {
                //If player failed, generate lose screen and UI buttons to retry or quit
                gameStart = false;
                //instantly turn the screen dark
                FadeScreen.GetComponent<Animator>().SetBool("Dark", true);
                SoundManager.PlaySound("LoseSound");
                LoseScreen.SetActive(true);
                EndButtons.SetActive(true);
                //Determine endgame stats
                string DistanceSurvived = Mathf.FloorToInt(fauxDist).ToString();
                lRunStats.text = "Time Survived: " + timerString + "\n" + "Distance Survived: " + DistanceSurvived + "m";
                NextButton.text = "Retry";

                hasRunEnd = true;
            }
        }
    }
    public void NextLevel()
    {
        //Next level button controls. Is used for retry and eventually turns into return to menu button too
        if (wasVictorious)
        {
            
            if (SceneManager.GetActiveScene().buildIndex != 4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       
        
    }
    
    public void QuitToMenu()
    {
        //control return to main menu button
        SceneManager.LoadScene("MainMenu");
    }
}
