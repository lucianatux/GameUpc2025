using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
        public float attackCooldown = 1f;

        public bool canAttack = true;
    private float lastAttackTime = 0f; // Última vez que atacó
    public GameObject attackEffectPrefab; // Prefab del efecto de ataque
    AnimationStateController animController;
    public Transform player; // Referencia al jugador

    void Start()
    {
        canAttack = true;
        player = GetComponent<Transform>();
        animController = GetComponent<AnimationStateController>();
    }

    void Update()
    {
        if (Time.time >= lastAttackTime + attackCooldown && canAttack)
        {
        if (Input.GetMouseButtonDown(0))
            {
                AttackBasic();
            }
        }
    }


    void AttackBasic()
    {
        
        lastAttackTime = Time.time;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)player.position).normalized; // Dirección del ataque
        StartCoroutine(AttackAnimation());
            // Instancia el efecto en el jugador
        GameObject effect = Instantiate(attackEffectPrefab, player.position, Quaternion.identity);

            // Rota el efecto para que apunte al mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Destruye el efecto después de 0.3 segundos
        Destroy(effect, 0.3f);
        Debug.Log("is attacking");
    }

    private IEnumerator AttackAnimation()
    {
        animController.Play(AnimName.AttackAnim, 1, true);
        yield return new WaitForSeconds(0.2f);
        animController.Unlock();       // 🔓 desbloquea justo antes de cambiar de animación
        animController.Play(AnimName.IdleAnim, 1);
    }
}
