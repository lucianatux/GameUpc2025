using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemiesEventsManager centraliza los eventos relacionados con enemigos y jefes.
/// Usa el patrón Singleton para permitir que otros scripts accedan fácilmente a sus eventos.
/// Otros scripts pueden suscribirse a estos eventos para reaccionar (como reproducir sonidos o animaciones).
/// </summary>

public class EnemiesEventsManager : MonoBehaviour
{
    public static EnemiesEventsManager Instance { get; private set; }
    // Eventos que otros pueden escuchar
    public event Action OnEnemyDefeated;
    public event Action OnEnemyDamaged;
    public event Action OnEnemySwordAttack;
    public event Action OnEnemySodaAttack;
    public event Action OnBossCherryBombs;
    public event Action OnBossCharge;
    public event Action OnBossDefeated;
    public event Action OnBossDamaged;

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
    // Enemy and boss methods
    public void EnemyDefeated() => OnEnemyDefeated?.Invoke();
    public void EnemyDamaged() => OnEnemyDamaged?.Invoke();
    public void EnemySwordAttack() => OnEnemySwordAttack?.Invoke();
    public void EnemySodaAttack() => OnEnemySodaAttack?.Invoke();
    public void BossCherryBombs() => OnBossCherryBombs?.Invoke();
    public void BossCharge() => OnBossCharge?.Invoke();
    public void BossDamaged() => OnBossDamaged?.Invoke();
    public void BossDefeated() => OnBossDefeated?.Invoke();
}