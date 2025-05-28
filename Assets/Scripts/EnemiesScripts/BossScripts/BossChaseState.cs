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
    public BossChaseState (Transform _playerTransform)
    {
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
            Debug.LogError(this + "Error de casteo fallido");
        }
    }

    public void UpdateState()
    {
        isInAttackSight = bossAI.GetPlayerInSight(); // Chequea si esta frente al player
        Debug.Log(isInAttackSight);
        bossAI.ChaseHorizontally(playerTransform); // Persecución al player

        Debug.Log("chase update");
        //se fija si tiene al player y si puede atacar
        if (isInAttackSight && bossAI.attackTimer < 0f)
        {
            Debug.Log("se manda al attackstate");
            bossAI.SetState(bossAI.bossAttackState);
        }
    }
}
