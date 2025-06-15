using UnityEngine;

public class MeleeAbility : MonoBehaviour, IAttackAbility
{
    public float attackRadius = 1f;
    public int damage = 1;
    public LayerMask enemyLayer;
    public Transform attackPoint; // Se puede usar firePoint directamente también

    [SerializeField] private GameObject meleePrefab;
    public void UseAbility(Transform origin, Vector2 direction)
    {
        // Determinar el punto de ataque en dirección al enemigo
        //Vector3 attackPosition = origin.position + (Vector3)(direction.normalized * attackRadius * 0.5f);

        // Detectar enemigos en el área
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRadius, enemyLayer);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle );           // Create a rotation quaternion for the fireball to face the direction of travel. 
        Instantiate(meleePrefab, origin.position, rotation);  // Instantiate the fireball projectile prefab at the origin position with the calculated rotation.


       // foreach (Collider2D enemy in hitEnemies)
       // {
       //     enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
       // }

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