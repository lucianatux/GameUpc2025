using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : IBossState
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
    public void EnterState(BossAI _bossAI)
    {
        Debug.Log("se entra al chase state");
        bossAI = _bossAI;
    }

    public void UpdateBossState()
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
