using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the behavior of a bomb: falling animation, enabling collision, fall damage,
/// explosion, and deactivation.
/// IN TESTING YET
/// </summary>
public class BombScript : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;

    [SerializeField] private int damage;
    private Vector3 originalPosition;
    private PlayerHealth PlayerHealth;
    [Tooltip("Prefab instantiated when the bomb hits the ground.")]
    [SerializeField] private GameObject fallPrefab;

    [Tooltip("Prefab instantiated when the bomb explodes.")]
    [SerializeField] private GameObject explosionPrefab;

    //Set references and components
    private void Awake()
    {
        //get original position
        //originalPosition = transform.position;
        /*
        rb = GetComponent<Rigidbody2D>();

        if (rb == null) Debug.LogError(this + " : Rigidbody2D not found");

        col = GetComponent<Collider2D>();

        if (col == null)
        {
            Debug.LogError(this + " : Collider2D not found");
        }
        else
        {
            col.enabled = false;
        }
        */
    }

    //Called when BossAI enables it
    private void OnEnable()
    {
        originalPosition = transform.position;
        // Reset position and start the fall animation
        //transform.position = originalPosition;
        //StartCoroutine(BombFall(1f));
    }

    void Update()
    {
        transform.position = originalPosition;
    }
    //Bomb Falling CoRoutine

    //methods called in animator
    private void FallingDamage()
    {

        Instantiate(fallPrefab, originalPosition, Quaternion.identity);

    }

    private void ExplosionDamage()
    {
        Instantiate(explosionPrefab, originalPosition, Quaternion.identity);
        StartCoroutine((BombExplosion(0f)));
    }



    //Bomb Explosion CoRoutine
    private IEnumerator BombExplosion(float delay)
    {

        Debug.Log("Explodes: " + this);

        yield return new WaitForSeconds(0.1f);

        Debug.Log("Disable bomb: " + this);

        transform.position = originalPosition;
        gameObject.SetActive(false); //Disable to enable again if needed
    }

}