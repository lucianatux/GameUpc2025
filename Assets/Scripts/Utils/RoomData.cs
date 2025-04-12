using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomData
{
    public int roomID;               // ID de la room
    public int totalWaves;           // Wave count
    public List<EnemyWave> waves;    // Enemigos por oleada

    // Evento que se dispara cuando cambia la oleada
    public event Action<int> OnWaveChanged;
}

[System.Serializable]
public class EnemyWave
{
    public int enemiesCount; // Cantidad de enemigos en esta oleada
     [Min(0)]
    public int cantidad = 3;

    public List<int> valores = new List<int>();

    private void OnValidate()
    {
        // Esto ajusta la lista automáticamente cuando cambiás "cantidad"
        if (valores.Count < cantidad)
        {
            while (valores.Count < cantidad)
            {
                valores.Add(0);
            }
        }
        else if (valores.Count > cantidad)
        {
            valores.RemoveRange(cantidad, valores.Count - cantidad);
        }
    }
}
