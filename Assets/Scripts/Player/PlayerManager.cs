using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public LevelManager LevelManager;
    bool isLevel;
    bool playerHasControl;

    public BoxCollider mainColl;

    public bool gameOver;

    public short[] gearSpeeds;
    public float curSpeed;
    public float SteerInput;
    public short curGear = 1;
    public ParticleSystem DMG1, DMG2, DMG3;

    public float[] steerSpeeds;

    public Transform carBody;

    readonly private short PlayerLivesMax = 3;
    public int PlayerLives;
    // Start is called before the first frame update
    void Start()
    {
        mainColl = GetComponent<BoxCollider>();
        LevelManager = GetComponent<LevelManager>();
        isLevel = GetComponent<LevelManager>().isGameLevel;
        PlayerLives = PlayerLivesMax;
        if (isLevel) { curGear = 1; }
    }

    // Update is called once per frame
    void Update()
    {
       playerHasControl = GetComponent<LevelManager>().playerHasControl;
       
        ChangeGear();
        curSpeed = DetermineSpeed(curGear);
        SteerInput = Input.GetAxis("Horizontal");
        Movement(SteerInput);

    }
    void Movement(float input)
    {
        if (!gameOver&&playerHasControl)
        {
            transform.Translate(new Vector3(input * Time.deltaTime * steerSpeeds[curGear], 0, 0));
            transform.position = new Vector3(transform.position.x, 0, 0);
            //carBody.Rotate(Vector3.up, SteerInput * steerSpeeds[curGear] /10);
        }
    }
    void ChangeGear()
    {
        if (!gameOver&&playerHasControl)
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

    }
    void GetReward()
    {

    }
    void GameOver()
    {
        gameOver = true;
    }
    void Crash()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Crash();
            TakeLife();
        }
        else if (collision.gameObject.CompareTag("Collectible"))
        {
            GetReward();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (collision.gameObject.name == "WallL")
            {
                transform.Translate(Vector3.right * 3);
            }
            else if (collision.gameObject.name == "WallR")
            {
                transform.Translate(Vector3.left * 3);
            }
        }
        else if (collision.gameObject.CompareTag("ControlGiver"))
        {
            if (playerHasControl)
            {
                LevelManager.playerHasControl = false;
            }
            else if (!playerHasControl)
            {
                LevelManager.playerHasControl = true;
            }

        }
    }
}
