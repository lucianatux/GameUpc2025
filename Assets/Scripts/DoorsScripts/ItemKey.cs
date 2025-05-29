using UnityEngine;

public class ItemKey : MonoBehaviour   // key item in the game world that the player can pick up.
{
    [SerializeField] private Key _key;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerInventory>(out var inventory))     // Try to get a PlayerInventory component from the object that entered the trigger.
                                                                           // If it doesn't have one (i.e., it's not the player), do nothing.
            return;

        inventory.AddKey(_key);    // If it *is* the player, add the key to the player's inventory.
        // Key collected event
        EnvironmentEventsManager.Instance?.KeyCollected();
        Destroy(gameObject);
    }
}