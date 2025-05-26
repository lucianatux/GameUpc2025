using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public GameObject impactEffect;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        { 
            Destroy(gameObject); // Destruir la bola de fuego
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}