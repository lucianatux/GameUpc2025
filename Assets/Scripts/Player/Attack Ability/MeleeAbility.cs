using UnityEngine;

public class MeleeAbility : MonoBehaviour, IAttackAbility
{
    public float attackRadius = 1f;
    public int damage = 1;
    public LayerMask enemyLayer;
    public Transform attackPoint; // Se puede usar firePoint directamente también

    public void UseAbility(Transform origin, Vector2 direction)
    {
        // Determinar el punto de ataque en dirección al enemigo
        Vector3 attackPosition = origin.position + (Vector3)(direction.normalized * attackRadius * 0.5f);

        // Detectar enemigos en el área
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }

        // Efectos opcionales: sonido, animación, partículas
    }

    // Mostrar el área de ataque en el editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}