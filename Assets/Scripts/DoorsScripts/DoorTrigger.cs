using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerInventory>(out var inventory))
            return;

        Door parentDoor = GetComponentInParent<Door>();
        if (parentDoor != null)
        {
            parentDoor.TryOpen(inventory);
        }
    }
}

