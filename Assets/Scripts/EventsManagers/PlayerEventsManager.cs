using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerEventsManager centraliza los eventos relacionados con el player.
/// Usa el patrón Singleton para permitir que otros scripts accedan fácilmente a sus eventos.
/// Otros scripts pueden suscribirse a estos eventos para reaccionar (como reproducir sonidos o animaciones).
/// </summary>

public class PlayerEventsManager : MonoBehaviour
{
    public static PlayerEventsManager Instance { get; private set; }
   
    // Eventos que otros pueden escuchar
    public event Action OnPlayerDamaged;
    public event Action OnPlayerCroak;
    public event Action OnPlayerKick;
    public event Action OnPlayerFireball;
    public event Action OnPlayerHeal;
    public event Action OnPlayerDeath;
  
    private void Awake()
    {
        // Asegura que solo haya una instancia de este manager en la escena (Singleton pattern)
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