using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se encarga de aplicar movimiento físico y rotación del sprite.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    public bool canMove = true;

    public Vector2 CurrentVelocity => rb.velocity;
    public Vector2 CurrentInput { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
         // Se intenta obtener el SpriteRenderer si no fue asignado manualmente.
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            // Se detiene el movimiento si está deshabilitado.
            rb.velocity = Vector2.zero;
            CurrentInput = Vector2.zero;
            return;
        }

        CurrentInput = inputHandler.MovementInput;
        rb.velocity = CurrentInput * moveSpeed;

        // Se voltea el sprite horizontalmente según la dirección del movimiento.
        if (Mathf.Abs(CurrentInput.x) > 0.1f)
            spriteRenderer.flipX = (CurrentInput.x < 0);
    }
}


