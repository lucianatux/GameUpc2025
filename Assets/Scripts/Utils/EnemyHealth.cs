using UnityEngine;
using StatePattern;
/// <summary>
/// Handles the health, damage feedback, and death behavior of enemy characters.
/// Inherits from LifeSystem to manage base health functionality.
/// </summary>
public class EnemyHealth : LifeSystem
{
    // === Drop Settings ===
    [SerializeField] private GameObject lifeOrbPrefab;                      // Prefab to instantiate on death
    [SerializeField, Range(0f, 1f)] private float lifeOrbDropChance = 0.2f; // Chance to drop the orb

    // === Components ===
    [SerializeField] private HealthBarUI healthUI;

    private Rigidbody2D rb;

    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private EnemyAI enemyAI;

    private EnemyAnimatorController enemyAnimController;

    /// <summary>
    /// Initializes necessary components and references.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        enemyAnimController = GetComponent<EnemyAnimatorController>();

        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D not found on Enemy.");

        col = GetComponent<Collider2D>();
        if (col == null) Debug.LogError("Collider component not found on Enemy.");

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer not found on Enemy.");

        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Animator not found on Enemy.");

        enemyAI = GetComponent<EnemyAI>();
        if (enemyAI == null) Debug.LogError("EnemyAI script not found on Enemy.");
        
        if (healthUI != null)
            healthUI.Initialize(maxHealth);

    }

    /// <summary>
    /// Gets called when enemy are taking damage
    /// </summary>
    /// <param name="damage">Amount of damage taken.</param>
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        //Debug.Log("Enemy took damage");

        if (enemyAI != null)
        {
            enemyAI.EnemyTakeDamage(); // Visual/audio damage feedback
        }
        else
        {
            Debug.LogError("enemyAI not found");
        }

        if (animator != null)
        {
            //animator.SetTrigger("damage"); // Damage animation
        }
        
        if (healthUI != null)
            healthUI.SetHealth(currentHealth);

        EnemiesEventsManager.Instance?.EnemyDamaged(); //Notify Enemies Events Manager
    }

    /// <summary>
    /// Triggers death behavior: disables movement, animations, and notifies systems.
    /// </summary>
    protected override void Die()
    {

        Debug.Log("Enemy Dies");

        if (enemyAI != null)
        {
            if (enemyAI.IsDead) return;  // Evitar doble llamada
            enemyAI.Die();

            enemyAI.enabled = false;
            if (col != null) col.enabled = false;
            else Debug.LogError("Collider2D not found on Enemy.");

            if (animator != null)
            {
                Debug.Log("Se usa animacion de muerte");
                animator.SetTrigger("die");
            }

            RoomManager.Instance.NotifyEnemyDeath(enemyAI.RoomID);  // Aquí notificamos solo UNA vez
        }
        else
        {
            Debug.LogError("EnemyAI component missing on Enemy.");
        }

        TrySpawnLifeOrb(); // possibly drops a Life Orb
        EnemiesEventsManager.Instance?.EnemyDefeated();; // Notify Enemies events Manager

        BossDeathHandler bossHandler = GetComponent<BossDeathHandler>();//si es el boss llama a BossDeath Handler
        if (bossHandler != null)
        {
            bossHandler.OnBossDefeated();
        }

    }

    /// <summary>
    /// Tries to instantiate a life orb with a random chance.
    /// In TESTING
    /// </summary>
    private void TrySpawnLifeOrb()
    {
        float rng = Random.value;
        if (rng <= lifeOrbDropChance)
        {
            if (lifeOrbPrefab != null)
            {
                Instantiate(lifeOrbPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Life Orb PRefab not found");
            }
        }
    }
}