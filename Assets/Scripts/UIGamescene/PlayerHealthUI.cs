using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Configuración")]
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        if (PlayerEventsManager.Instance != null)
        {
            var p = PlayerEventsManager.Instance;
            p.OnPlayerDamaged += UpdateHealthBar;
            p.OnPlayerHeal += UpdateHealthBar;
            p.OnPlayerDeath += OnPlayerDeath;
        }

        if (playerHealth == null)
            Debug.LogError("PlayerHealth no asignado en PlayerHealthUI");

        UpdateHealthBar(); // Inicializar valor correcto al inicio
    }

    private void OnDestroy()
    {
        if (PlayerEventsManager.Instance != null)
        {
            var p = PlayerEventsManager.Instance;
            p.OnPlayerDamaged -= UpdateHealthBar;
            p.OnPlayerHeal -= UpdateHealthBar;
            p.OnPlayerDeath -= OnPlayerDeath;
        }
    }

    private void UpdateHealthBar()
    {
        if (playerHealth == null) return;

        float current = playerHealth.CurrentHealth;
        float max = playerHealth.MaxHealth;

        float normalized = Mathf.Clamp01(current / max);

        if (healthSlider != null)
        {
            healthSlider.value = normalized;
        }
        Debug.Log($"[UI] Actualizando barra. Vida: {current}/{max} = {normalized}");
    }

    private void OnPlayerDeath()
    {
        Debug.Log("El jugador murió");
        
    }
}
