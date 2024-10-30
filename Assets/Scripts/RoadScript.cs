using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public GameObject LightPosts;
    public Transform[] LightPositions;
    public Transform DestructionPoint;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        DestructionPoint = GameObject.Find("DestructionPoint").transform;
        //Generate lights on road
        for (int i = 0; i < LightPositions.Length; i++) 
        {
            Instantiate(LightPosts, LightPositions[i]);
        }
        
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
}
