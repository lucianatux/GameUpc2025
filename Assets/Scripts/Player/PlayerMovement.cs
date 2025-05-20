using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    public bool canMove = true;
    private bool isBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove) return;

        if (Input.GetKeyDown(KeyCode.Z)) TriggerAnim("fireball");
        if (Input.GetKeyDown(KeyCode.X)) TriggerAnim("kick");
        if (Input.GetKeyDown(KeyCode.C)) TriggerAnim("croak");
        if (Input.GetKeyDown(KeyCode.V)) TriggerAnim("damage");
        if (Input.GetKeyDown(KeyCode.B)) TriggerAnim("die");
    }

    void TriggerAnim(string triggerName)
    {
        animator.ResetTrigger(triggerName);
        animator.SetTrigger(triggerName);
    }


    void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 velocity = input.normalized * moveSpeed;
            rb.velocity = velocity;

            // Flip X si se mueve hacia la izquierda o derecha
            if (Mathf.Abs(input.x) > 0.1f)
            {
                spriteRenderer.flipX = (input.x < 0);
            }

            // Cambia entre modo "back" y "frente"
            if (input.y > 0.1f)
                isBack = true;
            else if (input.y < -0.1f)
                isBack = false;

            animator.SetBool("isBack", isBack);
            animator.SetFloat("Speed", rb.velocity.magnitude);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
        }
    }
}

