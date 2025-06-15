
using UnityEngine;
/// <summary>
/// Handles the player's health, damage response, and death behavior.
/// Inherits from LifeSystem for base health management.
/// </summary>
public class PlayerHealth : LifeSystem
{
    PlayerAnimatorController _playerAnimator;
    private PlayerMovement _playerMovement;

    /// <summary>
    /// Initializes necessary components.
    /// </summary>
    protected override void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null) Debug.LogError("PlayerMovement component not found on Player.");

        _playerAnimator = GetComponent<PlayerAnimatorController>();
        if (_playerAnimator == null) Debug.Log("Player Animator Not found");

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
        StartCoroutine(_playerMovement.StunPlayer(.15f));

        if (_playerAnimator != null)
        {
            _playerAnimator.TriggerAnim("damage");
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

        animator.SetBool("isDead", true);

        if (_playerMovement != null)
        {
            _playerMovement.canMove = false;
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

        _playerAnimator.enabled = false;

        AbilityController abilityController = GetComponent<AbilityController>();

        if (abilityController != null)
        {
            abilityController.enabled = false;
        }
        else
        {
            Debug.LogError("Ability Controller not found on Player.");
        }

        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        PlayerInputHandler playerInputHandler = GetComponent<PlayerInputHandler>();
        
        if (playerInputHandler != null)
        {
            playerInputHandler.enabled = false;
        }
        else
        {
            Debug.LogError("playerInputHandler not found on Player.");
        }

        PlayerEventsManager.Instance?.PlayerDeath();
    }
}