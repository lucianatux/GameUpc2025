using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitingState : IBossState
{
    private BossAI bossAI;

    private int enemyRoomID; 

    private int enemyWaveID;

    public BossWaitingState(int _enemyRoomID, int _enemyWaveID)
    {
        enemyRoomID = _enemyRoomID;
        enemyWaveID = _enemyWaveID;
        
    }

    public void EnterState(BossAI _bossAI)
    {
        Debug.Log("estado waiting");
        RoomManager.Instance.OnRoomEntered += BossWakeUp;
        bossAI = _bossAI;
        bossAI.isActive = false;

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
        bossAI.SetState(bossAI.bossChaseState);   
    }

    
    public void UpdateState()
    {

    }
}
