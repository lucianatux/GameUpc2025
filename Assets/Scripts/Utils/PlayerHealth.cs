using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : LifeSystem
{      
    //private AnimationStateController animController;
    private PlayerMovement playerMovement;
    protected override void Start()
    {
        //animController = GetComponent<AnimationStateController>();
        playerMovement = GetComponent<PlayerMovement>();
        base.Start();
    }

        protected override void Die() 
    {
                GetComponent<BasicAttack>().canAttack = false;
    
        base.Die();

        playerMovement.canMove = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        //animController.Play(AnimName.DieAnim, 3);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
