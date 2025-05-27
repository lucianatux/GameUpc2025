using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public GameObject impactEffect;
    public float projectileSpeed = 5f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null) Debug.LogError("Projectile attached to " + gameObject.name + " has no Rigidbody");
        
        _rb.velocity = _rb.velocity * projectileSpeed;
        
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
        
        Debug.Log ("Choca con " + collision.gameObject.name);
    }
}