using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private bool subscribed = false;

    private void Start()
    {
        TrySubscribe();
    }

    private void TrySubscribe()
    {
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.OnRoomEntered += HandleRoomEntered;
            RoomManager.Instance.OnRoomCleared += HandleRoomCleared;

            PlayerHealth.OnPlayerDeath += HandlePlayerDeath;

            subscribed = true;
            Debug.Log("DoorManager subscribed to RoomManager and PlayerHealth events.");
        }
        else
        {
            Debug.LogWarning("RoomManager not initialized yet. Retrying in 0.5 seconds...");
            Invoke(nameof(TrySubscribe), 0.5f);
        }
    }

    private void OnDisable()
    {
        if (subscribed)
        {
            if (RoomManager.Instance != null)
            {
                RoomManager.Instance.OnRoomEntered -= HandleRoomEntered;
                RoomManager.Instance.OnRoomCleared -= HandleRoomCleared;
            }

            PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;

            subscribed = false;
        }
    }

    private void HandleRoomEntered(int roomID)
    {
        Debug.Log($"[DoorManager] Entered room {roomID}");

        if (RoomManager.Instance.IsRoomActive(roomID))
        {
            Debug.Log($"[DoorManager] Room {roomID} is active, closing and locking doors.");
            foreach (var door in GetDoorsForRoom(roomID))
            {
                door.Close();
                door.SetLocked(true);
            }
        }
        else
        {
            Debug.Log($"[DoorManager] Room {roomID} is NOT active, opening doors.");
            foreach (var door in GetDoorsForRoom(roomID))
            {
                door.SetLocked(false);
                door.Open();
            }
        }
    }

    private void HandleRoomCleared(int roomID)
    {
        Debug.Log($"[DoorManager] Room {roomID} cleared. Unlocking and opening doors.");
        foreach (var door in GetDoorsForRoom(roomID))
        {
            door.SetLocked(false);
            door.Open();
        }
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("[DoorManager] Player died. Unlocking and opening all doors.");
        foreach (var door in FindObjectsOfType<Door>())
        {
            door.SetLocked(false);
            if (!door.NeverAutoOpen)
                door.Open();
            else
                door.Close(); // por si se había quedado abierta por algún bug previo
        }
        
    }

    private List<Door> GetDoorsForRoom(int roomID)
    {
        return new List<Door>(FindObjectsOfType<Door>()).FindAll(d => d.roomID == roomID);
    }
}
