using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKeys playerKeys = other.GetComponent<PlayerKeys>();
            if (playerKeys != null)
            {
                playerKeys.PickUpKey(keyID);
                Destroy(gameObject);
            }
        }
    }
}
