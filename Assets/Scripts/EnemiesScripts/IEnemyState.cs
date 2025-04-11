using UnityEngine;
namespace StatePattern
{
public interface IEnemyState
{
    void EnterState (EnemyAI _enemyAI);
    void UpdateState();
}
}