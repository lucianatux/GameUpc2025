using UnityEngine;

public class FireballAbility : ProjectileAbility
{
    public override void UseAbility(Transform origin, Vector2 direction)      // This method is called to use the fireball ability.
                                                                             // It takes the origin point (e.g., player's fire point) and the direction to shoot in.
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);           // Create a rotation quaternion for the fireball to face the direction of travel. 
        GameObject projectile = Instantiate(projectilePrefab, origin.position, rotation);  // Instantiate the fireball projectile prefab at the origin position with the calculated rotation.
        
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        Collider2D playerCollider = origin.GetComponent<Collider2D>();
        
        if (projectileCollider != null && playerCollider != null)  // If both colliders exist, prevent the projectile from colliding with the player.
        {
            Physics2D.IgnoreCollision(projectileCollider, playerCollider);
        }
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();       // Give the projectile a velocity so it moves in the intended direction.
        rb.velocity = direction.normalized * projectileSpeed;
    }
}