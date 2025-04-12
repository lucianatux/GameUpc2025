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
     private void Start()
    {
        CallEnemies(currentWave);
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
        if (waveIndex >= room.waveCount)
        {
            Debug.Log("🎉 Todas las oleadas completas");
            return;
        }

        int enemyCount = room.enemyCount[waveIndex];
        remainingEnemies = enemyCount; // los remaining enemies se reinician

        Debug.Log($"▶️ Llamando a wave {waveIndex + 1} con {enemyCount} enemigos");

        OnCallEnemies?.Invoke(waveIndex); // envia informacion de la wave actual
    }
    public void SetCurrentRoom(int roomID)
    {
        _currentRoomID = roomID; // dato a enviar 
        Debug.Log("Jugador entró a la room " + roomID);
        OnRoomEntered?.Invoke(roomID); // Evento para que lo escuchen, envia
    }
}
