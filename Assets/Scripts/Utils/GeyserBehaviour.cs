using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the visual and functional behavior of a geyser that can be charged and triggered.
/// Still in testing.
/// </summary>
public class GeyserBehaviour : MonoBehaviour
{
    // =========================
    //        VARIABLES
    // =========================

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

    [Header("References")]
    [SerializeField] private Animator animator;
    private Collider2D col;

    // Detector de impacto por proyectil (se reinicia cada frame)
    private bool gotHitThisFrame = false;

    // =========================
    //         MÉTODOS
    // =========================

    void Start()
    {
        // Inicializa estados
        currentHits = 0;
        eruptionEnded = false;
        isCharged = false;

        // Obtiene el collider del geyser
        col = GetComponent<Collider2D>();
        if (col == null) Debug.LogError("Collider2D not assigned on GeyserBehaviour.");

        // Verifica referencia al animator
        if (animator == null) Debug.LogError("Animator not assigned on GeyserBehaviour.");

        // Comienza la cuenta regresiva aleatoria para cargarse
        chargingTimer = Random.Range(chargingCooldownMin, chargingCooldownMax);
    }

    void Update()
    {
        HandleCharging();      // Maneja la carga del géiser
        UpdateVisuals();       // Actualiza visuales según estado

        // Si está cargado y recibió un impacto, suma un hit
        if (isCharged && GetHit())
        {
            currentHits++;
            animator.SetInteger("currentHits", currentHits);
        }
    }

    void LateUpdate()
    {
        // Reinicia el detector de impacto para el próximo frame
        gotHitThisFrame = false;
    }

    /// <summary>
    /// Maneja la lógica de carga del géiser con un temporizador.
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
    /// Actualiza el estado visual según la cantidad de impactos y si está cargado.
    /// </summary>
    private void UpdateVisuals()
    {
        // No cargado y sin golpes
        if (currentHits == 0 && !isCharged)
        {
            geyserNotCharged?.SetActive(true);
            geyserZeroHits?.SetActive(false);
            geyserOneHit?.SetActive(false);
            geyserTwoHits?.SetActive(false);
        }

        // Cargado pero sin golpes
        if (currentHits == 0 && isCharged)
        {
            geyserNotCharged?.SetActive(false);
            geyserZeroHits?.SetActive(true);
        }

        // Primer golpe recibido
        if (currentHits == 1)
        {
            geyserZeroHits?.SetActive(false);
            geyserOneHit?.SetActive(true);
        }

        // Segundo golpe recibido: comienza erupción
        if (currentHits == 2)
        {
            geyserOneHit?.SetActive(false);
            StartCoroutine(Eruption());
        }
    }

    /// <summary>
    /// Maneja la secuencia de erupción tras recibir dos golpes.
    /// </summary>
    private IEnumerator Eruption()
    {
        Debug.Log("Eruption begins!");
        geyserTwoHits?.SetActive(true);

        yield return new WaitForSeconds(eruptionDuration);

        geyserTwoHits?.SetActive(false);
        eruptionEnded = true;
        animator.SetBool("eruptionEnded", eruptionEnded);

        // Reinicia el estado del géiser
        currentHits = 0;
        isCharged = false;
        animator.SetBool("isCharged", false);
        chargingTimer = Random.Range(chargingCooldownMin, chargingCooldownMax);

        Debug.Log("Eruption ends. Geyser resets.");
    }

    /// <summary>
    /// Detecta colisiones con objetos de la capa "Projectile".
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            gotHitThisFrame = true;

            // Opcional: destruir el proyectil al impactar
            // Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Devuelve true si el géiser fue golpeado en este frame.
    /// </summary>
    public bool GetHit()
    {
        return gotHitThisFrame;
    }
}
