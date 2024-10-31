using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool playerHasControl;
    bool gameOver;
    float realSpeed;
    int playerLives;
    public GameObject player;

    float fauxSpeed;
    public TMP_Text timerText;
    public TMP_Text speedText;

    public GameObject live1, live2, live3;

    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        fauxSpeed = 0f;
        live1.SetActive(true);
        live2.SetActive(true);
        live3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        realSpeed = player.GetComponent<PlayerManager>().curSpeed;
        playerHasControl = player.GetComponent<PlayerManager>().hasControl;
        gameOver = player.GetComponent<PlayerManager>().gameOver;
        playerLives = player.GetComponent<PlayerManager>().PlayerLives;
        if (playerHasControl && !gameOver)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimer();
        }
        UpdateSpeedometer();
        UpdateLives();
    }
    void UpdateTimer()
    {
        int mins = Mathf.FloorToInt(elapsedTime / 60f);
        int secs = Mathf.FloorToInt(elapsedTime - mins * 60);
        int msecs = Mathf.FloorToInt((elapsedTime * 1000f) - secs * 1000 - mins * 60000);
        string timerString = string.Format("{0:00}:{1:00}:{2:000}", mins, secs, msecs);
        timerText.text = timerString;
    }
    void UpdateSpeedometer()
    {
        float speedChange = (fauxSpeed - realSpeed);
        fauxSpeed -= speedChange * Time.deltaTime;

        string speedRead = Mathf.FloorToInt(fauxSpeed).ToString();
        if (gameOver) { speedRead = 0.ToString(); };
        speedText.text = speedRead;
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
        else if(playerLives == 0)
        {
            live3.SetActive(false);
        }
    }
}
