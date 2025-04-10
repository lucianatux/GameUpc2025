using Unity.VisualScripting;
using UnityEngine;
namespace StatePattern
{
public class WanderState : IEnemyState
{
    private EnemyAI enemyAI;
<<<<<<< Updated upstream
    private float wanderTimeMin = 2;
    private float wanderTimeMax = 5;
    private float wanderDistance = 4;
=======

    private float wanderTimeMin = 2;

    private float wanderTimeMax = 5;

    private float wanderDistance = 4;
    
>>>>>>> Stashed changes
    private float followRange;

    public WanderState(float _followRange)
    {
        followRange = _followRange;
    }
    public void EnterState(EnemyAI _enemyAI)
    {
        Debug.Log("Cambia a estado wander");

        enemyAI = _enemyAI;
    }

    public void UpdateState()
    {
        float distToPlayer = enemyAI.GetDistanceToPlayer();
//        Debug.Log(enemyAI.GetAngleToPlayer());
        if (distToPlayer > followRange)
        {
            Wander();
        }
        else 
        {
            enemyAI.Attention();
            enemyAI.SetState(enemyAI.enemyChaseState);
        }
    }

    float wanderTimer = 0;
    Vector2 randomPoint;

private void Wander()
{
    wanderTimer -= Time.deltaTime;

    if (wanderTimer <= 0)
    {
        GetNewWanderPosition();
    }

    enemyAI.MoveTowards(randomPoint, 1);
}

private void GetNewWanderPosition()
{
    wanderTimer = Random.Range(wanderTimeMin, wanderTimeMax);
    randomPoint = Random.insideUnitCircle * wanderDistance + (Vector2)enemyAI.transform.position;
}
        private bool IsPointValid(Vector2 point)
        {
            Debug.Log("calcula punto");
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.up, 1f, enemyAI.walkableLayer);
            return hit.collider != null;
        }
    }
}
