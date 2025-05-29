using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerInventory>(out var inventory)) // Checks if player is colliding 
            return;

        Door parentDoor = GetComponentInParent<Door>(); // Verify if there is a Door parent
        if (parentDoor != null)
        {
            parentDoor.TryOpen(inventory);   // Use Method "TryOpen" to open the Door
        }
    }
}

