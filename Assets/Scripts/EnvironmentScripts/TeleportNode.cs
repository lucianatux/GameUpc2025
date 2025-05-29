using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    [SerializeField] private TeleportNode linkedNode; // Nodo de destino
    [SerializeField] private float teleportCooldown = 2f; // Tiempo para evitar loops
    
    private bool isOnCooldown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || isOnCooldown)
            return;

        if (linkedNode == null)
        {
            Debug.LogWarning("Linked node is null, teleport aborted.");
            return;
        }
        
        // Teletransportar al jugador
        StartCoroutine(Teleport(other.transform));
    }

    private System.Collections.IEnumerator Teleport(Transform player)
    {
        // Evita loops entre nodos
        isOnCooldown = true;
        linkedNode.SetCooldown(true);

        // Mover al jugador
        player.position = linkedNode.transform.position;

        yield return new WaitForSeconds(teleportCooldown);

        isOnCooldown = false;
        linkedNode.SetCooldown(false);
    }

    public void SetCooldown(bool value)
    {
        isOnCooldown = value;
    }
}
