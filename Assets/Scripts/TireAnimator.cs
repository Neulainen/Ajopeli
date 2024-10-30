using UnityEngine;

public class TireAnimator : MonoBehaviour
{
    public Transform rightTire;
    public Transform leftTire;
    public Transform backTires;
    float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
        //Deternime rotationspeed 
        Vector3 wheelSpeed = (Vector3.right * speed / 10) / 6;
        //Rotate wheels forwards
        backTires.transform.Rotate(wheelSpeed);
        rightTire.transform.Rotate(wheelSpeed);
        leftTire.transform.Rotate(wheelSpeed);
       
       
        
        
    }
}
