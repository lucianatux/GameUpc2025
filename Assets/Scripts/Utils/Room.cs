using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Settings")]

    [Tooltip("ID único para esta habitación.")]
    
    [SerializeField] private int roomID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                RoomManager.Instance.SetCurrentRoom(roomID);
        }
    }
}
