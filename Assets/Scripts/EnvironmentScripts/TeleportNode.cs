using System.Collections;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    [SerializeField] private TeleportNode linkedNode; // Nodo de destino
    [SerializeField] private float teleportCooldown = 2f; // Tiempo para evitar loops

    private bool isOnCooldown = false;
    private bool isTeleporting = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || isOnCooldown || isTeleporting)
            return;

        if (linkedNode == null)
        {
            Debug.LogWarning("Linked node is null, teleport aborted.");
            return;
        }

        StartCoroutine(Teleport(other.transform));
    }

    private IEnumerator Teleport(Transform player)
    {
        isTeleporting = true;
        isOnCooldown = true;
        linkedNode.SetCooldown(true);
        linkedNode.SetTeleporting(true);

        // Teletransportar al jugador
        player.position = linkedNode.transform.position;

        // Esperar cooldown
        yield return new WaitForSeconds(teleportCooldown);

        isOnCooldown = false;
        isTeleporting = false;
        linkedNode.SetCooldown(false);
        linkedNode.SetTeleporting(false);
    }

    public void SetCooldown(bool value)
    {
        isOnCooldown = value;
    }

    public void SetTeleporting(bool value)
    {
        isTeleporting = value;
    }
}
