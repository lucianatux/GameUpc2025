using UnityEngine;

public class ItemKey : MonoBehaviour   // key item in the game world that the player can pick up.
{
    [SerializeField] private Key _key;
    private bool _collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_collected) return;

        if (!other.TryGetComponent<PlayerInventory>(out var inventory))
            return;

        _collected = true; // Marcar como recogida para evitar múltiples llamadas

        inventory.AddKey(_key);
        EnvironmentEventsManager.Instance?.KeyCollected();
        Destroy(gameObject);
    }
}