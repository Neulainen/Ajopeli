using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           GetComponent<ParticleSystem>().Play();
           Destroy(gameObject);
        }
    }
}
