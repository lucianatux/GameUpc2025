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
    private bool bombsActivated = false;

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
        bossAttackState = new BossAttackState(attackTimer, bulletPrefab, attackRange, chargeVelocity, rb, attackCooldown);

        SetState(bossWaitingState);
    }

    /// <summary>
    /// Makes the boss chase the player while staying on its original Y position.
    /// </summary>
    public void ChaseHorizontally(Transform toChase)
    {
        if (isStunned)
        {
            agent.ResetPath();

            Debug.Log("esta stunned");
            return;
        }



        Vector3 targetPosition = new Vector3(toChase.position.x, originalPosition.y, 0);
        Debug.DrawLine(transform.position, targetPosition, Color.red);
        agent.SetDestination(targetPosition);


        if (transform.position != targetPosition)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (toChase.position.x < transform.position.x)
        {
                animator.SetFloat("SpeedX", -1);
        }
        else
        {
                animator.SetFloat("SpeedX", 1);
        }
    
    }
        public void UpdateSprite()
        {
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
    // <summary>
    /// Devuelve true si el jugador está dentro del rango de visión circular.
    /// </summary>
    public bool GetPlayerInSight()
    {
        float detectionRadius = attackRange; // Radio del círculo de detección
        Vector2 detectionCenter = transform.position + Vector3.down * 1f; // Opcional: ajustar altura
        int playerLayer = 1 << LayerMask.NameToLayer("Player");

        Collider2D hit = Physics2D.OverlapCircle(detectionCenter, detectionRadius, playerLayer);

        // Visualización en el editor
        Debug.DrawLine(transform.position, detectionCenter, Color.yellow);
        DebugExtension.DrawCircle(detectionCenter, Color.red, detectionRadius); // Necesita método extra

        if (hit != null && hit.CompareTag("Player"))
        {
            Debug.Log("Boss detects player in circle.");
            return true;
        }

        return false;
    }
    public static class DebugExtension
    {
        public static void DrawCircle(Vector2 center, Color color, float radius = 1f, int segments = 32)
        {
            float angle = 0f;
            Vector2 prevPoint = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            for (int i = 1; i <= segments; i++)
            {
                angle += 2 * Mathf.PI / segments;
                Vector2 newPoint = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                Debug.DrawLine(prevPoint, newPoint, color);
                prevPoint = newPoint;
            }
        }
    }
    private void ActivateBombs()
    {
        for (int i = 0; i <= 4; i++)
        {
            if (cherryBombs.Count >= i)
            {
                cherryBombs[i].SetActive(true);
            }
            else
            {
                Debug.LogError("Index not reachable");
            }
            if (i == 4)
            {
                bombsActivated = true;
                Debug.Log("All bombs activated");
            }
        }
    }

    public override void EnemyTakeDamage()
    {
        base.EnemyTakeDamage();

        EnemiesEventsManager.Instance.BossDamaged();


        animator.SetTrigger("damage");
    }

}