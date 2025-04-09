using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rb.velocity = moveInput * _moveSpeed;
    }
}
