using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalizedHitImpact : MonoBehaviour
{

    [SerializeField] GameObject impactHit;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No rb component found");
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PlayerProjectile"))
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);
            Instantiate(impactHit, hitPoint, Quaternion.identity);
        }
    }

}
