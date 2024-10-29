using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    float speed;
    public GameObject[] deco;
    public Transform[] decoSlot;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < decoSlot.Length; i++) 
        {
            Instantiate(deco[Random.Range(0, deco.Length)], decoSlot[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
        Movement();
    }
    void Movement()
    {
        transform.Translate(Vector3.back*speed*Time.deltaTime);
    }
}
