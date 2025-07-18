using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the behavior of a bomb: falling animation, enabling collision, fall damage,
/// explosion, and deactivation.
/// IN TESTING YET
/// </summary>
public class BombScript : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;

    [SerializeField] Transform playerTransform;
    private NavMeshAgent agent;

    [SerializeField] private float closeSpeed = 5;
    [SerializeField] private float mediumSpeed = 7;
    [SerializeField] private float farSpeed = 14;


    private bool canChase;
    [SerializeField] private int damage;
    private Vector3 originalPosition;
    private PlayerHealth PlayerHealth;

    [Tooltip("Prefab instantiated when the bomb hits the ground.")]
    [SerializeField] private GameObject fallPrefab;

    [Tooltip("Prefab instantiated when the bomb explodes.")]
    [SerializeField] private GameObject explosionPrefab;

    private Animator animator;


    //Set references and components
    private void Awake()
    {
        animator = GetComponent<Animator>();    

        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("agent component not found");
        }

        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    //Called when BossAI enables it
    private void OnEnable()
    {


        originalPosition = transform.position;
        // Reset position and start the fall animation
        transform.position = originalPosition;
        canChase = true;
        //StartCoroutine(BombFall(1f));
    }

    void Update()
    {
        if (GetDistanceToPlayer() >= 0 && GetDistanceToPlayer() < 4)
        {
            agent.speed = closeSpeed;
        }
        else if (GetDistanceToPlayer() >= 4 && GetDistanceToPlayer() < 10)
        {
            agent.speed = mediumSpeed;
        }
        else if (GetDistanceToPlayer() >= 10 && GetDistanceToPlayer() < 13)
        {
            agent.speed = farSpeed;
        }
        else if (GetDistanceToPlayer() >= 13)
        {
            agent.speed = farSpeed + GetDistanceToPlayer();
        }


        if (!canChase) return;
            
        agent.SetDestination(playerTransform.position);

        // transform.position = originalPosition;
    }
    //Bomb Falling CoRoutine

    //methods called in animator
    private void FallingDamage()
    {
        Instantiate(fallPrefab, transform.position, Quaternion.identity);
        int attackChoice = 1;

        switch (attackChoice)
        {
            case 0:

                break;
            case 1:

                animator.SetBool("Bounce", true);
                break;
        }
    }

    private void ExplosionDamage()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        animator.SetBool("Bounce", false);

        StartCoroutine((BombExplosion(0f)));
    }

    private void StopChase()
    {
        agent.ResetPath();
        canChase = false;
    }
    private void StartChase()
    {
        canChase = true;
    }
    //Bomb Explosion CoRoutine
    private IEnumerator BombExplosion(float delay)
    {

        Debug.Log("Explodes: " + this);

        yield return new WaitForSeconds(0.1f);

        Debug.Log("Disable bomb: " + this);

    }
    public float GetDistanceToPlayer()
    {
        //if (playerTransform == null) return 0f;
        return Vector2.Distance(transform.position, playerTransform.position);
    }
    private void DisableBomb()
    {

        transform.position = originalPosition;
        gameObject.SetActive(false); //Disable to enable again if needed
    }

}