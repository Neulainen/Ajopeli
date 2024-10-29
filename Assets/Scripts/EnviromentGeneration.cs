using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentGeneration : MonoBehaviour
{

    public Transform[] BuildingSlots;
    public GameObject[] Buildings;
    // Start is called before the first frame update
    void Start()
    {
        GenerateBuilding();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void GenerateBuilding()
    {
      for (int i = 0; i < BuildingSlots.Length; i++)
        {
            Instantiate(Buildings[Random.Range(0, Buildings.Length)], BuildingSlots[i].transform);
        }

    }

}
