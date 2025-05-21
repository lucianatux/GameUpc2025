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
 

        [Tooltip("Distancia a la que el enemigo entra en estado de ataque.")]
        [SerializeField] protected float attackRange = 4;

        // -------------------------------------------
        // Ataque
        // -------------------------------------------
        [Header("Ataque")]
        [Tooltip("Tiempo de espera entre ataques.")]
        [SerializeField] protected float attackCooldown = 2;

        [Tooltip("Prefab del proyectil que dispara el enemigo.")]
        [SerializeField] protected GameObject bulletPrefab;

        [Tooltip("Transform del arma desde donde se dispara el proyectil.")]
        public Transform weaponTransform;

        [Tooltip("Temporizador para controlar el ataque.")]
        [HideInInspector] public float attackTimer;

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
        [HideInInspector] protected Transform playerTransform;

        [Tooltip("SpriteRenderer del enemigo para efectos visuales.")]
        [HideInInspector] protected SpriteRenderer spriteRenderer;

        [Tooltip("Rigidbody2D del enemigo para aplicar físicas.")]
        [HideInInspector] public Rigidbody2D rb;

        // -------------------------------------------
        // Daño y Knockback
        // -------------------------------------------
        [Header("Daño y Knockback")]
        [Tooltip("Color original del enemigo antes de recibir daño.")]
        protected Color originalColor;

        [Tooltip("Duración del parpadeo blanco cuando recibe daño.")]
        [SerializeField] private float flashDuration = 0.1f;

        [Tooltip("Indica si el enemigo está aturdido.")]

        public bool isStunned = false;

        [Tooltip("Duración del aturdimiento tras recibir daño.")]

        [SerializeField] protected float stunDuration;
        [SerializeField] protected int _waveID;
        //[HideInInspector] public AnimationStateController animController;
        [SerializeField] protected int roomID;

        protected NavMeshAgent agent; 
        Animator animator;
        private string currentAnim; 

        public bool isActive;
        protected GameObject player;
    

           protected virtual void InitializeStates()
            {
                player = GameObject.FindWithTag("Player");
                playerTransform = player.transform;
                enemyWaitingState = new WaitingState(roomID, _waveID);
                enemyAttackState = new AttackState(attackTimer, attackCooldown, warningPrefab, attackRange, bulletPrefab, weaponTransform, playerTransform);
                enemyChaseState = new ChaseState(attackRange, playerTransform, attentionPrefab, agent);
                SetState(enemyWaitingState);
            }
            protected void Awake()
            {
            animator = GetComponent<Animator>();
            //animController = GetComponent<AnimationStateController>();
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody2D>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
            }
            public float GetDistanceToPlayer()
            {
                if(playerTransform == null) return 0f;
                return Vector2.Distance(transform.position, playerTransform.position);
            }


            protected virtual void Start()
            {
            //RoomManager.Instance.OnRoomExited += Deactivate;
                InitializeStates();
                GetComponent<Collider2D>().enabled = false;
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.bodyType = RigidbodyType2D.Static; // para que no lo afecte la física
                }
                originalColor = spriteRenderer.color;
            }

            private void Deactivate(int _roomID)
            {
                if (_roomID != roomID) return;
                SetState(enemyWaitingState);
                Debug.Log("se desactiva al player salir de la room");
                RoomManager.Instance.OnRoomEntered -= Deactivate;

            }

            protected virtual void Update()
            {
                currentState.UpdateState();
                UpdateSprite();

            }

            public void SetState(IEnemyState iEnemyState)
            {
                currentState = iEnemyState;
                iEnemyState.EnterState(this);
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
                if (!isActive) return;
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
                    isStunned = true;
                    Invoke(nameof(RemoveStun), stunDuration);
                }
            protected void RemoveStun()
            {
                isStunned = false;
            }
            protected void ResetColor()
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
            Debug.Log("se muere " + this + " por el enemy ai");
            isActive = false;
            gameObject.SetActive(false);
            // También podés lanzar un evento si querés avisarle al RoomManager
        }

       public void Chase(Transform toChase)
        {
            if (isStunned) return;
            //animController.Play(AnimName.WalkAnim, 1);
            agent.SetDestination(toChase.position);
        }

    


    }
}