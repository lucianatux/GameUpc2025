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
            bool isDead = false; 
    public override void TriggerAnim(string triggerName)
    {
        // Se asegura de reiniciar el trigger antes de activarlo (evita animaciones bloqueadas).
        if (triggerName == "die")
        {
            animator.SetTrigger("die");
            isDead = true;
            return;
        }
        if (isDead == true) return;
        animator.ResetTrigger(triggerName);
        animator.SetTrigger(triggerName);

    }
    
}
