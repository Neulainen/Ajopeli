using UnityEngine;

public class RoadScript : MonoBehaviour
{
    //var from other scripts
    float speed;
    bool gameOver;

    //Where to generate lightposts, and lightposts used in this level
    public GameObject LightPosts;
    public Transform[] LightPositions;

    //Destruction point determines when to terminate object
    public Transform DestructionPoint;

    // Start is called before the first frame update
    void Start()
    {
        //find destruction point location
        DestructionPoint = GameObject.Find("DestructionPoint").transform;

        //Generate lighposts on road
        for (int i = 0; i < LightPositions.Length; i++)
        {
            Instantiate(LightPosts, LightPositions[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {

        gameOver = GameObject.Find("LevelScripts").GetComponent<LevelManager>().gameOver;

        if (!gameOver)
        {
            //Move towards player with speed determined by player manager
            speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
            transform.Translate(speed * Time.deltaTime * Vector3.back);

            //if we pass the destruction point, object gets removed
            if (transform.position.z < DestructionPoint.position.z)
            {
                Destroy(gameObject);
            }

        }
    }
}
