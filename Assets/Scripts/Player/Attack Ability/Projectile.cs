using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour   // This script handles the behavior of a projectile (e.g., a fireball) once it's been launched.
{
    public float lifetime = 3f;
    public GameObject impactEffect;
    public float projectileSpeed = 5f;
    public int damage = 5;

    private Rigidbody2D _rb;
    [SerializeField] bool _canHitWalls;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null) Debug.LogError("Projectile attached to " + gameObject.name + " has no Rigidbody");

        _rb.velocity = _rb.velocity * projectileSpeed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)   // Called when the projectile enters a trigger collider.
    {
        if (collision.CompareTag("Room")) return;
            Vector2 hitPoint = collision.ClosestPoint(transform.position);

        if (collision.CompareTag("Enemy"))   // If the projectile hits an object tagged "Enemy", destroy the projectile.
        {
            Destroy(gameObject);

            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null) enemyHealth.TakeDamage(damage);
            else Debug.LogError("enemy health not found");

                // Instancia el efecto en ese punto            
            if (impactEffect != null)   // If there's an impact effect assigned, spawn it at the projectile's current position.
            {
                Instantiate(impactEffect, hitPoint, Quaternion.identity);
            }

        }

        if (impactEffect != null && _canHitWalls)   // If there's an impact effect assigned, spawn it at the projectile's current position.
        {
            Instantiate(impactEffect, hitPoint, Quaternion.identity);
        }

        Destroy(gameObject);   // Destroy the projectile after triggering the effect or hitting something.

        Debug.Log("Choca con " + collision.gameObject.name);
    }
    

    
}