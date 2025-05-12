using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LifeSystem : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected float invulnerabilityTime;
    protected float invulnerabilityTimer;
    [SerializeField] protected bool canHeal = false;

    protected Animator animator;
    protected string currentAnim;

    protected AnimationStateController animController;
    protected virtual void Start()
    {
        currentHealth = maxHealth; 
        animController = GetComponent<AnimationStateController>();
    }

    protected virtual void Update() 
    {
        invulnerabilityTimer -= Time.deltaTime; // timer que cuenta en cuanto tiempo puede volver a recibir daño
    }


    // funcion publica a la que acceden los ataques, dando un parametro daño que se va a recibir 
    public virtual void TakeDamage(int damage) 
    {
        if (invulnerabilityTimer > 0 || currentHealth <= 0) return; // si estas en invulnerable, o con menos de 0 de vida, no recibis
        currentHealth -= damage; 
        Debug.Log($"{gameObject.name} recibe {damage} de daño. Vida restante: {currentHealth}");
        invulnerabilityTimer = invulnerabilityTime; // se resetea el tiempo de invulnerabilidad
        if (currentHealth <= 0) // si el ataque baja la vida a menos de 0 o 0 se llama a la funcion morir
        {
            currentHealth = 0;
            Die();
        }
        animController.Play(AnimName.DamageAnim, 1, true);
        StartCoroutine(UnlockAfter(.15f));
    }
    private IEnumerator UnlockAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animController.Unlock();
        animController.Play(AnimName.IdleAnim, 1);
    }
    // protected porque es privada para otras clases, publica para clases hijas
    // virtual porque es abstracta y se puede modificar desde otra clase
    protected virtual void Die() 
    {
        animController.Play(AnimName.DieAnim, 4, true);
        StartCoroutine(WaitAndDestroy(3f)); // o animación.length si lo calculás
    }

    private IEnumerator WaitAndDestroy(float delay)
    {
        animController.Play(AnimName.DieAnim, 10, true);
        yield return new WaitForSeconds(.2f);
        
        animController.Play(AnimName.DieAnim, 10, true);

        yield return new WaitForSeconds(delay);
        animController.Play(AnimName.DieAnim, 10, true);

        Destroy(gameObject);
    }

    // se llama cuando se vaya a querer curar a la entidad
    public virtual void Heal(int healAmount)
    {
        if (!canHeal || currentHealth >= maxHealth) return; // no se puede curar si, no es un objeto curable o si tiene maxhealth

        currentHealth += healAmount;
        Debug.Log($"{gameObject.name} se cura. Vida actual: {currentHealth}");
        if (currentHealth >= maxHealth) currentHealth = maxHealth;
    }


        public void ChangeAnimationState (AnimName newAnim) 
    {
        string newAnimString = newAnim.ToAnimString(); // de AnimName lo convertimos a string con la funcion estaticas toanimstring

        if (currentAnim == newAnimString) return; //chequeamos que no se interrumpa a si misma

        animator.Play(newAnimString); //empieza la animacion

        currentAnim = newAnimString; //reseteamos la current animation a la que esta sucediendo
    }

}

