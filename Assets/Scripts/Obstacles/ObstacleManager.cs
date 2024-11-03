using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    float speed;
    bool endGame;

    //Point to spawn into
    Transform DestructionPoint;
    
    //Where to spawn Obstacles
    public Transform[] ObstaclePoints;
    public GameObject[] Obstacles;

    // Start is called before the first frame update
    void Start()
    {
        DestructionPoint = GameObject.Find("DestructionPoint").transform;
        Generation();
    }

    // Update is called once per frame
    void Update()
    {
        endGame = GameObject.Find("LevelScripts").GetComponent<LevelManager>().gameOver;
        if (!endGame)
        {
            speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
            transform.Translate(speed * Time.deltaTime * Vector3.back);
            if (transform.position.z < DestructionPoint.position.z)
            {
                Destroy(gameObject);
            }
        }
    }
    void Generation()
    {
        int SafePoint1 = Random.Range(0, ObstaclePoints.Length);
        int SafePoint2 = Random.Range(0, ObstaclePoints.Length);
        for (int i=SafePoint1; i==SafePoint2; i++)
        {
            SafePoint2++;
        }
        for (int i = 0; i < ObstaclePoints.Length; i++)
        {
            if (i != SafePoint1 && i !=SafePoint2)
            {
                Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], ObstaclePoints[i]);
            }
        }

    }
}
