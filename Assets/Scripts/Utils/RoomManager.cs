using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance => _instance;
    private static RoomManager _instance;

    public event Action<int> OnRoomEntered;

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this; // si no hay una instancia, que la cree
            return;
        }

        Destroy(gameObject); // si hay una instancia, que la destruya
    }

    public void SetCurrentRoom(int roomID)
    {
        Debug.Log("Jugador entró a la room " + roomID);
        OnRoomEntered?.Invoke(roomID); // Evento para que lo escuchen, envia room ID
    }
}
