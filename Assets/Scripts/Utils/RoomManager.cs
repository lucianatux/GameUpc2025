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

    public event Action<int> OnRoomExited;

    public event Action<int> OnCallWaves;

    //private int currentWave = 0;
    
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

    public void SetCurrentRoom(Room newRoom)
    {
        currentRoom = newRoom; // dato a enviar 
    
        if (currentRoom == null) return; // si no estas referenciando a un cuarto, te vas

        if (currentRoom.currentWave == 0) newRoom.currentWave = 1; // si la wave fuese la primera
        Debug.Log("Jugador entró a la room " + newRoom.roomID);
        Debug.Log("Oleada nro " + newRoom.currentWave );
        OnRoomEntered?.Invoke(newRoom.roomID); // Evento para que lo escuchen, envia
        currentEnemies = enemyCount;

    }
    public void OnPlayerLeftRoom(Room _room)
{
    if (currentRoom == _room)
    {
        OnRoomExited?.Invoke(currentRoom.roomID);
        currentRoom = null;
        Debug.Log("Room actual vaciado porque el jugador salió.");
    }
}
    
    public void UpdateWave()
    {   
        if (currentEnemies <= 0)
        {
            Debug.Log("Todos los enemigos murieron, pasar a siguiente wave");
            currentRoom.currentWave ++;
            OnCallWaves?.Invoke(currentRoom.currentWave); // envia informacion de la wave actual 
        }
    }
    public void NotifyEnemyDeath()
    {
        currentEnemies--;
        UpdateWave();
    }


}
