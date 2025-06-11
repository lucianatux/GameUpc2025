using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : PlayerAnimatorController
{
    // Start is called before the first frame update
    protected override void Awake()
    {
    animator = GetComponent<Animator>();
    if (animator == null) Debug.LogWarning("Animator not found");
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
