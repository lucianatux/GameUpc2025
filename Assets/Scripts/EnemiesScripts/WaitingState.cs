using Unity.VisualScripting;
using UnityEngine;
namespace StatePattern
{
public class WaitingState : IEnemyState
{
    private EnemyAI enemyAI;
    
    private int enemyRoomID; 

    private int enemyWaveID;

    private int currentWave;

    private bool isActive;

    private int enemyCount;

    public WaitingState(int _enemyRoomID, int _enemyWaveID, bool _isActive)
    {
        enemyRoomID = _enemyRoomID;
        enemyWaveID = _enemyWaveID;
        isActive = _isActive;
    }
    
    public void EnterState(EnemyAI _enemyAI)
    {
        Debug.Log("estado waiting");
        RoomManager.Instance.OnRoomEntered += WakeUp;
        RoomManager.Instance.OnCallWaves += SpawnWaves;
        enemyAI = _enemyAI;
    }

    private void WakeUp(int room)
    {
        if (room != enemyRoomID) return;
        SpawnWaves(1);
        RoomManager.Instance.OnRoomEntered -= WakeUp;

    }
    private void SpawnWaves(int waveNumber)
    {
        if (waveNumber != enemyWaveID) return;

        // starCoroutine, donde paase el tiempo de la animacion de despertarse/activarse
        enemyAI.Attention();
                enemyAI.isActive = true;

        RoomManager.Instance.enemyCount++;
        RoomManager.Instance.OnCallWaves -= SpawnWaves;
        enemyAI.SetState(enemyAI.enemyChaseState);        
    }
    
    public void UpdateState()
    {

    }


}
}




