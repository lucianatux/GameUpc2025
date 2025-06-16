using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Animator doorAnimator;

    private BoxCollider2D solidCollider;
    private bool alreadyOpened = false;

    private void Awake()
    {
        // Busca el BoxCollider2D no trigger en este GameObject
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                solidCollider = col;
                solidCollider.enabled = true; // Asegura que esté activado al inicio
                Debug.Log("[Puerta] Collider sólido encontrado y ACTIVADO al inicio.");
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyOpened) return;

        if (other.CompareTag(playerTag))
        {
            Debug.Log("[Puerta] Jugador detectado. Abriendo puerta...");
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
        else
        {
            Debug.LogWarning("[Puerta] Animator no asignado.");
        }

        if (solidCollider != null)
        {
            solidCollider.enabled = false;
            Debug.Log("[Puerta] Collider sólido DESACTIVADO (puerta abierta).");
        }

        alreadyOpened = true;
    }
}