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

    public void TryOpen(PlayerInventory inventory)
    {
         if (isOpen)
        {
            Debug.Log("La puerta ya está abierta.");
            return;
        }
        if (requiredKeys == null || requiredKeys.Count == 0)
        {
            Debug.LogWarning("No hay llaves requeridas configuradas en la puerta.");
            return;
        }
        // Verificamos si el jugador tiene TODAS las llaves necesarias
        if (requiredKeys.Any(key => !inventory.HasKey(key.id)))
        {
            Debug.Log("¡Te faltan llaves para abrir esta puerta!");
            return;
        }
        foreach (var key in requiredKeys)
        {
            if (!inventory.HasKey(key.id))
            {
                Debug.Log("Falta la llave: " + key.displayName);
                return;
            }
        }

        Debug.Log("Todas las llaves presentes. Abriendo puerta.");
        OpenDoor();
    }

    private void OpenDoor()
    {
        Debug.Log("Puerta abierta con llaves: " + string.Join(", ", requiredKeys.Select(k => k.displayName)));
        isOpen = true;

        if (doorVisual != null)
            doorVisual.SetActive(false);

        if (physicalCollider != null)
            physicalCollider.enabled = false;
    }
}
