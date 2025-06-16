using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnvironmentEventsManager centraliza los eventos relacionados con el entorno.
/// Usa el patrón Singleton para permitir que otros scripts accedan fácilmente a sus eventos.
/// Otros scripts pueden suscribirse a estos eventos para reaccionar (como reproducir sonidos o animaciones).
/// </summary>
/// 
public class EnvironmentEventsManager : MonoBehaviour
{
    public static EnvironmentEventsManager Instance { get; private set; }
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
    public event Action OnEndPlayground;
    public event Action OnEndMiddlePart;

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
    public void EndPlayground() => OnEndPlayground?.Invoke();
    public void EndMiddlePart() => OnEndMiddlePart?.Invoke();
}