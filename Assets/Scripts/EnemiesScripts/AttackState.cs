using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace StatePattern
{
    public class AttackState : IEnemyState
    {
        private EnemyAI enemyAI;

        // Configuration
        private float attackRange;
        private float attackCooldown;

        // References
        private Transform playerTransform;
        private Transform weaponTransform;
        private GameObject bulletPrefab;

        // State flag
        public bool isAttacking = false;

        /// <summary>
        /// Receives values and references.
        /// </summary>
        public AttackState(
            float _attackCooldown,
            float _attackRange,
            GameObject _bulletPrefab,
            Transform _weaponTransform,
            Transform _playerTransform)
        {
            attackCooldown = _attackCooldown;
            attackRange = _attackRange;
            bulletPrefab = _bulletPrefab;
            weaponTransform = _weaponTransform;
            playerTransform = _playerTransform;
        }

        /// <summary>
        /// Called once when entering the Attack state.
        /// </summary>
        public void EnterState(EnemyAI _enemyAI)
        {
            if (_enemyAI == null)
            {
                Debug.LogError("AttackState: enemyAI is null when entering state.");
                return;
            }
                
            enemyAI = _enemyAI;
            enemyAI.animator.SetBool("isWalking", false);

            enemyAI.animator.SetTrigger("idle");

            //Debug.Log("Switched to Attack state");
        }

        /// <summary>
        /// Called every frame while in the Attack state.
        /// </summary>
        public void UpdateState()
        {
            if (enemyAI == null || playerTransform == null)
            {
                Debug.LogError("AttackState: enemyAI or playerTransform is null in UpdateState.");
                return;
            }

            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer <= attackRange)
            {
                Attack();
            }

            else
            {
                enemyAI.animator.ResetTrigger("idle");
                enemyAI.animator.SetBool("isWalking", true);

                enemyAI.SetState(enemyAI.enemyChaseState);
            }


        }

        /// <summary>
        /// Initiates an attack if conditions are met (not stunned, cooldown ready).
        /// </summary>
        private void Attack()
        {
            if (enemyAI == null || enemyAI.isStunned || isAttacking) return;

            if (enemyAI.attackTimer <= 0)
            {
                enemyAI.StartCoroutine(CheckAttacking());
                isAttacking = true;
                Debug.Log("Enemy prepares attack");
            }
        }

        /// <summary>
        /// Coroutine: plays a warning, waits, and then shoots.
        /// </summary>
        private IEnumerator CheckAttacking()
        {
            enemyAI.isStunned = true;
            enemyAI.animator.ResetTrigger("idle");
            enemyAI.ShowAlert("Warning");

            yield return new WaitForSeconds(0.2f);
            if (enemyAI.IsDead) yield break;
            enemyAI.animator.SetTrigger("attack");
            if (enemyAI.IsDead) yield break;
 
            enemyAI.isStunned = false;

            yield return new WaitForSeconds(.9f); // Delay before shooting
            if (enemyAI.IsDead) yield break;

            enemyAI.animator.SetTrigger("idle");

            isAttacking = false;
            enemyAI.attackTimer = attackCooldown;

            Debug.Log("Enemy finishes attack");
        }

        /// <summary>
        /// Spawns and launches a bullet toward the player.
        /// </summary>
        public void Shoot()
        {
            if (weaponTransform == null)
            {
                Debug.LogError("AttackState: weaponTransform is null in Shoot().");
                return;
            }
            GameObject bullet = bulletPrefab;
            if (bullet == null)
            {
                Debug.LogError("AttackState: bulletPrefab is null in Shoot().");
                return;
            }
            float angle = enemyAI.GetAngleToPlayer();

            GameObject newBullet = Object.Instantiate(bullet, enemyAI.transform.position, Quaternion.Euler(0, 0, angle));
            Object.Destroy(newBullet, 0.2f);
            Debug.Log("Shot fired with angle: " + angle);
        }
    }
}