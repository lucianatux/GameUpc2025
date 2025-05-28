using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla los parámetros del Animator según el movimiento y las acciones del jugador.
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    private Animator animator;

    private bool isBack = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (movement == null) movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Vector2 input = movement.CurrentInput;

         // Si el input vertical es positivo, el personaje está de espaldas.
        if (input.y > 0.1f)
            isBack = true;
        else if (input.y < -0.1f)
            isBack = false;

        animator.SetBool("isBack", isBack);
        animator.SetFloat("Speed", movement.CurrentVelocity.magnitude);
        animator.SetBool("isWalkingDown", input.y < -0.1f);
    }

    public void TriggerAnim(string triggerName)
    {
        // Se asegura de reiniciar el trigger antes de activarlo (evita animaciones bloqueadas).
        animator.ResetTrigger(triggerName);
        animator.SetTrigger(triggerName);
    }
}
