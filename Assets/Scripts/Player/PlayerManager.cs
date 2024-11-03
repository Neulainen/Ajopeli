using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public LevelManager LevelManager;
    bool isLevel, gameOver, playerHasControl;

    public SoundManager SoundManager;

    public short[] gearSpeeds;
    public float curSpeed, SteerInput;
    public short curGear = 1;
    public ParticleSystem DMG1, DMG2, DMG3;

    public float[] steerSpeeds;

    readonly private short PlayerLivesMax = 3;
    public int PlayerLives;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = LevelManager.GetComponent<LevelManager>();

        isLevel = LevelManager.isGameLevel;
        gameOver = LevelManager.gameOver;

        PlayerLives = PlayerLivesMax;
        if (isLevel) { curGear++; }
    }

    // Update is called once per frame
    void Update()
    {
        playerHasControl = LevelManager.playerHasControl;
        gameOver = LevelManager.gameOver;

        ChangeGear();
        curSpeed = DetermineSpeed(curGear);
        SteerInput = Input.GetAxis("Horizontal");
        Movement(SteerInput);
        CarSoundMixer();

    }
    void CarSoundMixer()
    {
        switch (curGear)
        {
            case 1:
                {
                    SoundManager.PitchSound("EngineNoise", .66f);
                    SoundManager.PitchSound("CarNoise", .74f);
                    break;
                }
            case 2:
                {
                    SoundManager.PitchSound("EngineNoise", 1f);
                    SoundManager.PitchSound("CarNoise", 1.2f);
                    break;
                }
            case 3:
                {
                    SoundManager.PitchSound("EngineNoise", 1.3f);
                    SoundManager.PitchSound("CarNoise", 1.6f);
                    break;
                }

        }
        
        
    }
    void Movement(float input)
    {
        if (!gameOver && playerHasControl)
        {
            transform.Translate(new Vector3(input * Time.deltaTime * steerSpeeds[curGear], 0, 0));
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        if (!playerHasControl)
        {
            //Force player to middle when not in control
            Vector3 midPoint = Vector3.zero;
            float distToMid =  midPoint.x - transform.position.x;
            distToMid = Mathf.Clamp(distToMid, -1, 1);
            transform.Translate(new Vector3(distToMid * Time.deltaTime, 0, 0));
        }
    }
    void ChangeGear()
    {
        if (!gameOver && playerHasControl)
        {
            if (Input.GetButtonDown("GearUp"))
            {
                if (curGear < gearSpeeds.Length - 1)
                {
                    curGear++;
                    
                }
            }
            else if (Input.GetButtonDown("GearDown"))
            {
                if (curGear > 1)
                {
                    curGear--;
                }
            }
        }

    }
    float DetermineSpeed(short gear)
    {
        float finalSpeed;
        finalSpeed = gearSpeeds[gear];
        return finalSpeed;

    }
    void TakeLife()
    {
        SoundManager.PlaySound("Crash");
        PlayerLives--;
        if (PlayerLives < 0)
        {
            GameOver();
        }
        SetDamage();

    }
    void SetDamage()
    {
        if (PlayerLives == 2)
        {
            DMG1.Play();
        }
        else if (PlayerLives == 1)
        {
            DMG2.Play();
        }
        else if (PlayerLives == 0)
        {
            DMG3.Play();
        }
        else if (gameOver)
        {
            DMG1.Pause();
            DMG2.Pause();
            DMG3.Pause();
        }

    }
    void GameOver()
    {
        LevelManager.prepareEnd = true;
        LevelManager.wasVictorious = false;
        LevelManager.gameOver = true;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeLife();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
                transform.Translate(-SteerInput*Time.deltaTime, 0, 0);    
        }
        else if (collision.gameObject.CompareTag("ControlGiver"))
        {
            if (!playerHasControl)
            {
                LevelManager.gameStart = true;
                LevelManager.playerHasControl = true;
            }
        }
    }
}
