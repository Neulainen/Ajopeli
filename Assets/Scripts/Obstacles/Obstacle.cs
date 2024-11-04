using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy object if hit by player
        if (collision.gameObject.CompareTag("Player"))
        {
           GetComponent<ParticleSystem>().Play();
           Destroy(gameObject);
        }
    }
}
