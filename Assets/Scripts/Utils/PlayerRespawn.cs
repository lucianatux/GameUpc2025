using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint;
    private Vector3 initialSpawn;

    private void Start()
    {
        // Guardamos el primer spawn (inicio del nivel)
        initialSpawn = GameObject.FindWithTag("SpawnPoint").transform.position;
        transform.position = initialSpawn;
    }
    public void Respawn()
    {
        Vector3 respawnPoint;

        if (RoomManager.Instance != null && RoomManager.Instance.LastCheckpointRoom != null)
        {
            respawnPoint = RoomManager.Instance.LastCheckpointRoom.transform.position;
        }
        else
        {
            respawnPoint = initialSpawn;
        }

        transform.position = respawnPoint;

        // Restauramos la vida al respawnear (opcional)
        LifeSystem life = GetComponent<LifeSystem>();
        if (life != null)
        {
            life.ResetHealth();
        }
    } 
}
