using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    public ThoughtSO thought;
    public bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            ThoughtManager.Instance.ShowThought(thought);

            if (triggerOnce)
                hasTriggered = true;
        }
    }
}
