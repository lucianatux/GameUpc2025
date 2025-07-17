using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern;

public class RoomManager : MonoBehaviour
{
    // === Singleton Pattern ===
    public static RoomManager Instance => _instance;
    private static RoomManager _instance;

    // === Delegates & Events ===
    public delegate void RoomEvent(int roomID);
    public event Action<int> OnRoomEntered;
    public event Action<int> OnRoomExited;
    public event Action<int> OnCallWaves;
    public event Action<int> OnRoomCleared;

    // === Room State ===
    private int _currentRoomID;
    public int CurrentRoomID => _currentRoomID;

    public Room currentRoom;
    public Room LastCheckpointRoom { get; private set; }

    public int enemyCount;
    public int currentEnemies;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogWarning("Multiple RoomManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Assigns the current room and handles wave, checkpoint and boss activation logic.
    /// </summary>
    public void SetCurrentRoom(Room newRoom)
    {
        if (newRoom == null)
        {
            Debug.LogError("Tried to assign a null room to RoomManager.");
            return;
        }

        currentRoom = newRoom;

        // Set initial wave
        if (currentRoom.currentWave == 0)
        {
            currentRoom.currentWave = 1;
        }

        Debug.Log($"Player entered room {newRoom.roomID}");
        Debug.Log($"Current wave: {newRoom.currentWave}");

        OnRoomEntered?.Invoke(newRoom.roomID);

        // === Contar enemigos vivos ===
        EnemyAI[] allEnemies = FindObjectsOfType<EnemyAI>();
        enemyCount = 0;

        foreach (var enemy in allEnemies)
        {
            if (enemy.RoomID == newRoom.roomID && enemy.isDead == false)
            {
                enemyCount++;
            }
        }

        Debug.Log($"Se encontraron {enemyCount} enemigos en la sala {newRoom.roomID}");
        currentEnemies = enemyCount;

        // === Si es checkpoint, lo guarda ===
        if (newRoom.isCheckpointRoom)
        {
            SetCheckpoint(newRoom);
        }

        // === Si es una sala de jefe, activar jefe ===
        if (newRoom.isBossRoom)
        {
            BossDeathHandler boss = FindObjectOfType<BossDeathHandler>();
            if (boss != null)
            {
                boss.ActivateBoss();
                Debug.Log("Boss activado desde RoomManager.");
            }
            else
            {
                Debug.LogWarning("No se encontró BossDeathHandler en la escena.");
            }
        }
    }

    /// <summary>
    /// Guarda la última sala de checkpoint alcanzada.
    /// </summary>
    public void SetCheckpoint(Room checkpointRoom)
    {
        LastCheckpointRoom = checkpointRoom;
        Debug.Log("Checkpoint actualizado: Room " + checkpointRoom.roomID);
    }

    /// <summary>
    /// Notifica que el jugador dejó la sala.
    /// </summary>
    public void OnPlayerLeftRoom(Room room)
    {
        if (currentRoom == room)
        {
            OnRoomExited?.Invoke(currentRoom.roomID);
            currentRoom = null;
            Debug.Log("Current room cleared because the player left.");
        }
    }

    /// <summary>
    /// Llama a la siguiente ola si no quedan enemigos vivos.
    /// </summary>
    public void UpdateWave()
    {
        if (currentEnemies <= 0)
        {
            Debug.Log("All enemies defeated. Advancing to next wave.");
            currentRoom.currentWave++;
            OnCallWaves?.Invoke(currentRoom.currentWave);
        }

        if (currentRoom.currentWave > currentRoom.maxWaves)
        {
            Debug.Log("All waves completed for room " + currentRoom.roomID);
            OnRoomCleared?.Invoke(currentRoom.roomID);
        }
    }

    /// <summary>
    /// Llamado por los enemigos al morir.
    /// </summary>
    public void NotifyEnemyDeath(int enemyRoomID)
    {
        currentEnemies--;
        Debug.Log($"NotifyEnemyDeath called. currentEnemies ahora es {currentEnemies}");

        if (currentRoom != null && enemyRoomID == currentRoom.roomID)
        {
            UpdateWave();
        }
    }

    /// <summary>
    /// Devuelve true si la sala está activa (tiene olas pendientes).
    /// </summary>
    public bool IsRoomActive(int roomID)
    {
        if (currentRoom != null && currentRoom.roomID == roomID)
        {
            return currentRoom.currentWave <= currentRoom.maxWaves;
        }

        return false;
    }
}
