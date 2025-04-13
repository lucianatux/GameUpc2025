using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance => _instance;
    private static RoomManager _instance;

    private int _currentRoomID;
    public int CurrentRoomID => _currentRoomID;

    public Room currentRoom;

    public event Action<int> OnRoomEntered;

    public static event Action<int> OnCallEnemies;


    public Room room;

    private int currentWave = 0;
    private int remainingEnemies = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this; // si no hay una instancia, que la cree
            return;
        }

        Destroy(gameObject); // si hay una instancia, que la destruya
    }

/*
        void StartWave(int waveIndex)
    {
        if (waveIndex >= room.waveCount)
        {
            Debug.Log("🎉 Todas las oleadas completas");
            return;
        }   
                                                                                            
        int enemyCount = room.enemyCount[waveIndex];
        remainingEnemies = enemyCount;

        Debug.Log($"▶️ Iniciando wave {waveIndex + 1} con {enemyCount} enemigos");
        CallEnemies(waveIndex);
    }
    */
     void CallEnemies(int waveIndex)
    {
        if (currentRoom == null) return;
        if (waveIndex >= currentRoom.waveCount)
        {
            Debug.Log("🎉 Todas las oleadas completas");
            return;
        }
        
        OnCallEnemies?.Invoke(currentWave); // envia informacion de la wave actual
    }
    public void SetCurrentRoom(Room newRoom)
    {
        currentRoom = newRoom; // dato a enviar 
        Debug.Log("Jugador entró a la room " + newRoom.roomID);
        Debug.Log("Tiene " + newRoom.waveCount + " oleadas ");
        OnRoomEntered?.Invoke(newRoom.roomID); // Evento para que lo escuchen, envia
    }

    private void UpdateWaves()
    {
        currentWave ++;
    }


}
