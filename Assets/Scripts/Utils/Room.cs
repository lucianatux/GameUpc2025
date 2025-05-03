using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Settings")]

    [Tooltip("ID único para esta habitación.")]
    
    [SerializeField] public int roomID;
    public int waveCount;
    
    private void OnTriggerEnter2D(Collider2D other) //detecta al player entrar a la room
    {
        if (other.CompareTag("Player"))
        {
                RoomManager.Instance.SetCurrentRoom(this); //
                Debug.Log("se envia la info a room manager del room " + roomID);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
            if (other.CompareTag("Player"))
        {
        Debug.Log("El jugador SALIÓ del cuarto: " + name);
        // Podés notificar al RoomManager si querés
        RoomManager.Instance.OnPlayerLeftRoom(this);
        }
    }

}


