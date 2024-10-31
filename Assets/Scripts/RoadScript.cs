using UnityEngine;

public class RoadScript : MonoBehaviour
{
    float speed;
    bool endGame;
    public GameObject LightPosts;
    public Transform[] LightPositions;
    public Transform DestructionPoint;

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
        endGame = GameObject.Find("=Player=").GetComponent<PlayerManager>().endGame;
        if (!endGame)
        {
            speed = GameObject.Find("=Player=").GetComponent<PlayerManager>().curSpeed;
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (transform.position.z < DestructionPoint.position.z)
            {
                Destroy(gameObject);
            }

        }
    }
}
