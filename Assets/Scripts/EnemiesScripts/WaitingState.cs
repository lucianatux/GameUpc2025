using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

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

    public WaitingState(int _enemyRoomID, int _enemyWaveID)
    {
        enemyRoomID = _enemyRoomID;
        enemyWaveID = _enemyWaveID;
        
    }
    
    public void EnterState(EnemyAI _enemyAI)
    {
        Debug.Log("estado waiting");
        enemyAI.isActive = false;
        RoomManager.Instance.OnRoomEntered += WakeUp;
        RoomManager.Instance.OnCallWaves += SpawnWaves;
        enemyAI = _enemyAI;
        enemyAI.ChangeAnimationState(AnimName.InactiveAnim);
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
        enemyAI.StartCoroutine(WakeUpAnimation()); // Esperar antes de disparar
        RoomManager.Instance.enemyCount++;
        RoomManager.Instance.OnCallWaves -= SpawnWaves;
    }
    
    private IEnumerator WakeUpAnimation()
{

    enemyAI.ChangeAnimationState(AnimName.WakeUpAnim);
    yield return new WaitForSeconds(1f); // Espera antes de volver a moverse
    enemyAI.SetState(enemyAI.enemyChaseState);        

    Debug.Log("Se despierta");
}



    public void UpdateState()
    {

    }


}
}




