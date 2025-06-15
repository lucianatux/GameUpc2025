using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla los parámetros del Animator según el movimiento y las acciones del jugador.
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] protected PlayerMovement movement;
    protected Animator animator;

    private bool isBack = false;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogWarning("Animator not found");

        if (movement == null) movement = GetComponent<PlayerMovement>();
    }

    protected virtual void Update()
    {
        Vector2 input = movement.CurrentInput;

         // Si el input vertical es positivo, el personaje está de espaldas.
        if (input.y > 0.1f)
            isBack = true;
        else if (input.y < -0.1f)
            isBack = false;

        animator.SetBool("isBack", isBack);
        animator.SetFloat("Speed", movement.CurrentVelocity.magnitude);
        // Se activa isWalkingDown solo si la única tecla presionada es hacia abajo
        bool isOnlyPressingDown = input.y < -0.1f && Mathf.Abs(input.x) < 0.1f && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D);
        animator.SetBool("isWalkingDown", isOnlyPressingDown);
        //animator.SetBool("isWalkingDown", isOnlyWalkingDown);
        
    }

     /// <summary>
    /// Triggers an animation using a trigger parameter name.
    /// Ensures the trigger is reset to avoid animation lock.
    /// </summary>
    /// <param name="triggerName">The name of the trigger parameter in the Animator.</param>
    
    public virtual void TriggerAnim(string triggerName)
    {
        // Se asegura de reiniciar el trigger antes de activarlo (evita animaciones bloqueadas).
        animator.ResetTrigger(triggerName);
        animator.SetTrigger(triggerName);
    }
}
