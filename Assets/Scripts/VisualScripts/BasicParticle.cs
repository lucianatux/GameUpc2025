using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicParticle : MonoBehaviour
{

public class ParticleCollisionDetector : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField] private int damage;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Partícula chocó con: " + other.name);

        // Ejemplo: hacer daño si colisiona con el player
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
}