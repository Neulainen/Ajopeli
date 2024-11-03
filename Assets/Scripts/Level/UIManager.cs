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
    //StatTexts
    public TMP_Text wRunStats, lRunStats;

    //Utils
    bool hasRunEnd;

    void Start()
    {
        live1.SetActive(true);
        live2.SetActive(true);
        live3.SetActive(true);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        EndButtons.SetActive(false);

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
            FadeScreen.GetComponent<Animator>().SetBool("StartFadeIn", true);
        }

        if (prepareEnd)
        {
            EndGame();
        }

    }
    void UpdateTimer()
    {
        mins = Mathf.FloorToInt(elapsedTime / 60f);
        secs = Mathf.FloorToInt(elapsedTime - mins * 60);
        msecs = Mathf.FloorToInt((elapsedTime * 1000f) - secs * 1000 - mins * 60000);
        timerString = string.Format("{0:00}:{1:00}:{2:000}", mins, secs, msecs);
        timerText.text = timerString;
    }
    void UpdateSpeedometer()
    {
        float speedChange = (fauxSpeed - realSpeed);
        fauxSpeed -= speedChange * Time.deltaTime;

        string speedRead = Mathf.FloorToInt(fauxSpeed * 2).ToString();
        if (gameOver) { speedRead = 0.ToString(); };
        speedText.text = speedRead + "Km/h";
    }
    void UpdateLives()
    {
        //V‰henn‰ kuvien m‰‰r‰‰
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
        float realDist = LevelManager.curDist;

        float distChange = (fauxDist - realDist);
        fauxDist -= distChange * Time.deltaTime;
        distance.text = fauxDist.ToString() + "m";
    }
    void LevelScreen()
    {
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
        if (!hasRunEnd)
        {
            timerText.text = "";
            speedText.text = "";
            live1.SetActive(false);
            live2.SetActive(false);
            live3.SetActive(false);
            FadeScreen.GetComponent<Animator>().SetBool("StartFadeOut", true);
            if (gameOver && wasVictorious)
            {
                gameStart = false;
                SoundManager.PlaySound("WinSound");
                WinScreen.SetActive(true);
                EndButtons.SetActive(true);
                string TimeTaken = timerString;
                float avgSpeed = (LevelManager.levelSize * 50) / elapsedTime;
                string avgSpeedString = avgSpeed.ToString();
                wRunStats.text = "Time taken: " + TimeTaken + "\n" + "Average Speed: " + avgSpeedString;

                hasRunEnd = true;
            }
            if (gameOver && !wasVictorious)
            {
                gameStart = false;
                FadeScreen.GetComponent<Animator>().SetBool("Dark", true);
                SoundManager.PlaySound("LoseSound");
                LoseScreen.SetActive(true);
                EndButtons.SetActive(true);
                string DistanceSurvived = (LevelManager.curDist / 1000).ToString();
                lRunStats.text = "Time Survived: " + timerString + "\n" + "Distance Survived: " + DistanceSurvived + "km";

                hasRunEnd = true;
            }
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
