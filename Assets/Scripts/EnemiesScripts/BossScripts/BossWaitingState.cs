using StatePattern;
using UnityEngine;

/// <summary>
/// Waiting state for the boss. It stays inactive until its assigned room is entered,
/// then becomes active and transitions to the chase state.
/// </summary>
public class BossWaitingState : IEnemyState
{
    private BossAI bossAI;

    private int enemyRoomID;
    private int enemyWaveID;

    /// <summary>
    /// Constructor to assign the boss's associated room and wave.
    /// </summary>
    public BossWaitingState(int _enemyRoomID, int _enemyWaveID)
    {
        enemyRoomID = _enemyRoomID;
        enemyWaveID = _enemyWaveID;
    }

    /// <summary>
    /// Called when this state is entered. Subscribes to room entry events.
    /// </summary>
    public void EnterState(EnemyAI _enemyAI)
    {
        if (_enemyAI is BossAI enemy)
        {
            bossAI = enemy;
        }
        else
        {
            Debug.LogError(this + " - Cast to BossAI failed.");
            return;
        }

        Debug.Log("Entered Waiting State");

        // Subscribe to the room entry event to wake up the boss
        RoomManager.Instance.OnRoomEntered += BossWakeUp;

        // Set the boss inactive until its room is triggered
        bossAI.isActive = false;
    }

    /// <summary>
    /// Triggered when a room is entered. If it's the boss's room, it wakes up and transitions to chase.
    /// </summary>
    private void BossWakeUp(int roomID)
    {
        if (enemyRoomID != roomID) return;

        bossAI.isActive = true;
        RoomManager.Instance.OnCallWaves -= BossWakeUp;

        //Initializes RigidBody and Collider
        if (bossAI.rb != null)
        {
            bossAI.rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else Debug.LogError("No RigidBody2D component found");

        if (bossAI.col != null)
        {
             bossAI.col.enabled = true;
        }
        else Debug.LogError("No Collider2D component found");

        Debug.Log("Boss wakes up");

        bossAI.SetState(bossAI.bossChaseState);
    }

    public void UpdateState()
    {
        // No update logic needed while waiting
    }
}