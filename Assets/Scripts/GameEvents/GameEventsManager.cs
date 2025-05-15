using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance;
   private void Awake()
    {
        Instance = this;
    }
    // Eventos que otros pueden escuchar
    // Player events
    public event Action OnPlayerDamaged;
    public event Action OnPlayerCroak;
    public event Action OnPlayerKick;
    public event Action OnPlayerFireball;
    public event Action OnPlayerHeal;
    public event Action OnPlayerDeath;
    // Enemy and boss events
    public event Action OnEnemyDefeated;
    public event Action OnEnemyDamaged;
    public event Action OnEnemySwordAttack;
    public event Action OnEnemySodaAttack;
    public event Action OnBossCherryBombs;
    public event Action OnBossCharge;
    public event Action OnBossDefeated;
    public event Action OnBossDamaged;
    // Item/Interaction events
    public event Action OnKeyCollected;
    public event Action OnKeyUsed;
    public event Action OnPepperCollected;
    public event Action OnTeratomaInteract;
    public event Action OnDoorClose;
    public event Action OnDoorOpen;
    public event Action OnLevelComplete;
    // Hazard events (géiser y ácido)
    public event Action OnGeyserErupt;    
    public event Action OnAcidRiverFlow;
    public event Action OnAcidRiverSplash;
   
   // ===== MÉTODOS PÚBLICOS (para invocar eventos) =====
    // Player methods
    public void PlayerDamaged() => OnPlayerDamaged?.Invoke();
    public void PlayerCroak() => OnPlayerCroak?.Invoke();
    public void PlayerKick() => OnPlayerKick?.Invoke();
    public void PlayerFireball() => OnPlayerFireball?.Invoke();
    public void PlayerHeal() => OnPlayerHeal?.Invoke();
    public void PlayerDeath() => OnPlayerDeath?.Invoke();

    // Enemy and boss methods
    public void EnemyDefeated() => OnEnemyDefeated?.Invoke();
    public void EnemyDamaged() => OnEnemyDamaged?.Invoke();
    public void EnemySwordAttack() => OnEnemySwordAttack?.Invoke();
    public void EnemySodaAttack() => OnEnemySodaAttack?.Invoke();
    public void BossCherryBombs() => OnBossCherryBombs?.Invoke();
    public void BossCharge() => OnBossCharge?.Invoke();
    public void BossDamaged() => OnBossDamaged?.Invoke();
    public void BossDefeated() => OnBossDefeated?.Invoke();

    // Hazard methods
    public void GeyserErupt() => OnGeyserErupt?.Invoke();
    public void AcidRiverSplash() => OnAcidRiverSplash?.Invoke();
    public void AcidRiverFlow() => OnAcidRiverFlow?.Invoke();

    // Item/Interaction methods
    public void KeyCollected() => OnKeyCollected?.Invoke();
    public void KeyUsed() => OnKeyUsed?.Invoke();
    public void PepperCollected() => OnPepperCollected?.Invoke();
    public void TeratomaInteract() => OnTeratomaInteract?.Invoke();
    public void DoorClose() => OnDoorClose?.Invoke();
    public void DoorOpen() => OnDoorOpen?.Invoke();
    public void LevelComplete() => OnLevelComplete?.Invoke();

}
