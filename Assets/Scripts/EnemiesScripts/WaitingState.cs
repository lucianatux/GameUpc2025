
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
        RoomManager.Instance.OnRoomEntered += WakeUp;
        RoomManager.Instance.OnCallWaves += SpawnWaves;
        enemyAI = _enemyAI;
        enemyAI.isActive = false;

    }
   private IEnumerator UnlockAfter(float seconds)
    {
        Debug.Log("empieza devloqueo");
        yield return new WaitForSeconds(seconds);
        Debug.Log("termina devloqueo");
        //enemyAI.animController.Unlock();
    }
    private void WakeUp(int room)
    {
        if (room != enemyRoomID) return;
        //RoomManager.Instance.NotifyEnemySpawn();

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
        RoomManager.Instance.OnCallWaves -= SpawnWaves;
    }
    
    private IEnumerator WakeUpAnimation()
{
    //enemyAI.animController.Play(AnimName.WakeUpAnim, 2, true); //seteamos su animacion
    enemyAI.StartCoroutine(UnlockAfter(.5f));
    //RoomManager.Instance.NotifyEnemySpawn();
    yield return new WaitForSeconds(1f); // Espera antes de volver a moverse
    if (enemyAI.rb != null)
                {
                    enemyAI.rb.bodyType = RigidbodyType2D.Kinematic; // para que no lo afecte la física
                }
        enemyAI.SetState(enemyAI.enemyChaseState);   
        enemyAI.GetComponent<Collider2D>().enabled = true;
        Debug.Log("Se despierta");
}



    public void UpdateState()
    {

    }


}
}

