using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeratomaAnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Configuración")]
    [SerializeField] private float minIdleTime = 2f;
    [SerializeField] private float maxIdleTime = 5f;
    [SerializeField] private float walkSpeed = 1f;

    private bool walkRight = true;

    private void Start()
    {
        StartCoroutine(StateRoutine());
    }

    private IEnumerator StateRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minIdleTime, maxIdleTime);
            yield return new WaitForSeconds(waitTime);

            int choice = Random.Range(0, 30); // 0–6 = blink, 7–9 = talk, 10–12 = point/unpoint, 13–17 = walk

            if (choice < 7)
            {
                animator.SetTrigger("blink");
                yield return new WaitForSeconds(1f);
            }
            else if (choice < 15)
            {
                yield return StartCoroutine(PlayTalk());
            }
            else if (choice < 18)
            {
                yield return StartCoroutine(PlayPoint());
            }
            else
            {
                yield return StartCoroutine(PlayWalk());
            }
        }
    }

    private IEnumerator PlayTalk()
    {
        EnvironmentEventsManager.Instance.TeratomaInteract(); 
        animator.SetTrigger("talk");
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator PlayPoint()
    {
        animator.SetTrigger("point");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("unpoint");
        yield return new WaitForSeconds(1f); // tiempo estimado para volver a idle
    }

    private IEnumerator PlayWalk()
    {
        Debug.Log("Iniciando caminata...");
        animator.Play("Idle");

        // Alternar dirección
        walkRight = !walkRight;
        spriteRenderer.flipX = !walkRight;

        animator.SetBool("walking", true);

        float duration = 3f;
        float timer = 0f;

        while (timer < duration)
        {
            float direction = walkRight ? 1f : -1f;
            transform.position += new Vector3(direction * walkSpeed * Time.deltaTime, 0f, 0f);
            timer += Time.deltaTime;
            yield return null;
        }

        animator.SetBool("walking", false);
    }

}

