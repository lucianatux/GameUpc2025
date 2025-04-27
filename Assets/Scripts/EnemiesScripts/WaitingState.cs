using Unity.VisualScripting;
using UnityEngine;
namespace StatePattern
{
public class WaitingState : IEnemyState
{
    private EnemyAI enemyAI;
    
    private int roomID; 

    private int waveID;

    private int currentWave;
    public WaitingState(int _roomID, int _waveID)
    {
        roomID = _roomID;
        waveID = _waveID;
    }
    
    public void EnterState(EnemyAI _enemyAI)
    {
        Debug.Log("Cambia a estado wander");
        // GameManager.Instance.OnEnterEvent += StartEnemyWaves;
        enemyAI = _enemyAI;
    }

    private void StartEnemyWaves(int room)
    {
        if (room != roomID) return;
        currentWave = 1;        
        WakeUp(currentWave);
    }
    private void WakeUp(int waveNumber)
    {
            if (waveNumber != waveID) return;
            // starCoroutine, donde paase el tiempo de la animacion de despertarse/activarse
            enemyAI.Attention();
            enemyAI.SetState(enemyAI.enemyChaseState);        
    }
    
    public void UpdateState()
    {

    }


}
}




