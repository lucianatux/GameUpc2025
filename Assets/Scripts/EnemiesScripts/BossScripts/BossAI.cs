using StatePattern;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;



public class BossAI : EnemyAI
{
    [HideInInspector] public BossWaitingState bossWaitingState;
    [HideInInspector] public BossAttackState bossAttackState;
    [HideInInspector] public BossChaseState bossChaseState;

    [Tooltip("Tiempo de espera entre ataques 1.")]
    [SerializeField] private float firstAttackCooldown = 3;

    [Tooltip("Tiempo de espera entre ataques 2.")]    
    [SerializeField] private float secondAttackCooldown = 3;

    [Tooltip("Tiempo de espera entre ataques 2.")]    
    [SerializeField] private float chargeVelocity;

     public List<GameObject> cherryBombs = new List<GameObject>();



    [Tooltip("Posicion original del Boss.")]

    public Vector3 originalPosition;


        protected override void Start()
    {
        base.Start();

        originalPosition = transform.position;

        Debug.Log("se activa el boss");

    }

    protected override void InitializeStates()
    {
        Debug.Log("se inician los estados");

        bossWaitingState = new BossWaitingState(roomID, _waveID);
        bossChaseState = new BossChaseState(playerTransform);
        bossAttackState = new BossAttackState(attackTimer, bulletPrefab, attackRange,chargeVelocity, rb);    
        
        SetState(bossWaitingState);
   
    }


    public void ChaseHorizontally(Transform toChase)
    {
        if (isStunned) return;
        Vector3 toChaseH  = new Vector3(toChase.position.x, originalPosition.y, 0);
        Debug.DrawLine(transform.position, toChaseH, Color.red);
        //animController.Play(AnimName.WalkAnim, 1);
        agent.SetDestination(toChaseH);
    }
    public void GoBackToOriginalY()
    {
        Vector3 originalY  = new Vector3(transform.position.x, originalPosition.y, 0);
        agent.SetDestination(originalY);

    }

    public bool GetPlayerInSight()
    {
        float rayDistance = 15;
        var layer = 1 << LayerMask.NameToLayer("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0,-1), rayDistance, layer);
        Debug.DrawRay(transform.position, new Vector2(0,-1) * rayDistance, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Boss Raycast meets player");
            return true;
        }

        else return false;
    }

}
