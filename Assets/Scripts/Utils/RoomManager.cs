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

    public event Action<int> OnCallWaves;

    public Room room;

    private int currentWave = 0;
    
    public int enemyCount; 

    public int currentEnemies;
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
        if (currentWave >= currentRoom.waveCount)
        {
            Debug.Log("🎉 Todas las oleadas completas");
            return;
        }

        OnCallWaves?.Invoke(currentWave); // envia informacion de la wave actual
    }
    public void SetCurrentRoom(Room newRoom)
    {
        currentRoom = newRoom; // dato a enviar 

        if (currentRoom == null) return; // si no estas referenciando a un cuarto, te vas

        currentWave = 1; //la wave seria la primera
        Debug.Log("Jugador entró a la room " + newRoom.roomID);
        Debug.Log("Oleada nro " + currentWave );
        OnRoomEntered?.Invoke(newRoom.roomID); // Evento para que lo escuchen, envia
        currentEnemies = enemyCount;

    }
    public void OnPlayerLeftRoom(Room room)
{
    if (currentRoom == room)
    {
        
        currentRoom = null;
        Debug.Log("Room actual vaciado porque el jugador salió.");
    }
}
    
    public void NotifyEnemyDeath()
    {   
        currentEnemies--;
        if (currentEnemies <= 0)
        {
            Debug.Log("Todos los enemigos murieron, pasar a siguiente wave");
            currentWave ++;
            OnCallWaves?.Invoke(currentWave); // envia informacion de la wave actual 
        }
    }
    private void EnemyDeath()
    {
        
    }


}
