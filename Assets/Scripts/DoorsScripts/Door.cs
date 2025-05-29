using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private List<Key> requiredKeys;
    [SerializeField] private GameObject doorVisual;
    private Collider2D physicalCollider;
    private bool isOpen = false;

    private void Awake()
    {
        physicalCollider = GetComponent<Collider2D>();
    }

    public void TryOpen(PlayerInventory inventory) // Called when an attempt is made to open the door using the player's inventory.
    {
         if (isOpen)
         {
             Debug.Log("La puerta ya está abierta.");  // If the door is already open, exit early.
             return;
         }
         if (requiredKeys == null || requiredKeys.Count == 0)
         {
             Debug.LogWarning("No hay llaves requeridas configuradas en la puerta."); // If no keys are configured, warn and exit.
             return;
         }
               
         if (requiredKeys.Any(key => !inventory.HasKey(key.id)))   // Check if the inventory is missing any required key.
         {
             Debug.Log("¡Te faltan llaves para abrir esta puerta!");
             return;
         }
         foreach (var key in requiredKeys)
         {
             if (!inventory.HasKey(key.id))
             {
                 Debug.Log("Falta la llave: " + key.displayName);
                 return;
             }
         }

         Debug.Log("Todas las llaves presentes. Abriendo puerta.");
         OpenDoor();
    }

    private void OpenDoor()  //Opens door once all keys are collected
    {
        Debug.Log("Puerta abierta con llaves: " + string.Join(", ", requiredKeys.Select(k => k.displayName)));
        isOpen = true;   // Mark the door as open.

        if (doorVisual != null)
            doorVisual.SetActive(false);

        if (physicalCollider != null)  // Disable the physical collider so the player can walk through.
            physicalCollider.enabled = false;
    }
}
