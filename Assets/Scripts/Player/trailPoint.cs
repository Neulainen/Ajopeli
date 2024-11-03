using UnityEngine;

public class trailPoint : MonoBehaviour
{   
    //Used for simulating forward movement on particle effects

    public LevelManager levelManager;
    bool gameOver;
    public PlayerManager playerManager;
    float speed;

    public int Modifier;
   

    // Update is called once per frame
    void Update()
    {
        gameOver = levelManager.gameOver;
        if (!gameOver)
        {
            speed = playerManager.curSpeed;
            transform.Translate(speed * Time.deltaTime * Vector3.back / Modifier);
        }
           
    }
}
