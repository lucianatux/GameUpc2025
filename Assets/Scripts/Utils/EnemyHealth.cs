using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : LifeSystem
{
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
        ChangeAnimationState(AnimName.DamageAnim);

      //  enemyAI.StopAttackingTemporarily();

    }
    protected override void Die() 
    {
        base.Die();
        GetComponent<Collider2D>().enabled = false;
        //GetComponent<EnemyAI>()?.enabled = false;
        TrySpawnLifeOrb();
        ChangeAnimationState(AnimName.DieAnim);
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
