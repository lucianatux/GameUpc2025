using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;

public interface IBossState
{
    void EnterState (BossAI _BossAI);
    void UpdateState();
}

