using UnityEngine;

public class TireAnimator : MonoBehaviour
{
    //Other scripts and var
    public LevelManager LevelManager;
    bool gameOver;

    public PlayerManager PlayerManager;
    float speed;
    float input;
   
    //Animator used for front tires
    public Animator animator;

    //Transforms of tires
    public Transform frontTires,backTires;
    void Update()
    {
        gameOver = LevelManager.gameOver;

        if(!gameOver)
        {
            speed = PlayerManager.curSpeed;
            input = PlayerManager.SteerInput;

            //Deternime rotationspeed, formula is arbitrary but looks correct enough
            Vector3 wheelSpeed = (Vector3.right * speed / 10) / 6;

            //Rotate tires forwards. Can't be applied to front tires
            backTires.transform.Rotate(wheelSpeed);

            //Sends in player input as integrer. Animator checks if input is greater,
            //less or equal to 0 and activates correct animation
            int animInput = Mathf.FloorToInt(input * 10);
            animator.SetInteger("Input", animInput);
        }

    }
}
