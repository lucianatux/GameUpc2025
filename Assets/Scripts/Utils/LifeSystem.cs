using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LifeSystem : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected float invulnerabilityTime;
    protected float invulnerabilityTimer;
    [SerializeField] protected bool canHeal = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        invulnerabilityTimer -= Time.deltaTime;
    }

    public virtual void TakeDamage(int damage)
    {
        if (invulnerabilityTimer > 0 || currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} recibe {damage} de daño. Vida restante: {currentHealth}");

        invulnerabilityTimer = invulnerabilityTime;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        StartCoroutine(WaitAndDestroy(1.5f)); // o animación.length si lo calculás
    }

    private IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public virtual void Heal(int healAmount)
    {
        if (!canHeal || currentHealth >= maxHealth) return;

        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log($"{gameObject.name} se cura. Vida actual: {currentHealth}");
    }
}

