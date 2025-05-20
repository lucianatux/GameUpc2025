using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEventsManager : MonoBehaviour
{
    public static EnvironmentEventsManager Instance;
    // Eventos que otros pueden escuchar
    public event Action OnKeyCollected;
    public event Action OnKeyUsed;
    public event Action OnPepperCollected;
    public event Action OnTeratomaInteract;
    public event Action OnDoorClose;
    public event Action OnDoorOpen;
    public event Action OnLevelComplete;
    public event Action OnGeyserErupt;    
    public event Action OnAcidRiverFlow;
    public event Action OnAcidRiverSplash;

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
    public void GeyserErupt() => OnGeyserErupt?.Invoke();
    public void AcidRiverSplash() => OnAcidRiverSplash?.Invoke();
    public void AcidRiverFlow() => OnAcidRiverFlow?.Invoke();
    public void KeyCollected() => OnKeyCollected?.Invoke();
    public void KeyUsed() => OnKeyUsed?.Invoke();
    public void PepperCollected() => OnPepperCollected?.Invoke();
    public void TeratomaInteract() => OnTeratomaInteract?.Invoke();
    public void DoorClose() => OnDoorClose?.Invoke();
    public void DoorOpen() => OnDoorOpen?.Invoke();
    public void LevelComplete() => OnLevelComplete?.Invoke();
}