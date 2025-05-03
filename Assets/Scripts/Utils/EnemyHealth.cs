using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : LifeSystem
{
    private Animator animator;
    [SerializeField] private GameObject lifeOrbPrefab;
    [SerializeField, Range(0f, 1f)] private float lifeOrbDropChance = 0.3f; // 30% por defecto

    //private EnemyAI enemyAI;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
     //   enemyAI = GetComponent<EnemyAI>();
    }

      public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
      //  enemyAI.StopAttackingTemporarily();

    }
    protected override void Die() 
    {
        base.Die();
        GetComponent<Collider2D>().enabled = false;
        //GetComponent<EnemyAI>()?.enabled = false;
        animator.SetTrigger("Die");
    //    enemyAI.enabled = false;
    }

    private void TrySpawnLifeOrb()
{
    float rng = Random.value; // entre 0 y 1 un rango aleatorio
    if (rng <= lifeOrbDropChance && lifeOrbPrefab != null)
    {
        Instantiate(lifeOrbPrefab, transform.position, Quaternion.identity);
    }
}

}
