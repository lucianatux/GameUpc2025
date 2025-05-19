using System.Collections;
using System.Collections.Generic;
using StatePattern;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : LifeSystem
{
    [SerializeField] private GameObject lifeOrbPrefab;
    SpriteRenderer spriteRenderer;
    EnemyAI enemyAI;
    //bool isStunned = false;
    Color originalColor;
    Rigidbody2D rb;
    [SerializeField, Range(0f, 1f)] private float lifeOrbDropChance = 0.3f; // 30% por defecto

    //private EnemyAI enemyAI;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        enemyAI = GetComponent<EnemyAI>();

     //   enemyAI = GetComponent<EnemyAI>();
    }

      public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Enemy recibió daño");
        enemyAI.EnemyTakeDamage();
        
        // Se queda quieto por el tiempo de stun
        
      //  enemyAI.StopAttackingTemporarily();

    }
      

    protected override void Die() 
    {
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static; // para que no lo afecte la física
    }
            Debug.Log("se muere " + this + " por el enemy health");
        if (enemyAI != null) enemyAI.enabled = false;
        animController.Play(AnimName.DieAnim, 10, true);
        RoomManager.Instance.NotifyEnemyDeath(); // le avisás al RoomManager
        Debug.Log("se le avisa al RoomManager de la muerte de " + this);
        GetComponent<Collider2D>().enabled = false;
         if (rb != null)
        base.Die();



        //GetComponent<EnemyAI>()?.enabled = false;
        TrySpawnLifeOrb();
    //    enemyAI.enabled = false;
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
