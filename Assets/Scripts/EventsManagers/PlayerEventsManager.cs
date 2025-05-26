using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventsManager : MonoBehaviour
{
    public static PlayerEventsManager Instance;
   
    // Eventos que otros pueden escuchar
    public event Action OnPlayerDamaged;
    public event Action OnPlayerCroak;
    public event Action OnPlayerKick;
    public event Action OnPlayerFireball;
    public event Action OnPlayerHeal;
    public event Action OnPlayerDeath;
  
    private void Awake()
    {
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }
    Instance = this;
    }

   // ===== MÉTODOS PÚBLICOS (para invocar eventos) =====
    public void PlayerDamaged() => OnPlayerDamaged?.Invoke();
    public void PlayerCroak() => OnPlayerCroak?.Invoke();
    public void PlayerKick() => OnPlayerKick?.Invoke();
    public void PlayerFireball() => OnPlayerFireball?.Invoke();
    public void PlayerHeal() => OnPlayerHeal?.Invoke();
    public void PlayerDeath() => OnPlayerDeath?.Invoke();
}