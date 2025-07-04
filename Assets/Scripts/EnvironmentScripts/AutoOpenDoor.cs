using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Animator doorAnimator;

    [Header("Colliders hijos")]
    [SerializeField] private Collider2D triggerCollider;     // Asignar el CapsuleCollider2D (isTrigger = true)
    [SerializeField] private Collider2D solidCollider;       // Asignar el BoxCollider2D (isTrigger = false)

    private bool alreadyOpened = false;

    private void Awake()
    {
        if (solidCollider != null)
        {
            solidCollider.enabled = true;
            Debug.Log("[Puerta] Collider sólido ACTIVADO al inicio.");
        }
        else
        {
            Debug.LogError("[Puerta] No se asignó el collider sólido.");
        }

        if (triggerCollider != null && !triggerCollider.isTrigger)
        {
            Debug.LogWarning("[Puerta] El collider de trigger no está marcado como isTrigger.");
        }
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += ResetDoor;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= ResetDoor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyOpened) return;

        // Solo reaccionamos si este trigger es el que asignamos
        if (triggerCollider != null && other.CompareTag(playerTag))
        {
            Debug.Log("[Puerta] Jugador entró en trigger. Abriendo puerta...");
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
            Debug.Log("[Puerta] Trigger 'Open' enviado al Animator.");
        }

        if (solidCollider != null)
        {
            solidCollider.enabled = false;
            Debug.Log("[Puerta] Collider sólido DESACTIVADO (puerta abierta).");
        }

        alreadyOpened = true;
    }

    private void ResetDoor()
    {
        Debug.Log("[Puerta] Reiniciando estado (por muerte del jugador)");

        if (doorAnimator != null)
        {
            doorAnimator.ResetTrigger("Open");
            doorAnimator.Play("frontDoorClosed", 0); 
        }

        if (solidCollider != null)
            solidCollider.enabled = true;

        alreadyOpened = false;
    }
}
