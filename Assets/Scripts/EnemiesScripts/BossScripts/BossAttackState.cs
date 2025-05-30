using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern;

public class BossAttackState : IEnemyState
{
    private BossAI bossAI;
    private bool bombsActivated = false;

    private float attackTimer;
    private GameObject bulletPrefab;

    private bool isAttacking;
    private float attackRange;
    private float chargeVelocity;
    private Rigidbody2D rb;
    //[SerializeField] private List<GameObject> cherryBombs;
    public BossAttackState(float _attackTimer, GameObject _bulletPrefab, float _attackRange, float _chargeVelocity, Rigidbody2D _rb)
    {
        attackTimer = _attackTimer;
        attackRange = _attackRange;
        bulletPrefab = _bulletPrefab;
        chargeVelocity = _chargeVelocity;
        rb = _rb;
    }


    public void EnterState(EnemyAI _enemyAI)
    {
        if (_enemyAI is BossAI enemy)
        {
            bossAI = enemy;
        }
        else
        {
            Debug.LogError(this + "Error de casteo fallido");
        }

        /* foreach (Transform child in bossAI.transform)
          {
              if (child.name.Contains("Bomb")) // o directamente todos
              {
                  bossAI.cherryBombs.Add(child.gameObject);
              }
          }*/
    }

    public void UpdateState()
    {

        if (!isAttacking && bossAI.attackTimer < 0 && bossAI.GetPlayerInSight() == true)
        {
            PickRandomAttack();
        }
        Debug.Log("attack state update ");
        Debug.Log("is attacking" +  isAttacking);

        if (isAttacking == false && bossAI.attackTimer < 0f)
        {
            Debug.Log("from attack to chase");
            bossAI.SetState(bossAI.bossChaseState);
        }

    }

    private IEnumerator BombAttack(float seconds)
    {
        // animacion 
        isAttacking = true;
        ActivateBombs();
        bossAI.attackTimer = 3f;

        yield return new WaitForSeconds(seconds);
        isAttacking = false;
    }

    private IEnumerator ChargeAttack(float seconds)
    {
        // animacion 
        isAttacking = true;
        //ChargeForward();
        bossAI.attackTimer = 3f;

        yield return new WaitForSeconds(seconds);
        //bossAI.GoBackToOriginalY();
        isAttacking = false;
    }
    private void ActivateBombs()
    {
        for (int i = 0; i <= 4; i++)
        {
            if (bossAI.cherryBombs.Count >= i)
            {
                bossAI.cherryBombs[i].SetActive(true);
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

    private void ChargeForward()
    {
        //Not yet implemented
    }

    private void PickRandomAttack()
    {
        int attackChoice = Random.Range(0, 2); // elige random entre 1 y 0
                                               //segun el random anterior usa un ataque

        switch (attackChoice)
        {
            case 0:
                bossAI.StartCoroutine(BombAttack(1f));
                Debug.Log("Se llama al ataque Bomba");

                break;
            case 1:
                bossAI.StartCoroutine(ChargeAttack(1f));

                Debug.Log("Se llama al ataque Carga");

                break;
        }
    }
}

