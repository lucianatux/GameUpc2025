using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint;

    private void Start()
    {
        // Asignar el spawn inicial
        currentCheckpoint = GameObject.FindWithTag("SpawnPoint").transform.position;
        transform.position = currentCheckpoint;
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint;
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
}
