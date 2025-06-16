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
            subscribed = true;
            Debug.Log("DoorManager subscribed to RoomManager events.");
        }
        else
        {
            Debug.LogWarning("RoomManager not initialized yet. Retrying in 0.5 seconds...");
            Invoke(nameof(TrySubscribe), 0.5f); // reintenta después de 0.5 segundos
        }
    }

    private void OnDisable()
    {
        if (subscribed && RoomManager.Instance != null)
        {
            RoomManager.Instance.OnRoomEntered -= HandleRoomEntered;
            RoomManager.Instance.OnRoomCleared -= HandleRoomCleared;
            subscribed = false;
        }
    }

    private void HandleRoomEntered(int roomID)
    {
        if (RoomManager.Instance.IsRoomActive(roomID))
        {
            foreach (var door in GetDoorsForRoom(roomID))
            {
                door.Close();
                door.SetLocked(true);
            }
        }
        else
        {
            foreach (var door in GetDoorsForRoom(roomID))
            {
                door.SetLocked(false);
                door.Open();
            }
        }
    }


    private void HandleRoomCleared(int roomID)
    {
        foreach (var door in GetDoorsForRoom(roomID))
        {
            door.SetLocked(false);
            door.Open();
        }
    }

    private List<Door> GetDoorsForRoom(int roomID)
    {
        return new List<Door>(FindObjectsOfType<Door>()).FindAll(d => d.roomID == roomID);
    }
}