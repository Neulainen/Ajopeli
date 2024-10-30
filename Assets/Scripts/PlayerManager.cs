using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider mainColl;

    public short[] gearSpeeds;
    public float curSpeed;
    public float SteerInput;
    public float turboMod = .5f;
    public short curGear = 1;

    public float[] steerSpeeds;

    public Transform carBody;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainColl = GetComponent<BoxCollider>();


    }

    // Update is called once per frame
    void Update()
    {
       SteerInput = Input.GetAxis("Horizontal");
        changeGear();
        Turbo();
        curSpeed = DetermineSpeed(curGear);
        Movement(SteerInput);

         

    }
    void Movement(float input)
    {
       

        transform.Translate(new Vector3(input * Time.deltaTime * steerSpeeds[curGear], 0, 0));
        transform.position = new Vector3(transform.position.x, 0, 0);
        //carBody.Rotate(Vector3.up, SteerInput * steerSpeeds[curGear] /10);
    }
    void changeGear()
    {
        if (Input.GetButtonDown("GearUp"))
        {
            if (curGear < gearSpeeds.Length-1)
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
    short Turbo()
    {
        if (Input.GetButtonDown("Turbo"))
        {
            return 1;
        }
        else 
        {
            return 0;
        }

        
    }
    float DetermineSpeed(short gear)
    {
        float finalSpeed;

        finalSpeed = gearSpeeds[gear] * 1 + (Turbo() * turboMod);
        return finalSpeed;

    }
}
