using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailPoint : MonoBehaviour
{
    float speed;
    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameOver = GameObject.Find("=Player=").GetComponent<PlayerManager>().gameOver;
        if (!gameOver)
        {
            speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
            transform.Translate(Vector3.back * speed * Time.deltaTime/8);
        }
           
    }
}
