
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
namespace StatePattern
{
    public class WaitingState : IEnemyState
    {
        private EnemyAI enemyAI;

        // Room and wave identifiers
        private int enemyRoomID;
        private int enemyWaveID;

        /// <summary>
        /// Constructor: receives the enemy's room ID and wave ID.
        /// </summary>
        public WaitingState(int _enemyRoomID, int _enemyWaveID)
        {
            enemyRoomID = _enemyRoomID;
            enemyWaveID = _enemyWaveID;
        }

        /// <summary>
        /// Called when entering the Waiting state. Subscribes to room events.
        /// </summary>
        public void EnterState(EnemyAI _enemyAI)
        {
            if (_enemyAI == null)
            {
                Debug.LogError("WaitingState: enemyAI is null in EnterState.");
                return;
            }

            enemyAI = _enemyAI;
            enemyAI.isActive = false;

            //Subscribes on RoomManager Events
            RoomManager.Instance.OnRoomEntered += WakeUp;
            RoomManager.Instance.OnCallWaves += SpawnWaves;

            Debug.Log("Entered Waiting state");
        }

        /// <summary>
        /// Not used but necessary for IEnemyState
        /// </summary>
        public void UpdateState()
        {
        }

        /// <summary>
        /// Triggered when a player enters a room. If it's the right room, prepares the enemy.
        /// </summary>
        private void WakeUp(int room)
        {
            if (room != enemyRoomID) return;

            // Optionally: RoomManager.Instance.NotifyEnemySpawn();
            SpawnWaves(1);

            RoomManager.Instance.OnRoomEntered -= WakeUp;
        }

        /// <summary>
        /// Called when waves are triggered. If it's the correct wave, start wake-up logic.
        /// </summary>
        private void SpawnWaves(int waveNumber)
        {
            //checks if correct enemy wave
            if (waveNumber != enemyWaveID) return;

            enemyAI.ShowAlert("attention");
            enemyAI.isActive = true;

            enemyAI.StartCoroutine(WakeUpAnimation());

            RoomManager.Instance.OnCallWaves -= SpawnWaves;
        }

        /// <summary>
        /// Coroutine: handles wake-up animation, disables physics, and switches to Chase.
        /// </summary>
        private IEnumerator WakeUpAnimation()
        {
            //wake up animation

            yield return new WaitForSeconds(.5f);

            //Activate physics and collisions 
            if (enemyAI.rb != null)
            {
                enemyAI.rb.bodyType = RigidbodyType2D.Kinematic; // Disable physics interactions
            }

            if (enemyAI.col)
            {
                enemyAI.col.enabled = true;
            }

            Debug.Log("Enemy wakes up");

            enemyAI.SetState(enemyAI.enemyChaseState);
        }
    }
}