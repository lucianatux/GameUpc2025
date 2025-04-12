using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Settings")]

    [Tooltip("ID único para esta habitación.")]
    
    [SerializeField] private int roomID;

    [Min(0)]

    [Header("Cantidad de Waves")]
    [Tooltip("Número total de oleadas en el nivel")]
        public int waveCount;
    [Tooltip("Cantidad de enemigos en cada oleada")]
    public List<int> enemyCount = new List<int>();

    private void OnValidate()
    {
        // ajustar  en el editor cada vez que cambie la wavecount
        if (enemyCount.Count < waveCount)
        {
            while (enemyCount.Count < waveCount)
            {
                enemyCount.Add(0);
            }
        }
        else if (enemyCount.Count > waveCount)
        {
            enemyCount.RemoveRange(waveCount, enemyCount.Count - waveCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                RoomManager.Instance.SetCurrentRoom(roomID);
                // RoomManager.Instance.S
        }
    }

}
