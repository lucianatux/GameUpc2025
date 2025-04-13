using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace StatePattern
{
public class AttackState : IEnemyState
{
    private EnemyAI enemyAI;
    private float attackRange;
    private float attackCooldown;
    private Transform playerTransform;
    private Transform weaponTransform;
    private GameObject bulletPrefab;
    private GameObject warningPrefab;
    private float attackTimer;


        public AttackState(float _attackTimer, float _attackCooldown, GameObject _warningPrefab,float _attackRange, GameObject _bulletPrefab, Transform _weaponTransform, Transform _playerTransform)
        {
            attackCooldown = _attackCooldown;
            attackRange = _attackRange;
            bulletPrefab = _bulletPrefab;
            weaponTransform = _weaponTransform;
            playerTransform = _playerTransform;
            warningPrefab = _warningPrefab;
            attackTimer = _attackTimer;
        }

    public void EnterState(EnemyAI _enemyAI)
    {   
        
        Debug.Log("Cambia a estado Attack");
        enemyAI = _enemyAI;
    }

    public void UpdateState()
    {
        attackTimer -= Time.deltaTime;
        float distToPlayer = enemyAI.GetDistanceToPlayer();
        if (distToPlayer <= attackRange)
        {
            Attack();
            if (!isAttacking)
            {
                Retreat(playerTransform);
            }
        }
        else if (distToPlayer > attackRange && !isAttacking)
        {
            enemyAI.SetState(enemyAI.enemyChaseState);
        }
    }

    bool isAttacking = false;
private void Attack()  
{   
    enemyAI.attackTimer -= Time.deltaTime;

    if (enemyAI.attackTimer < 0)
    {
        
       // Mostrar la advertencia antes de atacar
        enemyAI.attackTimer = attackCooldown;
        Warning();
        enemyAI.StartCoroutine(CheckAttacking()); // Esperar antes de disparar
        Debug.Log("Malo prepara ataque");
    }
}

private IEnumerator CheckAttacking()
{
    isAttacking = true; // Activa el estado de ataque
    yield return new WaitForSeconds(.3f); // Espera antes de disparar
    Shoot(bulletPrefab, playerTransform);
    yield return new WaitForSeconds(1f); // Espera antes de volver a moverse
    isAttacking = false; // Termina el ataque
    Debug.Log("Malo termina ataque");
}
    public void Shoot(GameObject bullet, Transform enemy)
{
    // Calcula la dirección al enemigo
    Vector2 direction = (enemy.position - enemyAI.transform.position).normalized;
    float angle = enemyAI.GetAngleToPlayer();
    GameObject NewBullet = Object.Instantiate(bullet, weaponTransform.position, Quaternion.Euler(0, 0, angle));
    GameObject.Destroy (NewBullet, 2);
}
    private void Warning()
    {
    GameObject warning = Object.Instantiate(warningPrefab, enemyAI.transform.position,  Quaternion.Euler(0, 0, 0));
    GameObject.Destroy (warning, 2);

    }

    public void Retreat(Transform enemy)
    {
        
        enemyAI.MoveTowards(enemy.position, -.8f);
    }

}
}