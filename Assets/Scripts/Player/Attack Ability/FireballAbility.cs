using UnityEngine;

public class FireballAbility : ProjectileAbility
{
    [Header("Cooldown")]
    public float cooldownDuration = 1f;
    private float cooldownTimer = 0f;

    public bool IsReady => cooldownTimer <= 0f;

    void Update()
    {

    }

    public override void UseAbility(Transform origin, Vector2 direction)
    {

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);
        GameObject projectile = Instantiate(projectilePrefab, origin.position, rotation);

        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        Collider2D playerCollider = origin.GetComponent<Collider2D>();

        /*
        if (projectileCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(projectileCollider, playerCollider);
        }
        */

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;

        cooldownTimer = cooldownDuration;
    }

    //  Para usar desde animaciones, UnityEvents, etc.
    public void ResetCooldown()
    {
        cooldownTimer = 0f;
    }
}