using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern;

public class BossAttackState : IEnemyState
{
    private BossAI bossAI;

    private float attackTimer;
    private GameObject bulletPrefab;

    private bool isAttacking;
    private float attackRange;
    private float chargeVelocity;
    private Rigidbody2D rb;
    [SerializeField] private List<GameObject> cherryBombs;
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
        
        foreach (Transform child in bossAI.transform)
        {
            if (child.name.Contains("Bomb")) // o directamente todos
            {
                cherryBombs.Add(child.gameObject);
            }
        }
    }

    public void UpdateState()
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

    private IEnumerator BombAttack(float seconds)
    {
        // animacion 
        isAttacking = true;
        ActivateBombs();

        yield return new WaitForSeconds(seconds);

        isAttacking = false;

    }

    private IEnumerator ChargeAttack(float seconds)
    {
        // animacion 
        isAttacking = true;
        ChargeForward();
        yield return new WaitForSeconds(seconds);
        bossAI.GoBackToOriginalY();
        isAttacking = false;


    }
    private void ActivateBombs()
    {
        foreach (GameObject bomb in cherryBombs)
        {
            bomb.SetActive(true);
            Debug.Log("se activa bomb");
        }
    }

    private void ChargeForward()
    {
        
    }

}

