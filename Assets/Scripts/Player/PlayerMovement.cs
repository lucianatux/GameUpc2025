using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Rigidbody2D _rb;
    public bool canMove = true;
    private AnimationStateController _animController;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        canMove = true;
        _rb = GetComponent<Rigidbody2D>();
        _animController = GetComponent<AnimationStateController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
         if (Input.GetKeyDown(KeyCode.C))
        {
            GameEventsManager.Instance.PlayerCroak(); // Activás el evento
        }
         if (Input.GetKeyDown(KeyCode.D))
        {
            GameEventsManager.Instance.PlayerDamaged(); // Activás el evento
        }
         if (Input.GetKeyDown(KeyCode.K))
        {
            GameEventsManager.Instance.PlayerKick(); // Activás el evento
        }
    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rb.velocity = moveInput * _moveSpeed;
        }
        CheckAnimation();
    }

    private void CheckAnimation()
    {
        if (_rb.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_rb.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (_rb.velocity.x != 0 || _rb.velocity.y != 0)
        {
            _animController.Play(AnimName.WalkAnim, 1);
        }
        else if (_rb.velocity.x == 0 && _rb.velocity.y == 0)
        {
            _animController.Play(AnimName.IdleAnim, 1);
        }
    }
    
}

