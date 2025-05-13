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
        float distToPlayer = enemyAI.GetDistanceToPlayer();
        enemyAI.attackTimer -= Time.deltaTime;
        if (distToPlayer <= attackRange)
        {
            Attack();

        }
        else if (distToPlayer > attackRange && !isAttacking)
        {
            enemyAI.SetState(enemyAI.enemyChaseState);
        }

    }

    bool isAttacking = false;
    private void Attack()  
    {   
        if (!playerTransform) return;   

        if (enemyAI.attackTimer <= 0)
        {
            if (enemyAI.isStunned) return;

            enemyAI.attackTimer = attackCooldown;
            enemyAI.StartCoroutine(CheckAttacking()); // Esperar antes de disparar
            Debug.Log("Malo prepara ataque");
        }
    }

    private IEnumerator CheckAttacking()
{
    isAttacking = true;
    Warning();
    yield return new WaitForSeconds(0.2f);

    //enemyAI.animController.Play(AnimName.AttackAnim, 2, true);
    yield return new WaitForSeconds(1f); // Espera antes de desbloquear
    //enemyAI.animController.Unlock();       // 🔓 desbloquea justo antes de cambiar de animación
    Shoot(bulletPrefab, weaponTransform);
    //enemyAI.animController.Play(AnimName.IdleAnim, 1, false); // Ahora sí cambia a idle

    yield return new WaitForSeconds(1.3f);
    //enemyAI.animController.Play(AnimName.IdleAnim, 1, false);

    isAttacking = false;
    Debug.Log("Malo termina ataque");
}


        public void Shoot(GameObject bullet, Transform enemy)
    {
        // Calcula la dirección al enemigo
        if(weaponTransform == null || bulletPrefab == null) return;
        Vector2 direction = (enemy.position - enemyAI.transform.position).normalized;
        float angle = enemyAI.GetAngleToPlayer();
        GameObject NewBullet = Object.Instantiate(bullet, weaponTransform.position, Quaternion.Euler(0, 0, angle));
        GameObject.Destroy (NewBullet, 0.2f);
        Debug.Log(angle);
    }
        private void Warning()
        {
        if (warningPrefab == null) return;
        GameObject warning = Object.Instantiate(warningPrefab, enemyAI.transform.position,  Quaternion.Euler(0, 0, 0));
        GameObject.Destroy (warning, 2);

        }

    private IEnumerator UnlockAfter(float seconds)
    {
        Debug.Log("empieza desbloqueo");
        yield return new WaitForSeconds(seconds);
        Debug.Log("termina desbloqueo");
        //enemyAI.animController.Unlock();
    }
    }
    }
