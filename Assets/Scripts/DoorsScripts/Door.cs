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
             Debug.Log("Door is already open");  // If the door is already open, exit early.
             return;
         }

         if (requiredKeys == null || requiredKeys.Count == 0)
         {
             Debug.LogWarning("No keys are required to be set to the door."); // If no keys are configured, warn and exit.
             return;
         }
               
         if (requiredKeys.Any(key => !inventory.HasKey(key.id)))   // Check if the inventory is missing any required key.
         {
             Debug.Log("You're missing keys to open this door!");
             return;
         }

         Debug.Log("All keys present. Unlocking door.");
         OpenDoor();
    }

    private void OpenDoor()  //Opens door once all keys are collected
    {
        Debug.Log("Door open with keys:" + string.Join(", ", requiredKeys.Select(k => k.displayName)));
        isOpen = true;   // Mark the door as open.

        if (doorVisual != null)
            doorVisual.SetActive(false);

        if (physicalCollider != null)  // Disable the physical collider so the player can walk through.
            physicalCollider.enabled = false;

        // Door Open event
        EnvironmentEventsManager.Instance?.DoorOpen();
    }
}
