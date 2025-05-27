using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
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
    }

    protected virtual void Update()
    {
        invulnerabilityTimer -= Time.deltaTime; // timer que cuenta en cuánto tiempo puede volver a recibir daño
    }

    // función pública a la que acceden los ataques, dando un parámetro daño que se va a recibir
    public virtual void TakeDamage(int damage)
    {
        if (invulnerabilityTimer > 0 || currentHealth <= 0) return; // si está invulnerable o con 0 o menos de vida, no recibe daño

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} recibe {damage} de daño. Vida restante: {currentHealth}");

        invulnerabilityTimer = invulnerabilityTime; // se resetea el tiempo de invulnerabilidad

        if (currentHealth <= 0) // si el ataque baja la vida a 0 o menos se llama a la función Die
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            if (animator != null) animator.SetTrigger("damage"); // se activa la animación de daño
        }
    }

    // protected porque es privada para otras clases, pública para clases hijas
    // virtual porque se puede modificar desde una clase hija
    protected virtual void Die()
    {
        if (animator != null) animator.SetTrigger("die"); // se activa la animación de muerte
        // No se destruye el objeto automáticamente. Las clases hijas pueden decidir cuándo destruirlo.
    }

    // se llama cuando se vaya a querer curar a la entidad
    public virtual void Heal(int healAmount)
    {
        if (!canHeal || currentHealth >= maxHealth) return; // no se puede curar si no es un objeto curable o si ya tiene vida máxima

        currentHealth += healAmount;
        Debug.Log($"{gameObject.name} se cura. Vida actual: {currentHealth}");

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    // corrutina opcional que puede usarse desde clases hijas para destruir el objeto luego de un delay
    protected IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

