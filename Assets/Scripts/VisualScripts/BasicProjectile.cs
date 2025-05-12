using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int damage;
    [SerializeField] private bool isEnemy;
        public GameObject hitImpactPrefab; // Prefab del efecto de impacto

    private void OnTriggerEnter2D(Collider2D other)
{   
    Debug.Log("Colisión detectada con: " + other.name);

    
    EnemyHealth enemyH = other.GetComponent<EnemyHealth>();

    if (enemyH != null)
    {
    {         
        Vector2 hitPoint = other.ClosestPoint(transform.position);

        if (isEnemy) return;
        if (hitImpactPrefab != null) 
    {
        GameObject effect = Instantiate(hitImpactPrefab, hitPoint, Quaternion.identity);
        Debug.Log("Encontró al player, hace daño");
        enemyH.TakeDamage(damage);
        Destroy(effect, 0.3f);

    }
    }
}
    
    PlayerHealth playerH = other.GetComponent<PlayerHealth>();

    if (playerH != null)
    {
    {         
        Vector2 hitPoint = other.ClosestPoint(transform.position);

        if (!isEnemy) return;
        if (hitImpactPrefab != null) 
    {
        GameObject effect = Instantiate(hitImpactPrefab, hitPoint, Quaternion.identity);
        Debug.Log("Encontró al player, hace daño");
        playerH.TakeDamage(damage   );
        Destroy(effect, 0.3f);

    }
    }
}
}
}


