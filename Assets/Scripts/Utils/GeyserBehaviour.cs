using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the visual and functional behavior of a geyser that can be charged and triggered.
/// Still In Testing
/// </summary>
public class GeyserBehaviour : MonoBehaviour
{
    [Header("Charge Settings")]
    [SerializeField] private float chargingCooldownMin = 5f;
    [SerializeField] private float chargingCooldownMax = 10f;
    private float chargingTimer;
    private bool isCharged;

    [Header("Eruption Settings")]
    [SerializeField] private float eruptionDuration;
    private bool eruptionEnded;

    [Header("Hits")]
    [SerializeField] private int currentHits;

    [Header("Visual States")]
    public GameObject geyserNotCharged;
    public GameObject geyserZeroHits;
    public GameObject geyserOneHit;
    public GameObject geyserTwoHits;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    void Start()
    {
        currentHits = 0;
        eruptionEnded = false;
        isCharged = false;

        if (animator == null) Debug.LogError("Animator not assigned on GeyserBehaviour.");

        // Start initial charge countdown
        chargingTimer = Random.Range(chargingCooldownMin, chargingCooldownMax);
    }

    void Update()
    {
        HandleCharging();
        UpdateVisuals();

        if (isCharged && Input.GetKeyDown(KeyCode.K))
        {
            currentHits++;
            animator.SetInteger("currentHits", currentHits);
        }
    }

    /// <summary>
    /// Handles the charging logic with a countdown.
    /// </summary>
    private void HandleCharging()
    {
        if (isCharged) return;

        chargingTimer -= Time.deltaTime;

        if (chargingTimer <= 0)
        {
            isCharged = true;
            animator.SetBool("isCharged", true);
            Debug.Log("Geyser is now charged.");
        }
        else
        {
            Debug.Log("Charging... time left: " + chargingTimer.ToString("F2"));
        }
    }

    /// <summary>
    /// Updates the VFX depending on the current hits and charge state.
    /// </summary>
    private void UpdateVisuals()
    {
        // Not charged state
        if (currentHits == 0 && !isCharged)
        {
            geyserNotCharged?.SetActive(true);
            geyserZeroHits?.SetActive(false);
            geyserOneHit?.SetActive(false);
            geyserTwoHits?.SetActive(false);
        }

        // Charged but not hit
        if (currentHits == 0 && isCharged)
        {
            geyserNotCharged?.SetActive(false);
            geyserZeroHits?.SetActive(true);
        }

        // First hit
        if (currentHits == 1)
        {
            geyserZeroHits?.SetActive(false);
            geyserOneHit?.SetActive(true);
        }

        // Second hit triggers eruption
        if (currentHits == 2)
        {
            geyserOneHit?.SetActive(false);
            StartCoroutine(Eruption());
        }
    }

    /// <summary>
    /// Handles the eruption sequence after two hits.
    /// </summary>
    private IEnumerator Eruption()
    {
        Debug.Log("Eruption begins!");
        geyserTwoHits?.SetActive(true);

        yield return new WaitForSeconds(eruptionDuration);

        geyserTwoHits?.SetActive(false);
        eruptionEnded = true;
        animator.SetBool("eruptionEnded", eruptionEnded);

        // Reset geyser state
        currentHits = 0;
        isCharged = false;
        animator.SetBool("isCharged", false);
        chargingTimer = Random.Range(chargingCooldownMin, chargingCooldownMax);

        Debug.Log("Eruption ends. Geyser resets.");
    }
}