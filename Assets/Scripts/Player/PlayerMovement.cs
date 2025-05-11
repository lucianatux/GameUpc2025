using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rb.velocity = moveInput * _moveSpeed;
    }
    
}
