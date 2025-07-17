using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject canvasObject;

    [Header("Visibility Settings")]
    [SerializeField] private bool hideWhenFull = true;
    [SerializeField] private float visibleDuration = 2f;

    private float hideTimer;

    /// <summary>
    /// Initializes the health bar with the maximum value.
    /// </summary>
    public void Initialize(float maxHealth)
    {
        if (healthSlider == null)
            Debug.LogError("HealthSlider not assigned on " + gameObject.name);

        healthSlider.minValue = 0f;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        if (canvasObject != null && hideWhenFull)
            canvasObject.SetActive(false);
    }

    /// <summary>
    /// Updates the slider's value and optionally shows the bar.
    /// </summary>
    public void SetHealth(float currentHealth)
    {
        healthSlider.value = currentHealth;

        if (canvasObject == null || !hideWhenFull)
            return;

        if (!canvasObject.activeSelf)
            canvasObject.SetActive(true);

        hideTimer = visibleDuration;
    }

    private void Update()
    {
        if (hideWhenFull && canvasObject != null && canvasObject.activeSelf)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
                canvasObject.SetActive(false);
        }
    }
}