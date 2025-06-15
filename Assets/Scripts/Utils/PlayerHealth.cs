using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles the player's health, damage response, and death behavior.
/// Inherits from LifeSystem for base health management.
/// </summary>
public class PlayerHealth : LifeSystem
{
    private PlayerMovement playerMovement;

    /// <summary>
    /// Initializes necessary components.
    /// </summary>
    protected override void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null) Debug.LogError("PlayerMovement component not found on Player.");

        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Animator component not found on Player.");

        base.Start();
        PlayerEventsManager.Instance?.PlayerHeal(); // begins with full life
    }

    /// <summary>
    /// Applies damage to the player and triggers feedback.
    /// </summary>
    /// <param name="damage">Amount of damage taken.</param>
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (animator != null)
        {
            animator.SetTrigger("damage");
        }

        PlayerEventsManager.Instance?.PlayerDamaged(); //Notify Player Events Manager
        Debug.Log("El jugador recibe daño");
    }

    /// <summary>
    /// Triggers player death: disables movement and physics, plays animation, and notifies systems.
    /// </summary>
    protected override void Die()
    {
        base.Die();
        
 
        if (playerMovement != null)
        {
            playerMovement.canMove = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            Debug.LogError("Rigidbody2D not found on Player.");
        }

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            col.enabled = false;
        }
        else
        {
            Debug.LogError("Collider2D not found on Player.");
        }

        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        PlayerEventsManager.Instance?.PlayerDeath();
        // Iniciar respawn después de una pequeña espera
        StartCoroutine(RespawnDelay());
    }
    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(2f); // Tiempo para la animación de muerte

        PlayerRespawn respawn = GetComponent<PlayerRespawn>();
        if (respawn != null)
            respawn.Respawn();
    }
}