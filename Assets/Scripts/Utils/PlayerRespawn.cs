using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 _currentCheckpoint;
    private Vector3 _initialSpawn;
    AbilityController abilityController;
    private void Start()
    {
        // Guardamos el primer spawn (inicio del nivel)
        _initialSpawn = GameObject.FindWithTag("SpawnPoint").transform.position;
        transform.position = _initialSpawn;
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
            respawnPoint = _initialSpawn;
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

        PlayerInputHandler playerInputHandler = GetComponent<PlayerInputHandler>();
        if (playerInputHandler != null) playerInputHandler.enabled = true;

        AbilityController abilityController = GetComponent<AbilityController>();
        if (abilityController != null)
        {
            abilityController.enabled = true;
        }

        PlayerAnimatorController playerAnimatorController = GetComponent<PlayerAnimatorController>();
        if (playerAnimatorController != null) playerAnimatorController.enabled = true;

        Debug.Log("Jugador respawneado.");


        Animator animator = GetComponent<Animator>();
        if (animator != null) animator.SetBool("isDead", false);

        PlayerEventsManager.Instance?.PlayerHeal(); // ← Esto fuerza el update de la barra de vida
        } 
        
}
