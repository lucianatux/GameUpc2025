using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string requiredKeyID;
    [SerializeField] private GameObject doorVisual;
    private Collider2D physicalCollider;
    private bool isOpen = false;

    private void Awake()
    {
        physicalCollider = GetComponent<Collider2D>();
    }

    public void TryOpen(PlayerInventory inventory)
    {
        if (isOpen) return;

        if (!inventory.HasKey(requiredKeyID))
        {
            Debug.Log("¡No tenés la llave para esta puerta!");
            return;
        }

        OpenDoor();
    }

    private void OpenDoor()
    {
        Debug.Log("Puerta abierta con llave: " + requiredKeyID);
        isOpen = true;

        if (doorVisual != null)
            doorVisual.SetActive(false);

        if (physicalCollider != null)
            physicalCollider.enabled = false;
    }
}