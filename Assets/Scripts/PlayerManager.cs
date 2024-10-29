using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider mainColl;

    public float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainColl = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
