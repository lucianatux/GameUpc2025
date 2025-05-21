using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : IEnemyState
{
    private BossAI bossAI;

    private int attackTimer;

    private bool isInAttackSight;
    private GameObject bulletPrefab;
    private Transform playerTransform;
    private int attackRange;
    public BossChaseState(bool _isInAttackSight, Transform _playerTransform)
    {
        isInAttackSight = _isInAttackSight;
        playerTransform = _playerTransform;

    }

    public void EnterState(EnemyAI _enemyAI)
    {
        Debug.Log("se entra al chase state");

        if (_enemyAI is BossAI enemy)
        {
            bossAI = enemy;
        }
        else
        {
            Debug.LogError("");
        }
    }

    public void UpdateState()
    {
        isInAttackSight = bossAI.GetPlayerInSight();
        bossAI.ChaseHorizontally(playerTransform);
        Debug.Log("chase update");
        if (isInAttackSight && attackTimer < 0)
        {
            // bossAI.SetState(bossAI.bossAttackState);
        }
    }
}
