using UnityEngine;

public class TireAnimator : MonoBehaviour
{
    public LevelManager LevelManager;
    bool gameOver;

    public PlayerManager PlayerManager;
    float speed;
    float input;
   
    public Animator animator;

    public Transform frontTires,backTires;
    void Update()
    {
        gameOver = LevelManager.gameOver;

        if(!gameOver)
        {
            speed = PlayerManager.curSpeed;
            input = PlayerManager.SteerInput;

            //Deternime rotationspeed 
            Vector3 wheelSpeed = (Vector3.right * speed / 10) / 6;
            //Rotate wheels forwards
            backTires.transform.Rotate(wheelSpeed);
            //frontTires.transform.Rotate(wheelSpeed);
            int animInput = Mathf.FloorToInt(input * 10);
            animator.SetInteger("Input", animInput);
        }

    }
}
