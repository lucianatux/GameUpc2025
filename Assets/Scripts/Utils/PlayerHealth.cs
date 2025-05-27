using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : LifeSystem
{
    private PlayerMovement playerMovement;

    protected override void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        base.Start();
    }

    protected override void Die()
    {
        base.Die();

        // Bloquea el movimiento
        playerMovement.canMove = false;

        // Cambia físicas y colisiones
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;

        // Activa animación de muerte
        animator.SetTrigger("die");

        // Notifica evento de muerte
        PlayerEventsManager.Instance?.PlayerDeath();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        animator.SetTrigger("damage");
        PlayerEventsManager.Instance?.PlayerDamaged();
    }
}
