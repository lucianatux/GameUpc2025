using UnityEngine;
using UnityEngine.AI;
namespace StatePattern
{
    public class EnemyAI : MonoBehaviour
    {
        protected IEnemyState currentState;

        #region Estados del Enemigo
        [HideInInspector] public WaitingState enemyWaitingState;
        [HideInInspector] public AttackState enemyAttackState;
        [HideInInspector] public ChaseState enemyChaseState;
        #endregion

        #region Movimiento y Detección
        [Header("Movimiento y Detección")]

        [SerializeField] protected float attackRange;
        [SerializeField] public LayerMask walkableLayer;
        #endregion

        #region Ataque
        [Header("Ataque")]
        [SerializeField] protected float attackCooldown;
        [SerializeField] protected GameObject bulletPrefab;
        public Transform weaponTransform;
        public float attackTimer;
        #endregion

        #region Prefabs de Aviso
        [Header("Prefabs de Aviso")]
        [SerializeField] protected GameObject warningPrefab;
        [SerializeField] protected GameObject attentionPrefab;
        #endregion

        #region Referencias de Componentes
        [Header("Referencias de Componentes")]
        protected Transform playerTransform;
        protected SpriteRenderer spriteRenderer;
        public Rigidbody2D rb;
        protected NavMeshAgent agent;
        public Animator animator;
        public EnemyAnimatorController enemyAnimator;
        public Collider2D col;
        #endregion

        #region Daño y Knockback
        [Header("Daño y Knockback")]
        protected Color originalColor;
        [SerializeField] protected float flashDuration = 0.1f;
        public bool isStunned;
        [SerializeField] protected float stunDuration;
        #endregion

        #region Otros
        [SerializeField] protected int _waveID;
        [SerializeField] protected int roomID;
        public bool isActive;
        protected GameObject player;
        public float attentionTimer = 0;
        #endregion

        protected void Awake()
        {
            enemyAnimator = GetComponent<EnemyAnimatorController>();

            animator = GetComponent<Animator>();

            if (animator == null) Debug.LogError("EnemyAI: Animator not found");

            agent = GetComponent<NavMeshAgent>();

            if (agent == null) Debug.LogError("EnemyAI: NavMeshAgent not found");

            rb = GetComponent<Rigidbody2D>();

            if (rb == null) Debug.LogError("EnemyAI: Rigidbody2D not found");

            col = GetComponent<Collider2D>();

            if (col == null)
            {
                Debug.LogError("EnemyAI: Collider2D not found");
            }
            else
            {
                col.enabled = false;
            }

            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) Debug.LogError("EnemyAI: SpriteRenderer componenent not found");

            player = GameObject.FindWithTag("Player");

            if (player == null)
            {
                Debug.LogError("EnemyAI: Player with 'Player' tag not found"); return;
            }
            else
            {
                playerTransform = player.transform;

            }

            if (playerTransform == null)
            {
                Debug.LogError("EnemyAI: Player trasform componenent not found");
            }

            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        protected virtual void Start()
        {
            InitializeStates();

            //Initialize enemy with no collision or physics
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }

            //Get original color 
            if (spriteRenderer != null)
                originalColor = spriteRenderer.color;
        }

        /// <summary>
        //Initialize states and give them their respectives variables
        /// <summary>
        protected virtual void InitializeStates()
        {
            enemyWaitingState = new WaitingState(roomID, _waveID);
            enemyAttackState = new AttackState(attackCooldown, attackRange, bulletPrefab, weaponTransform, playerTransform);
            enemyChaseState = new ChaseState(attackRange, playerTransform);

            //set initial state
            SetState(enemyWaitingState);
        }

        /// <summary>
        //Returns distance to player
        /// <summary>
        public float GetDistanceToPlayer()
        {
            if (playerTransform == null) return 0f;
            return Vector2.Distance(transform.position, playerTransform.position);
        }

        protected virtual void Update()
        {
            //Calls current state's UpdateState()
            if (currentState == null)
            {
                Debug.LogError("current state not found");
            }
            else
            {
                if (!isActive) return;
                currentState?.UpdateState();
            }
            if (!isActive)
            {
                 //           animator.SetBool("isWalking", false);

            }
            //Attack Cooldown Handler
                attackTimer -= Time.deltaTime;   
            UpdateSprite();
        }

        /// <summary>
        //Sets states when called
        /// <summary>
        public void SetState(IEnemyState iEnemyState)
        {
            currentState = iEnemyState;
            iEnemyState.EnterState(this);
        }

