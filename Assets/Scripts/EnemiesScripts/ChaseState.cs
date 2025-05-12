using UnityEngine;
using UnityEngine.AI;
using System.Collections;
namespace StatePattern
{
public class ChaseState : IEnemyState
{   
    private float followRange;
    private float attackRange;
    private Transform playerTransform;
    private EnemyAI enemyAI;
    private GameObject attentionPrefab;

    private NavMeshAgent sagent;

    public ChaseState(float _followRange, float _attackRange, Transform _playerTransform, GameObject _attentionPrefab, NavMeshAgent _agent)
    {
        followRange = _followRange;
        attackRange = _attackRange;
        playerTransform = _playerTransform;
        attentionPrefab = _attentionPrefab;
        sagent = _agent;
    }


    public void EnterState(EnemyAI _enemyAI)
    {
        Debug.Log("Cambia a estado chase");
        enemyAI = _enemyAI;
    }

    public void UpdateState()
    {
        enemyAI.attackTimer -= Time.deltaTime;
        enemyAI.attentionTimer -= Time.deltaTime;
        float distToPlayer = enemyAI.GetDistanceToPlayer();
        if(distToPlayer > attackRange)
        {
            if (enemyAI.isStunned) return;

            enemyAI.Chase(playerTransform);
        }
        else if (distToPlayer <= attackRange)
        {
            
            enemyAI.SetState(enemyAI.enemyAttackState);
        }

    }
        

}
}