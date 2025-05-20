using UnityEngine;

public class ItemKey : MonoBehaviour
{
    [SerializeField] private Key _key;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerInventory>(out var inventory))
            return;

        inventory.AddKey(_key);
        Destroy(gameObject);
    }
}