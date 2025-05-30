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
    }

    //Called when BossAI enables it
    private void OnEnable()
    {
        originalPosition = transform.position;

        // Reset position and start the fall animation
        transform.position = originalPosition;
        StartCoroutine(BombFall(1f));
    }

    //Bomb Falling CoRoutine
    private IEnumerator BombFall(float delay)
    {
        // Falling cherry animation
        yield return new WaitForSeconds(delay);
        col.enabled = true;
        Debug.Log("Falls: " + this);

        if (fallPrefab != null)
        {
            Debug.Log("Instantiate fall damage: " + this);
            Instantiate(fallPrefab, originalPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Fall prefab is not assigned.");
        }

        // Start explosion after fall
        StartCoroutine(BombExplosion(1f));
    }

    //Bomb Explosion CoRoutine
    private IEnumerator BombExplosion(float delay)
    {
        // Wait before exploding
        //Before Explosion Animation
        yield return new WaitForSeconds(delay);
        Debug.Log("Explodes: " + this);
        //Bomb Explosion animation

        if (explosionPrefab != null)
        {
            Debug.Log("Instantiate explosion: " + this);
            Instantiate(explosionPrefab, originalPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Explosion prefab is not assigned.");
        }

        yield return new WaitForSeconds(0.1f);

        Debug.Log("Disable bomb: " + this);

        gameObject.SetActive(false); //Disable to enable again if needed
    }

}