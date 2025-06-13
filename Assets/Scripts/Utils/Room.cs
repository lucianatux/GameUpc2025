using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Info")]

    [Tooltip("ID for this room.")]
    
    [SerializeField] public int roomID;
    
    [Tooltip("Current wave in room.")]
    [HideInInspector] public int currentWave;
    
    [Tooltip("Total waves for this room.")]
    public int maxWaves = 1;
    
    [Header("Checkpoint Settings")]
    public bool isCheckpointRoom = false;
    private void Awake()
    {
        if (maxWaves <= 0)
            maxWaves = 1;
    }
    private void OnTriggerEnter2D(Collider2D other) //Detects Collision When entering the room
    {
        // When detects the player entering the room
        if (other.CompareTag("Player"))
        {
            RoomManager.Instance.SetCurrentRoom(this); 

            Debug.Log("se envia la info a room manager del room " + roomID);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the room: " + name);
            
            RoomManager.Instance.OnPlayerLeftRoom(this); //Calls Function in Room Manager
        }
    }

}


