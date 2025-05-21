using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : IBossState
{
    private BossAI bossAI;

    private float attackTimer;
    private GameObject bulletPrefab;
    private float attackRange;
    public BossAttackState(float _attackTimer, GameObject _bulletPrefab, float _attackRange)
    {
        attackTimer = _attackTimer;
        attackRange = _attackRange;
        bulletPrefab = _bulletPrefab;
    }


    public void EnterState(BossAI _bossAI)
    {
        bossAI = _bossAI;
    }

    public void UpdateBossState()
    {

    }
}