        /// <summary>
        //Returns angle to player
        /// <summary>
        public float GetAngleToPlayer()
        {
            if (playerTransform == null) return 0f;

            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < 0)
                angle += 360f;

            return angle;
        }
        /// <summary>
        //Flips enemy sprite when needed
        /// <summary>
        public void UpdateSprite()
        {
            if (!isActive || playerTransform == null || spriteRenderer == null) return;
            //if (!isStunned) return; 
            // if (rb.velocity != Vector2.zero)
            //  {
            //     Debug.Log(this + "is walking");
            //      enemyAnimator.TriggerAnim("walk");
            //  }
        float angle = GetAngleToPlayer();

            spriteRenderer.flipX = angle > 90 && angle < 270;
        }
        /// <summary>
        //In charge of displaying enemy in red
        /// <summary>
        public void EnemyTakeDamage()
        {
            Debug.Log("Enemy recibió daño");
            //attackTimer = attackCooldown / 2;
            isStunned = true;

            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
                Invoke(nameof(ResetColor), flashDuration);
            }

            Invoke(nameof(RemoveStun), stunDuration);
        }

        protected void ResetColor()
        {
            if (spriteRenderer != null)
                spriteRenderer.color = originalColor;
        }

        protected void RemoveStun()
        {
            isStunned = false;
        }

        /// <summary>
        //summons each alert VFX 
        /// <summary>
        public void ShowAlert(string type)
        {
            GameObject prefabToSpawn = null;

            if (type == "attention")
            {
                if (attentionPrefab == null)
                {
                    Debug.LogError("EnemyAI: attentionPrefab no asignado");
                    return;
                }

                if (attentionTimer > 0) return;

                attentionTimer = 0.3f;
                prefabToSpawn = attentionPrefab;
            }
            else if (type == "Warning")
            {
                if (warningPrefab == null)
                {
                    Debug.LogError("EnemyAI: warningPrefab no asignado");
                    return;
                }

                prefabToSpawn = warningPrefab;
            }
            else
            {
                Debug.LogError("EnemyAI: tipo de alerta desconocido: " + type);
                return;
            }

            GameObject alert = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            Destroy(alert, 2f);
        }

        /// <summary>
        //In charge of chasing player with navmesh
        /// <summary>
        public void Chase(Transform toChase)
        {
            //animator.SetTrigger("walk");
            if (isStunned) 
            {
                agent.ResetPath();

                Debug.Log("esta stunned");
                return;
            }
            if (isActive == false) return;

            if (agent == null)
            {
                Debug.LogError("NavMesh agent not found");
            }

            if (enemyAnimator == null)
            {
                Debug.LogError("enemy animator  not found");
            }

            agent.SetDestination(toChase.position);
        }

        public void Die()
        {
            animator.ResetTrigger("idle");
            animator.ResetTrigger("attack");
            animator.SetBool("isWalking", false);
            animator.ResetTrigger("damage");
            
            animator.SetTrigger("die");

            agent.ResetPath();
            agent.enabled = false;
            isActive = false;

        }
            [SerializeField] bool isDistance;
            [SerializeField] float projectileSpeed;

        public void Shoot()
        {
            if (weaponTransform == null)
            {
                Debug.LogError("AttackState: weaponTransform is null in Shoot().");
                return;
            }
            GameObject bullet = bulletPrefab;
            if (bulletPrefab == null)
            {
                Debug.LogError("AttackState: bulletPrefab is null in Shoot().");
                return;
            }
            float angle = GetAngleToPlayer();

            if (isDistance)
            {
                Vector2 direction = (playerTransform.position - weaponTransform.position).normalized;
                float angleB = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, angleB - 90f);           // Create a rotation quaternion for the fireball to face the direction of travel. 

                GameObject projectile = Instantiate(bullet, weaponTransform.position, rotation);  // Instantiate the fireball projectile prefab at the origin position with the calculated rotation.

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();       // Give the projectile a velocity so it moves in the intended direction.
                rb.velocity = direction.normalized * projectileSpeed;
            }
            else
            {
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

            }
            //Destroy(newBullet, 0.2f);
            Debug.Log("Shot fired with angle: " + angle);

        }
        /*Testing
        private void Deactivate(int _roomID)
        {
            if (_roomID != roomID) return;
            SetState(enemyWaitingState);
            Debug.Log("se desactiva al player salir de la room");
            RoomManager.Instance.OnRoomEntered -= Deactivate;
        }
        */
    }
}
