using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] private float enemyMoveSpeed;

    [Tooltip("Distancia máxima a la que el enemigo empieza a perseguir.")]
    [SerializeField] private float followRange = 7  ;

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

    [SerializeField] private float stunDuration;
    [SerializeField] private int waveID;

    [SerializeField] private int roomID;

    NavMeshAgent agent; 
    Animator animator;
    private string currentAnim; 


    public bool isActive;
     GameObject player;
       private void InitializeStates()
        {
            player = GameObject.FindWithTag("Player");
            playerTransform = player.transform;
            enemyAttackState = new AttackState(attackTimer, attackCooldown, warningPrefab, attackRange, bulletPrefab, weaponTransform, playerTransform);
            enemyChaseState  = new ChaseState(followRange, attackRange, playerTransform, attentionPrefab, agent);
            enemyWaitingState = new WaitingState(roomID, waveID);
            SetState(enemyWaitingState);
        }

        public float GetDistanceToPlayer()
        {
            return Vector2.Distance(transform.position, playerTransform.position);
        }


        private void Start()
        {
        RoomManager.Instance.OnRoomExited += Deactivate;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
            InitializeStates();
        }

        private void Deactivate(int _roomID)
        {
            if (_roomID != roomID) return;
            SetState(enemyWaitingState);
            RoomManager.Instance.OnRoomEntered -= Deactivate;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) 
            {
                EnemyTakeDamage();
            }  
            currentState.UpdateState();

            // Si el jugador está a la derecha

            UpdateSprite();
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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Convertimos de -180°~180° a 0°~360°
        if (angle < 0)
        
        angle += 360f;

        return angle;
        
    }


        public void UpdateSprite()
        {
            float angle = GetAngleToPlayer();

            if (angle <= 90 || angle >= 270)
            {
                spriteRenderer.flipX = false;
            }
                    // Si el player está a la izquierda
            else
            {
                spriteRenderer.flipX = true;
            }
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
                ChangeAnimationState(AnimName.DamageAnim);
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
        if (attentionPrefab == null) return;
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

   public void Chase(Transform toChase)
    {
        if (isStunned) return;
        ChangeAnimationState(AnimName.WalkAnim); //seteamos su animacion
        agent.SetDestination(toChase.position);
    }

    public void ChangeAnimationState (AnimName newAnim)
    {
        string newAnimString = newAnim.ToAnimString();

        if (currentAnim == newAnimString) return; //chequeamos que no se interrumpa a si misma

        animator.Play(newAnimString); //empieza la animacion

        currentAnim = newAnimString; //reseteamos la current animation a la que esta sucediendo
    }

    


}
}