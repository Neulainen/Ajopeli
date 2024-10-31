using UnityEngine;

public class TireAnimator : MonoBehaviour
{
    float speed;
    bool gameOver;

    public Transform rightTire;
    public Transform leftTire;
    public Transform backTires;
    void Update()
    {
        gameOver = GameObject.Find("=Player=").GetComponent<PlayerManager>().gameOver;
        if(!gameOver)
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
}
