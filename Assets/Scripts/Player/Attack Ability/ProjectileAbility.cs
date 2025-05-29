using UnityEngine;

public abstract class ProjectileAbility : MonoBehaviour, IAttackAbility           // This abstract class defines the basic behavior of any projectile-based ability.
                                                                                 // It implements the IAttackAbility interface, meaning it can be used as an attack.
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;

    public virtual void UseAbility(Transform origin, Vector2 direction)    // This is the method that triggers the ability.
                                                                          // It can be overridden by subclasses for more specific behavior.
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;   // Calculate the angle (in degrees) of the direction vector for rotation.
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject projectile = Instantiate(projectilePrefab, origin.position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;   // Set the projectile’s velocity in the given direction, scaled by the speed.
    }
}