using UnityEngine;
namespace StatePattern
{
public class ChaseState : IEnemyState
{   
    private float followRange;
    private float attackRange;
    private Transform playerTransform;
    private EnemyAI enemyAI;
    private GameObject attentionPrefab;

    public ChaseState(float _followRange, float _attackRange, Transform _playerTransform, GameObject _attentionPrefab)
    {
        followRange = _followRange;
        attackRange = _attackRange;
        playerTransform = _playerTransform;
        attentionPrefab = _attentionPrefab;
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
        if(distToPlayer < followRange && distToPlayer > attackRange)
        {
            Chase();
        }
        else if (distToPlayer > followRange)
        {
            enemyAI.SetState(enemyAI.enemyWanderState);
        }
        else if (distToPlayer <= attackRange)
        {
            enemyAI.SetState(enemyAI.enemyAttackState);
        }
    }
    
    private void Chase()
    {
        enemyAI.MoveTowards(playerTransform.position);
        //enemyAI.LookAt(playerTransform.position);

    }
    

}
}