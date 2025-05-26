using UnityEngine;

public abstract class ProjectileAbility : MonoBehaviour, IAttackAbility
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;

    public virtual void UseAbility(Transform origin, Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject projectile = Instantiate(projectilePrefab, origin.position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;
    }
}