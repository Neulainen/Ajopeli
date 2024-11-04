using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Other scripts and var from them
    public LevelManager LevelManager;
    bool isLevel, gameOver, playerHasControl;

    public SoundManager SoundManager;

    //Movement inputs
    public float curSpeed, SteerInput;
    public short curGear;
    

    //Player speeds, meant to take curGear as index number
    public float[] steerSpeeds;
    public short[] gearSpeeds;

    //Player lives and damage indications
    readonly private short PlayerLivesMax = 3;
    public int PlayerLives;
    public ParticleSystem DMG1, DMG2, DMG3;

    void Start()
    {
        LevelManager = LevelManager.GetComponent<LevelManager>();

        //Check if we should treat this as a game level
        isLevel = LevelManager.isGameLevel;

        //Reset gameover
        gameOver = LevelManager.gameOver;

        //reset player lives to max
        PlayerLives = PlayerLivesMax;

        //Player can't start level with 0 gear, ie without speed
        if (isLevel) { curGear++; }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerHasControl = LevelManager.playerHasControl;
        gameOver = LevelManager.gameOver;

        ChangeGear();
        SteerInput = Input.GetAxis("Horizontal");

        curSpeed = DetermineSpeed(curGear);
        Movement(SteerInput);
        CarSoundMixer();

    }
    void CarSoundMixer()
    {
        //Pitch sounds up and down depending on used gear
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
            //Keep player y,z locked, use player input to determine x
            transform.Translate(new Vector3(input * Time.deltaTime * steerSpeeds[curGear], 0, 0));
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        if (!playerHasControl)
        {
            //Force player to middle when not in control by determining
            //their distance from midpoint and moving towards it. Midpoint should be 0,0,0
            float distToMid = Mathf.Clamp(Vector3.zero.x - transform.position.x, -1, 1);
            transform.Translate(new Vector3(distToMid * Time.deltaTime, 0, 0));
        }
    }
    void ChangeGear()
    {
        //As long as game is not over and player has control, they can change gears up or down,
        //provided they are not at the limit
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
        //Determine speed for each frame
        float finalSpeed;
        finalSpeed = gearSpeeds[gear];
        return finalSpeed;

    }
    void TakeLife()
    {
        //Take away life, if lives are over, give failure ending
        PlayerLives--;
        if (PlayerLives < 0)
        {
            GameOver();
        }

        SetDamage();
        SoundManager.PlaySound("Crash");
    }
    void SetDamage()
    {
        //Set damage indicator particle effects
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
        else if (LevelManager.prepareEnd)
        {
            //Pause effects on game end
            DMG1.Pause();
            DMG2.Pause();
            DMG3.Pause();
        }

    }
    void GameOver()
    {
        //raise flags for endgame determination.
        LevelManager.prepareEnd = true;
        LevelManager.wasVictorious = false;
        LevelManager.gameOver = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //If collision is with obstacle, take a life away
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeLife();
        }
        //If collision is with wall, return player toward track
        else if (collision.gameObject.CompareTag("Wall"))
        {
            transform.Translate(-SteerInput, 0, 0);
        }
        //If we collide with the level start marker, start level and give player control
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
