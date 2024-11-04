using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    float speed;
    bool gameOver;

    /*
    Used to assing Decoration objects on buildings at random
    */
    public GameObject[] deco;
    public Transform[] decoSlot;

    //Check if script is used on a building with windows
    public bool isBuilding;

    //Used to determine window color at random
    Material material;
    public Color[] WindowColor;

    //Determines when building is destroyed
    Transform DestructionPoint;
    void Start()
    { 
        DestructionPoint = GameObject.Find("DestructionPoint").transform;

        for (int i = 0; i < decoSlot.Length; i++)
        {
            Instantiate(deco[Random.Range(0, deco.Length)], decoSlot[i]);
        }
        if (isBuilding)
        {
            //generate window colours from array
            material = GetComponent<MeshRenderer>().material;
            material.SetColor("_EmissionColor", WindowColor[Random.Range(0,WindowColor.Length)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
        gameOver = GameObject.Find("LevelScripts").GetComponent<LevelManager>().gameOver;
        Movement();
    }
    void Movement()
    {
        if (!gameOver)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.back);
            if (transform.position.z < DestructionPoint.position.z)
            {
                Destroy(gameObject);
            }
        }
    }

}
