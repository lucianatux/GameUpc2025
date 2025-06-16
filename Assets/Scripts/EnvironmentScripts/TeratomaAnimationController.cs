using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeratomaAnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Configuración")]
    [SerializeField] private float minIdleTime = 2f;
    [SerializeField] private float maxIdleTime = 5f;

    private bool isPlaying = false;

    private void Start()
    {
        StartCoroutine(StateRoutine());
    }

    private IEnumerator StateRoutine()
    {
        while (true)
        {
            // Esperar tiempo random antes de siguiente acción
            float waitTime = Random.Range(minIdleTime, maxIdleTime);
            yield return new WaitForSeconds(waitTime);

            // Elegir animación aleatoria
            int choice = Random.Range(0, 10); // 0–6 = blink, 7–9 = talk
            if (choice < 7) {
                animator.SetTrigger("blink");
                yield return new WaitForSeconds(1f);
            } else {
                yield return StartCoroutine(PlayTalk());
            }
        }
    }

    private void TriggerBlink()
    {
        animator.SetTrigger("blink");
    }

    private IEnumerator PlayTalk()
    {
        EnvironmentEventsManager.Instance.TeratomaInteract(); 
        animator.SetTrigger("talk");
        yield return new WaitForSeconds(3f);
        // Cortamos el loop manualmente volviendo a Idle
        //animator.Play("Idle", 0);
       
    }
}
