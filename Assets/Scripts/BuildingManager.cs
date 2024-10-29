using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    float speed;
    public GameObject[] deco;
    public Transform[] decoSlot;
    Transform DestructionPoint;
    // Start is called before the first frame update
    void Start()
    {
        DestructionPoint = GameObject.Find("DestructionPoint").transform;
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
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < DestructionPoint.position.z)
        {
            Destroy(gameObject);
        }
    }

}
