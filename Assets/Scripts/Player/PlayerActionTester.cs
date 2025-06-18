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
    [SerializeField] private PlayerHealth playerHealth;


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
    }
}

