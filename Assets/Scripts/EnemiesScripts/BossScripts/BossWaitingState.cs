using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitingState : IEnemyState
{
    private BossAI bossAI;

    private int enemyRoomID; 

    private int enemyWaveID;

    public BossWaitingState(int _enemyRoomID, int _enemyWaveID)
    {
        enemyRoomID = _enemyRoomID;
        enemyWaveID = _enemyWaveID;
        
    }

    private void BossWakeUp(int roomID)
    {
        if (enemyRoomID != roomID) return;
        bossAI.isActive = true;
        RoomManager.Instance.OnCallWaves -= BossWakeUp;
        if (bossAI.rb != null)
        {
            bossAI.rb.bodyType = RigidbodyType2D.Kinematic; // para que no lo afecte la física
        }
        bossAI.GetComponent<Collider2D>().enabled = true;
            Debug.Log("se despierta");

        bossAI.SetState(bossAI.bossChaseState);   
    }

   

    public void EnterState(EnemyAI _enemyAI)
    {

        if (_enemyAI is BossAI enemy)
        {
            bossAI = enemy;
        }
        else
        {
            Debug.LogError("");
        }

        Debug.Log("estado waiting");
        RoomManager.Instance.OnRoomEntered += BossWakeUp;

        
        bossAI.isActive = false;
    }

    public void UpdateState()
    {
    }
}
