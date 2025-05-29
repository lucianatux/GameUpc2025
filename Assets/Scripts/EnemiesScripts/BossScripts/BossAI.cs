using StatePattern;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/// <summary>
/// Controls the boss AI using a state machine with wait, chase, and attack states.
/// </summary>
public class BossAI : EnemyAI
{
    [Header("Boss States")]
    [HideInInspector] public BossWaitingState bossWaitingState;
    [HideInInspector] public BossAttackState bossAttackState;
    [HideInInspector] public BossChaseState bossChaseState;

    [Header("Attack Settings")]
    [Tooltip("Charge speed for special attack.")]
    [SerializeField] private float chargeVelocity;

    [Tooltip("List of cherry bombs (special projectiles).")]
    public List<GameObject> cherryBombs = new List<GameObject>();

    [Header("Movement Settings")]
    [Tooltip("Original starting position of the boss.")]
    public Vector3 originalPosition;

    /// <summary>
    /// Called on start. Sets initial position and logs activation.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        originalPosition = transform.position;
        Debug.Log("Boss activated.");
    }

    /// <summary>
    /// Initializes the boss states and sets the initial state.
    /// </summary>
    protected override void InitializeStates()
    {
        Debug.Log("Boss states initialized.");

        bossWaitingState = new BossWaitingState(roomID, _waveID);
        bossChaseState = new BossChaseState(playerTransform);
        bossAttackState = new BossAttackState(attackTimer, bulletPrefab, attackRange, chargeVelocity, rb);

        SetState(bossWaitingState);
    }

    /// <summary>
    /// Makes the boss chase the player while staying on its original Y position.
    /// </summary>
    public void ChaseHorizontally(Transform toChase)
    {
        if (isStunned) return;

        Vector3 targetPosition = new Vector3(toChase.position.x, originalPosition.y, 0);
        Debug.DrawLine(transform.position, targetPosition, Color.red);
        agent.SetDestination(targetPosition);
    }

    /// <summary>
    /// Sends the boss back to its original Y position, keeping X unchanged.
    /// </summary>
    public void GoBackToOriginalY()
    {
        Vector3 yResetPosition = new Vector3(transform.position.x, originalPosition.y, 0);
        agent.SetDestination(yResetPosition);
    }

    /// <summary>
    /// Casts a vertical ray to detect the player directly below.
    /// </summary>
    public bool GetPlayerInSight()
    {
        float rayDistance = 15f;
        int playerLayer = 1 << LayerMask.NameToLayer("Player");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, playerLayer);
        Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Boss raycast meets player.");
            return true;
        }

        return false;
    }
}