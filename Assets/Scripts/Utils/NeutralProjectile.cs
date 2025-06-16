using System;
using Unity.VisualScripting;
using UnityEngine;

public class NeutralProjectile : MonoBehaviour   // This script handles the behavior of a projectile (e.g., a fireball) once it's been launched.
{
    public float lifetime = 3f;
    public GameObject impactEffect;
    public float projectileSpeed = 5f;
    private Rigidbody2D _rb;
    public int damage;
    [SerializeField] bool _canHitWalls;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null) Debug.LogError("Projectile attached to " + gameObject.name + " has no Rigidbody");

        _rb.velocity = _rb.velocity * projectileSpeed;


    }
    private void OnTriggerEnter2D(Collider2D collision)   // Called when the projectile enters a trigger collider.
    {
        Debug.Log("Choca con " + collision.gameObject.name);
        Vector2 hitPoint = collision.ClosestPoint(transform.position);
        
        if (impactEffect != null && _canHitWalls)   // If there's an impact effect assigned, spawn it at the projectile's current position.
        {
            Instantiate(impactEffect, hitPoint, Quaternion.identity);
        }
        if (collision.CompareTag("Room"))
        {
            Debug.Log("geyser choca con room");
            return;
        }
        //if (collision == this) return;
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null) enemyHealth.TakeDamage(damage);
            else Debug.LogError("enemy health not found");

                // Instancia el efecto en ese punto            
            if (impactEffect != null)   // If there's an impact effect assigned, spawn it at the projectile's current position.
            {
                Instantiate(impactEffect, hitPoint, Quaternion.identity);
            }

            Debug.Log("geyser choca con enemy");
            return;
        }

        if (collision.CompareTag("Player"))   // If the projectile hits an object tagged "Enemy", destroy the projectile.
        {

            Debug.Log("geyser choca con player");

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null) playerHealth.TakeDamage(damage);
            else Debug.LogError("player health not found");

            if (impactEffect != null)   // If there's an impact effect assigned, spawn it at the projectile's current position.
            {
                Instantiate(impactEffect, hitPoint, Quaternion.identity);
            }

        }

        if (impactEffect != null)   // If there's an impact effect assigned, spawn it at the projectile's current position.
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(impactEffect, 2f);
        }


        Debug.Log("Choca con " + collision.gameObject.name);
    }


    
}

    