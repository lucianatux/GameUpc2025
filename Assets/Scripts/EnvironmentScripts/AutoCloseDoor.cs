using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCloseDoor : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Animator doorAnimator;

    private BoxCollider2D solidCollider;
    private bool alreadyClosed = false;

    private void Awake()
    {
        // Busca el BoxCollider2D no trigger en este GameObject
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                solidCollider = col;
                solidCollider.enabled = false; // Asegura que esté desactivado al inicio
                Debug.Log("[Puerta] Collider sólido encontrado y desactivado.");
                break;
            }
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
        if (alreadyClosed) return;
        Debug.Log("[Puerta] Algo entró al trigger: " + other.name);

        if (other.CompareTag(playerTag))
        {
            Debug.Log("[Puerta] Jugador detectado. Cerrando puerta...");
            CloseDoor();
        }
    }

    private void CloseDoor()
    {
        if (doorAnimator != null)
        {
            Debug.Log("[Puerta] Trigger de animación enviado.");
            doorAnimator.SetTrigger("Close");
            EnvironmentEventsManager.Instance?.DoorClose(); 

        }
        else
        {
            Debug.LogWarning("[Puerta] Animator no asignado.");
        }

        if (solidCollider != null)
        {
            solidCollider.enabled = true;
            Debug.Log("[Puerta] Collider sólido activado.");
        }

        alreadyClosed = true;
    }

    private void ResetDoor()
    {
        Debug.Log("[Puerta] Reiniciando estado (por muerte del jugador)");

        // Volver al estado inicial (abierta)
        if (doorAnimator != null)
        {
            doorAnimator.ResetTrigger("Close");
            doorAnimator.Play("sideDoorOpen", 0); 
        }

        if (solidCollider != null)
            solidCollider.enabled = false;

        alreadyClosed = false;
    }
}