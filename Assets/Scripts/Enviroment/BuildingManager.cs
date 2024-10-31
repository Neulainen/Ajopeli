using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    float speed;
    bool gameOver;
    public GameObject[] deco;
    public Transform[] decoSlot;
    public Color[] WindowColor;
    public bool isBuilding;

    Transform DestructionPoint;
    // Start is called before the first frame update
    void Start()
    {
        DestructionPoint = GameObject.Find("DestructionPoint").transform;
        for (int i = 0; i < decoSlot.Length; i++)
        {
            Instantiate(deco[Random.Range(0, deco.Length)], decoSlot[i]);
        }
        if (isBuilding)
        {
            //Arvo ikkunoille väri
        }


    }

    // Update is called once per frame
    void Update()
    {
        speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
        gameOver = GameObject.Find("=Player=").GetComponent<PlayerManager>().gameOver;
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
