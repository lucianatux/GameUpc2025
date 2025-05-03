using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : LifeSystem
{
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

        protected override void Die() 
    {
        base.Die();
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("Die");
        ChangeAnimationState(AnimName.DieAnim);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        ChangeAnimationState(AnimName.DamageAnim);
    }
    
}
