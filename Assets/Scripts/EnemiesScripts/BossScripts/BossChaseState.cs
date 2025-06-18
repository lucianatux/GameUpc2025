using StatePattern;
using UnityEngine;

/// <summary>
/// State where the boss chases the player horizontally while checking if it's in attack range.
/// </summary>
public class BossChaseState : IEnemyState
{
    private BossAI bossAI;

    private bool isInAttackSight;
    private Transform playerTransform;

    /// <summary>
    /// Constructor receives the player's transform to follow.
    /// </summary>
    public BossChaseState(Transform _playerTransform)
    {
        playerTransform = _playerTransform;
    }

    /// <summary>
    /// Called when entering this state. Attempts to cast the EnemyAI to BossAI.
    /// </summary>
    public void EnterState(EnemyAI _enemyAI)
    {
        //Debug.Log("Entered Chase State");

        if (_enemyAI is BossAI enemy)
        {
            bossAI = enemy;
        }
        else
        {
            Debug.LogError(this + " Cast to BossAI failed.");
        }
                    bossAI.animator.SetBool("isWalking", true);

    }

    /// <summary>
    /// Main logic for the Chase state: move horizontally and switch to Attack state if the player is in sight and cooldown allows.
    /// </summary>
    public void UpdateState()
    {
        isInAttackSight = bossAI.GetPlayerInSight();
        Debug.Log(isInAttackSight);

        bossAI.ChaseHorizontally(playerTransform); // Follow the player horizontally
        Debug.Log("Chase update");

        // If player is in sight and the attack cooldown is over, transition to attack state
        if (isInAttackSight && bossAI.attackTimer < 0f)
        {           
            bossAI.animator.ResetTrigger("walk");
            bossAI.animator.SetBool("walk", false);
            bossAI.SetState(bossAI.bossAttackState);
        }
    }
}