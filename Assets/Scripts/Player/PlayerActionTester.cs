using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dispara acciones de prueba con teclas Z-X-C-V-B-N
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animatorController.TriggerAnim("fireball");
            PlayerEventsManager.Instance.PlayerFireball();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animatorController.TriggerAnim("kick");
            PlayerEventsManager.Instance.PlayerKick();
        }
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
            PlayerEventsManager.Instance.PlayerDamaged(); // o PlayerDied();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animatorController.TriggerAnim("heal");
            PlayerEventsManager.Instance.PlayerHeal();
        }
    }
}

