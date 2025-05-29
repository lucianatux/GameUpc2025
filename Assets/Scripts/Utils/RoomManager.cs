using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Singleton pattern for global access
    public static RoomManager Instance => _instance;
    private static RoomManager _instance;

    // Current active room data
    private int _currentRoomID;
    public int CurrentRoomID => _currentRoomID;

    public Room currentRoom;

    // Events triggered when player enters, exits, or advances a wave in a room
    public event Action<int> OnRoomEntered;
    public event Action<int> OnRoomExited;
    public event Action<int> OnCallWaves;

    // Enemy counters for the room
    public int enemyCount;
    public int currentEnemies;

    private void Awake()
    {
        // Singleton setup
        if (_instance == null)
        {
            _instance = this; //If there is no _instance it creates it 
        }
        else if (_instance != this)
        {
            Debug.LogWarning("Multiple RoomManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Assigns the current room and triggers the OnRoomEntered event.
    /// </summary>
    /// <param name="newRoom">The room the player is entering.</param>
    public void SetCurrentRoom(Room newRoom)
    {
        if (newRoom == null)
        {
            Debug.LogError("Tried to assign a null room to RoomManager.");
            return;
        }

        currentRoom = newRoom;

        // Set the initial wave if not set
        if (currentRoom.currentWave == 0)
        {
            currentRoom.currentWave = 1;
        }

        Debug.Log($"Player entered room {newRoom.roomID}");
        Debug.Log($"Current wave: {newRoom.currentWave}");

        OnRoomEntered?.Invoke(newRoom.roomID); // Notify listeners
        currentEnemies = enemyCount;
    }

    /// <summary>
    /// Called when the player leaves the current room.
    /// </summary>
    public void OnPlayerLeftRoom(Room room)
    {
        if (currentRoom == room)
        {
            OnRoomExited?.Invoke(currentRoom.roomID); // Notify listeners
            currentRoom = null;
            
            Debug.Log("Current room cleared because the player left.");
        }
    }

    /// <summary>
    /// Increments the wave if all enemies are defeated.
    /// </summary>
    public void UpdateWave()
    {
        if (currentEnemies <= 0)
        {
            Debug.Log("All enemies defeated. Advancing to next wave.");

            currentRoom.currentWave++;
            OnCallWaves?.Invoke(currentRoom.currentWave); // Notify listeners -> EnemyAI
        }
    }

    /// <summary>
    /// Should be called by enemies when they die.
    /// </summary>
    public void NotifyEnemyDeath()
    {
        //Deletes a counter on current enemies and Checks in Update Waves
        currentEnemies--;
        UpdateWave();
    }
}