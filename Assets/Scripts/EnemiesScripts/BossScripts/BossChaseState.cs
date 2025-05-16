using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : IBossState
{
    private BossAI bossAI;

    private int attackTimer;

    private bool isInAttackSight;
    private GameObject bulletPrefab;

    private int attackRange;
    public BossChaseState(bool _isInAttackSight)
    {
        isInAttackSight = _isInAttackSight;

    }
    public void EnterState(BossAI _bossAI)
    {
        bossAI = _bossAI;
    }

    public void UpdateState()
    {
        isInAttackSight = bossAI.GetPlayerInSight();
        if (isInAttackSight)
        {
            bossAI.SetState(bossAI.bossAttackState);   
        }
    }

}
