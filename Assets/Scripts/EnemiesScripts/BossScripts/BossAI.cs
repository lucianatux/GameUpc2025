using StatePattern;
using UnityEngine;
using UnityEngine.AI;



public class BossAI : EnemyAI
{
    private IBossState bossCurrentState;

    [HideInInspector] public BossWaitingState bossWaitingState;
    [HideInInspector] public BossAttackState bossAttackState;
    [HideInInspector] public BossChaseState bossChaseState;

    [Tooltip("Tiempo de espera entre ataques 1.")]
    [SerializeField] private float firstAttackCooldown = 3;

    [Tooltip("Tiempo de espera entre ataques 2.")]    
    [SerializeField] private float secondAttackCooldown = 3;

    private bool isInAttackSight;


    [Tooltip("Posicion original del Boss.")]

    public Vector3 originalPosition;    




    protected override void InitializeStates()
    {
        player = GameObject.FindWithTag("Player");
        originalPosition = transform.position;
        playerTransform = player.transform;
        bossWaitingState = new BossWaitingState(roomID, _waveID);
        bossChaseState = new BossChaseState(isInAttackSight);
        bossAttackState = new BossAttackState(attackTimer, bulletPrefab, attackRange);        
    }


    public void ChaseHorizontally(Transform toChase)
    {
        if (isStunned) return;
        Vector3 toChaseH  = new Vector3(toChase.position.x, originalPosition.y, 0);
        Debug.DrawLine(transform.position, toChaseH, Color.red);

        //animController.Play(AnimName.WalkAnim, 1);
        agent.SetDestination(toChaseH);
    }

    public bool GetPlayerInSight()
    {
        float rayDistance = 100f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance);

        Debug.DrawRay(transform.position, transform.right * rayDistance, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
        return true;
        }

        else return false;
    }


        public void SetState(IBossState iBossState)
        {
            bossCurrentState = iBossState;
            iBossState.EnterState(this);
        }

}
