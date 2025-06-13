using UnityEngine;
namespace StatePattern
{
    public class ChaseState : IEnemyState
    {
        // === Enemy Chase Settings ===
        private float attackRange;                      // Distance to switch to AttackState
        private Transform playerTransform;              // Reference to the player
        private EnemyAI enemyAI;                        // Reference to the enemy AI script

        /// <summary>
        /// Constructor: sets chase settings like player target and range.
        /// </summary>
        public ChaseState(float _attackRange, Transform _playerTransform)
        {
            attackRange = _attackRange;
            playerTransform = _playerTransform;
        }

        /// <summary>
        /// Called when the enemy enters the Chase state.
        /// </summary>
        public void EnterState(EnemyAI _enemyAI)
        {
            if (_enemyAI == null)
            {
                Debug.LogError("ChaseState: enemyAI is null in EnterState.");
                return;
            }

            enemyAI = _enemyAI;
            enemyAI.animator.SetBool("isWalking", true);

            Debug.Log("Enemy switched to Chase state.");
        }

        /// <summary>
        /// Chases the player unless they're in attack range.
        /// </summary>
        public void UpdateState()
        {
            if (enemyAI == null)
            {
                Debug.LogWarning("ChaseState: Missing enemy AI.");
                return;
            }

            if (playerTransform == null)
            {
                Debug.LogWarning("ChaseState: Missing player transform.");
                return;
            }

            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer > attackRange)
            {
                enemyAI.Chase(playerTransform); // Keep chasing the player
            }
            else
            {
                enemyAI.animator.ResetTrigger("walk");
                enemyAI.animator.SetBool("walk", false);
                enemyAI.SetState(enemyAI.enemyAttackState); // Switch to attack
            }
        }
    }
}