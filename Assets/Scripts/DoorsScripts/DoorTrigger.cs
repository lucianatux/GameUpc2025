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
            if (parentDoor.RequiresKeys())
                parentDoor.TryOpen(inventory);
            else
                Debug.Log("This door doesn't require keys. RoomManager controls this door.");
        }
        else
        {
            Debug.LogError("No parent Door found for DoorTrigger attached to: " + gameObject.name);
        }
    }
}

