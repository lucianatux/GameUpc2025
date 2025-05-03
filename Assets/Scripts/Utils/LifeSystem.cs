using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float invulnerabilityTime;
    private float invulnerabilityTimer;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        invulnerabilityTimer -= Time.deltaTime;
    }
    
    public void TakeDamage(int damage)
    {
        if (invulnerabilityTimer >= 0) return;
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        invulnerabilityTimer = invulnerabilityTime;
        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 0;
        }
        
    }


    
    private void Die()
    {
        if (currentHealth >= 0) return;
    }

    public void Heal(int healAmount)
    {
        if (currentHealth >= maxHealth) return;
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

}
