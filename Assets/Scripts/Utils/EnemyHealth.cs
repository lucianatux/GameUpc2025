using System.Collections;
using System.Collections.Generic;
using StatePattern;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : LifeSystem
{
    [SerializeField] private GameObject lifeOrbPrefab;
    [SerializeField, Range(0f, 1f)] private float lifeOrbDropChance = 0.3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private EnemyAI enemyAI;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Enemy recibió daño");

        enemyAI.EnemyTakeDamage(); // aplica el color rojo, etc.
        animator.SetTrigger("damage");
    }

    protected override void Die()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static; // para que no lo afecte la física

        if (enemyAI != null) enemyAI.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        animator.SetTrigger("die");

        base.Die();

        RoomManager.Instance.NotifyEnemyDeath();// le avisás al RoomManager
        TrySpawnLifeOrb();
        EnemiesEventsManager.Instance?.EnemyDefeated();
    }

    private void TrySpawnLifeOrb() //funcion que intenta spawnear un orbe de vida, usando la probabilidad 
    {
        float rng = Random.value; // entre 0 y 1 un rango aleatorio
        if (rng <= lifeOrbDropChance && lifeOrbPrefab != null)
        {
            Instantiate(lifeOrbPrefab, transform.position, Quaternion.identity);
        }
    }
}
