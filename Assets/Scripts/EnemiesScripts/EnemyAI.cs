using UnityEngine;
namespace StatePattern
{
    public class EnemyAI : MonoBehaviour
    {
private IEnemyState currentState;

#region Estados del Enemigo
[HideInInspector] public WaitingState enemyWaitingState;
[HideInInspector] public AttackState enemyAttackState;
[HideInInspector] public ChaseState enemyChaseState;
#endregion

// -------------------------------------------
// Movimiento y Rango de Detección
// -------------------------------------------
[Header("Movimiento y Detección")]
[Tooltip("Velocidad de movimiento del enemigo.")]
[SerializeField] private float enemyMoveSpeed = 4;

[Tooltip("Distancia máxima a la que el enemigo empieza a perseguir.")]
[SerializeField] private float followRange = 7;

[Tooltip("Distancia a la que el enemigo entra en estado de ataque.")]
[SerializeField] private float attackRange = 4;

[Tooltip("Distancia de retroceso cuando recibe daño.")]
[SerializeField] public float retreatDistance = 3;

[Tooltip("Velocidad de rotación del enemigo hacia el jugador.")]
[SerializeField] private float rotationSpeed = 200;
[SerializeField] public LayerMask walkableLayer; // Asigna esto en el Inspector


// -------------------------------------------
// Ataque
// -------------------------------------------
[Header("Ataque")]
[Tooltip("Tiempo de espera entre ataques.")]
[SerializeField] private float attackCooldown = 2;

[Tooltip("Prefab del proyectil que dispara el enemigo.")]
[SerializeField] private GameObject bulletPrefab;

[Tooltip("Transform del arma desde donde se dispara el proyectil.")]
public Transform weaponTransform;

[Tooltip("Temporizador para controlar el ataque.")]
public float attackTimer;

// -------------------------------------------
// Prefabs de Aviso
// -------------------------------------------
[Header("Prefabs de Aviso")]
[Tooltip("Prefab de la advertencia antes de atacar.")]
[SerializeField] private GameObject warningPrefab;

[Tooltip("Prefab que se muestra cuando el enemigo detecta al jugador.")]
[SerializeField] private GameObject attentionPrefab;

// -------------------------------------------
// Referencias de Componentes
// -------------------------------------------
[Header("Referencias de Componentes")]
[Tooltip("Referencia al transform del jugador.")]
private Transform playerTransform;

[Tooltip("SpriteRenderer del enemigo para efectos visuales.")]
private SpriteRenderer spriteRenderer;

[Tooltip("Rigidbody2D del enemigo para aplicar físicas.")]
private Rigidbody2D rb;

// -------------------------------------------
// Daño y Knockback
// -------------------------------------------
[Header("Daño y Knockback")]
[Tooltip("Color original del enemigo antes de recibir daño.")]
private Color originalColor;

[Tooltip("Duración del parpadeo blanco cuando recibe daño.")]
[SerializeField] private float flashDuration = 0.1f;

[Tooltip("Indica si el enemigo está aturdido.")]

private bool isStunned = false;

[Tooltip("Duración del aturdimiento tras recibir daño.")]

[SerializeField] private float stunDuration = 0.5f;
[SerializeField] private int enemyWaveID;

[SerializeField] private int enemyRoomID;

public bool isActive;

[SerializeField] GameObject player;
       private void InitializeStates()
        {
            player = GameObject.FindWithTag("Player");
            playerTransform = player.transform;
            enemyAttackState = new AttackState(attackTimer, attackCooldown, warningPrefab, attackRange, bulletPrefab, weaponTransform, playerTransform);
            enemyChaseState  = new ChaseState(followRange, attackRange, playerTransform, attentionPrefab);
            enemyWaitingState = new WaitingState(enemyRoomID, enemyWaveID, isActive);
            isActive = false;
            SetState(enemyWaitingState);
        }
        
        public float GetDistanceToPlayer()
        {
            return Vector2.Distance(transform.position, playerTransform.position);
        }

public void MoveTowards(Vector2 destination, float speedMultiplier = 1)
{
    if (isStunned) return; // al estar estuneado, no se podra mover

    Vector2 direction = (destination - (Vector2)transform.position).normalized; //calculamos la direccion 
    float moveDistance = enemyMoveSpeed * Time.deltaTime * speedMultiplier;
    
    // Raycast frontal
    RaycastHit2D hitFront = Physics2D.Raycast(transform.position, direction, moveDistance, LayerMask.GetMask("Wall"));
    
    if (hitFront.collider != null) // REVISARR
    {
        // Intenta moverse en una dirección alternativa (izquierda o derecha)
        Vector2 altDirection1 = new Vector2(-direction.y, direction.x); // Gira 90° a la izquierda
        Vector2 altDirection2 = new Vector2(direction.y, -direction.x); // Gira 90° a la derecha

        bool canMoveLeft = !Physics2D.Raycast(transform.position, altDirection1, moveDistance, LayerMask.GetMask("Wall"));
        bool canMoveRight = !Physics2D.Raycast(transform.position, altDirection2, moveDistance, LayerMask.GetMask("Wall"));

        if (canMoveLeft)
        {
            transform.position += (Vector3)altDirection1 * moveDistance;
        }
        else if (canMoveRight)
        {
            transform.position += (Vector3)altDirection2 * moveDistance;
        }
        else
        {
            // Si está totalmente bloqueado, retrocede
            transform.position -= (Vector3)direction * moveDistance;
        }
    }
    else
    {
        // Si no hay colisión, avanza normalmente
        transform.position += (Vector3)direction * moveDistance;
    }
}
        private void Start()
        {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
            InitializeStates();
        }
        private void Update()
        {
            if (isActive && Input.GetKeyDown(KeyCode.K)) 
            {
                EnemyTakeDamage();
                Die();
            }  
            currentState.UpdateState();

            
        }

        public void SetState(IEnemyState iEnemyState)
        {
            currentState = iEnemyState;
            iEnemyState.EnterState(this);
        }
    

    public void LookAt(Vector2 destination, float speedMultiplier = 1)
        {
            Vector3 direction = (Vector3)destination - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * speedMultiplier);
        }
    
        public float GetAngleToPlayer()
        {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float angle;
        
        return angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

public void EnemyTakeDamage()
    {
        Debug.Log("Enemy recibió daño");
        attackTimer = attackCooldown / 2;
        // Cambio de color a blanco
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            Invoke(nameof(ResetColor), flashDuration);
        }

        // Se queda quieto por el tiempo de stun
        isStunned = true;
        Invoke(nameof(RemoveStun), stunDuration);
    }
    private void RemoveStun()
    {
        isStunned = false;
    }
    private void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

        public float attentionTimer = 0;
        public void Attention()
    {
        if (attentionTimer <= 0)
        {
        
        attentionTimer = .3f;
        GameObject attention = Object.Instantiate(attentionPrefab, transform.position,  Quaternion.Euler(0, 0, 0));
        GameObject.Destroy (attention, 2);
        }
    }

    public void Die()
    {   
        Debug.Log("se muere");
        isActive = false;
        RoomManager.Instance.NotifyEnemyDeath(); // le avisás al RoomManager
        gameObject.SetActive(false);
        // También podés lanzar un evento si querés avisarle al RoomManager
    }

}
}