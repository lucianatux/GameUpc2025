using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Temporary script for testing player animations and events with key presses.
/// Not intended for production use.
/// </summary>

public class PlayerActionTester : MonoBehaviour
{
    [SerializeField] private PlayerAnimatorController animatorController;

    void Update()
    {
        if (animatorController == null)
        {
            Debug.LogWarning("PlayerAnimatorController no asignado en PlayerActionTester.");
            return;
        }
        // Teclas para testeo rápido de animaciones y eventos.
        if (Input.GetKeyDown(KeyCode.C))
        {
            animatorController.TriggerAnim("croak");
            PlayerEventsManager.Instance.PlayerCroak(); 
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animatorController.TriggerAnim("damage");
            PlayerEventsManager.Instance.PlayerDamaged();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animatorController.TriggerAnim("die");
            PlayerEventsManager.Instance.PlayerDamaged(); // o PlayerDied(); según implementación
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animatorController.TriggerAnim("heal");
            PlayerEventsManager.Instance.PlayerHeal();
        }
    }
}

