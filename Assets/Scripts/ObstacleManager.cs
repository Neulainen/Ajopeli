using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    Transform DestructionPoint;
    float speed;
    public Transform[] ObstaclePoints;
    public GameObject[] Obstacles;
    public GameObject[] Rewards;
    // Start is called before the first frame update
    void Start()
    {
        DestructionPoint = GameObject.Find("DestructionPoint").transform;
        AttemptGeneration(IsLucky());

    }

    // Update is called once per frame
    void Update()
    {
        speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < DestructionPoint.position.z)
        {
            Destroy(gameObject);
        }
    }
    bool IsLucky()
    {
        int LuckyNum=Random.Range(0, 10);
        if (LuckyNum == 1 || LuckyNum ==10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void AttemptGeneration(bool isLucky)
    {
        int SafePoint = Random.Range(0, ObstaclePoints.Length);
        Debug.Log(SafePoint);
        for(int i = 0; i < ObstaclePoints.Length; i++)
        {
            if (i != SafePoint&&!isLucky)
            {
                Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], ObstaclePoints[i]);
            }
            else if (i == SafePoint && isLucky)
            {
                Instantiate(Rewards[Random.Range(0, Rewards.Length)], ObstaclePoints[i]);
            }
        }

    }
}
