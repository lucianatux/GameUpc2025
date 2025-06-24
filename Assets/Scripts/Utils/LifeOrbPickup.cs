using UnityEngine;

/// <summary>
/// Heals the player by a specific amount if not at full health. 
/// Only destroys itself after healing.
/// </summary>
public class LifeOrbPickup : MonoBehaviour
{
    [Tooltip("Amount of health to restore to the player.")]
    public int healAmount = 25;

    [Tooltip("Optional effect prefab to instantiate when healing.")]
    public GameObject healEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            int currentHealth = playerHealth.CurrentHealth;
            int maxHealth = playerHealth.MaxHealth;

            // Solo cura si el jugador tiene menos que la vida máxima
            if (currentHealth < maxHealth)
            {
                int amountToHeal = Mathf.Min(healAmount, maxHealth - currentHealth);
                playerHealth.Heal(amountToHeal);

                // Instancia efecto si existe
                if (healEffect != null)
                {
                    
                    Vector3 spawnPos = other.transform.position + new Vector3(0, -0.6f, 0);
                    Instantiate(healEffect, spawnPos, Quaternion.identity);

                }

                Destroy(gameObject); // Solo se destruye si curó algo
            }
        }
    }
}