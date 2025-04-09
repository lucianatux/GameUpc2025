using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredKeyID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKeys playerKeys = other.GetComponent<PlayerKeys>();
            if (playerKeys != null && playerKeys.HasKey(requiredKeyID))
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Puerta abierta con llave: " + requiredKeyID);
        gameObject.SetActive(false); // o animación, sonido, etc.
    }
}
