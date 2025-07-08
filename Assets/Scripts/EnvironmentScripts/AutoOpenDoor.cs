using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Animator doorAnimator;

    private BoxCollider2D solidCollider;
    private bool alreadyOpened = false;

    private void Awake()
    {
        // Encuentra el collider sólido (no trigger)
        foreach (var col in GetComponents<BoxCollider2D>())
        {
            if (!col.isTrigger)
            {
                solidCollider = col;
                solidCollider.enabled = true;
                Debug.Log("[AutoOpenDoor] Collider sólido activado al inicio.");
                break;
            }
        }
    }

    public void TryOpen()
    {
        if (alreadyOpened) return;

        Debug.Log("[AutoOpenDoor] Abrir puerta solicitado por trigger.");
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
            EnvironmentEventsManager.Instance?.DoorOpen(); 
            Debug.Log("[AutoOpenDoor] Animación 'Open' disparada.");
        }

        if (solidCollider != null)
        {
            solidCollider.enabled = false;
            Debug.Log("[AutoOpenDoor] Collider sólido desactivado.");
        }

        alreadyOpened = true;
    }

    public void ResetDoor()
    {
        return;
        Debug.Log("[AutoOpenDoor] Reiniciando estado de la puerta.");

        if (doorAnimator != null)
        {
            doorAnimator.ResetTrigger("Open");
            doorAnimator.Play("sideDoorClosed", 0);
        }

        if (solidCollider != null)
            solidCollider.enabled = true;

        alreadyOpened = false;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += ResetDoor;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= ResetDoor;
    }
}
