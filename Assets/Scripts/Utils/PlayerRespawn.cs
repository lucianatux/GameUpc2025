using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint;
    private Vector3 initialSpawn;

    private void Start()
    {
        // Guardamos el primer spawn (inicio del nivel)
        initialSpawn = GameObject.FindWithTag("SpawnPoint").transform.position;
        transform.position = initialSpawn;
    }
    public void Respawn()
    {
        Vector3 respawnPoint;

        if (RoomManager.Instance != null && RoomManager.Instance.LastCheckpointRoom != null)
        {
            respawnPoint = RoomManager.Instance.LastCheckpointRoom.transform.position;
        }
        else
        {
            respawnPoint = initialSpawn;
        }

        transform.position = respawnPoint;

        // Restauramos la vida al respawnear (opcional)
        LifeSystem life = GetComponent<LifeSystem>();
        if (life != null)
        {
            life.ResetHealth();
        }
        // Reactivar movimiento y físicas
        PlayerMovement move = GetComponent<PlayerMovement>();
        if (move != null)
            move.canMove = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;

        Debug.Log("Jugador respawneado.");

        PlayerEventsManager.Instance?.PlayerHeal(); // ← Esto fuerza el update de la barra de vida
        } 
        
}
