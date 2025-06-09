using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema base de vida: gestiona daño, curación, muerte e invulnerabilidad.
/// Heredan enemigos, jugadores, etc.
/// </summary>
public class LifeSystem : MonoBehaviour
{
    [Header("Parámetros de vida")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float invulnerabilityTime;
    [SerializeField] protected bool canHeal = false;

    protected int currentHealth;
    protected float invulnerabilityTimer;

    protected Animator animator;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError($"{gameObject.name} no tiene un Animator asignado.");
        }
    }

    protected virtual void Update()
    {
        // Timer que cuenta en cuánto tiempo puede volver a recibir daño
        invulnerabilityTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Función pública a la que acceden los ataques, dando un parámetro de daño.
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        // Si está invulnerable o muerto, no recibe daño
        if (invulnerabilityTimer > 0 || currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} recibe {damage} de daño. Vida restante: {currentHealth}");

        // Se resetea el tiempo de invulnerabilidad
        invulnerabilityTimer = invulnerabilityTime;

        // Si la vida llega a 0, se llama a Die
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            if (animator != null) animator.SetTrigger("damage"); // Activa animación de daño
        }
    }

    /// <summary>
    /// Función virtual llamada al morir. Las clases hijas pueden extenderla.
    /// </summary>
    protected virtual void Die()
    {
        if (animator != null) animator.SetTrigger("die"); // Activa animación de muerte
        // No se destruye el objeto automáticamente. Las clases hijas pueden decidir cuándo destruirlo.
    }

    /// <summary>
    /// Se llama cuando se quiera curar a la entidad.
    /// </summary>
    public virtual void Heal(int healAmount)
    {
        // No se puede curar si no es curable o si ya tiene vida máxima
        if (!canHeal || currentHealth >= maxHealth) return;

        currentHealth += healAmount;
        Debug.Log($"{gameObject.name} se cura. Vida actual: {currentHealth}");

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    /// <summary>
    /// Corrutina opcional para destruir el objeto luego de un delay.
    /// </summary>
    protected IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}