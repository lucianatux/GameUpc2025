using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private AnimName currentState;
    private int currentPriority;
    private bool isLocked;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Play(AnimName newState, int priority = 0, bool lockState = false)
    {
        if (isLocked && priority < currentPriority) return;
        if (newState == currentState) return;
        if (priority < currentPriority) return;
        animator.Play(newState.ToAnimString());
        currentState = newState;
        currentPriority = priority;
        isLocked = lockState;
    }

    public void Unlock()
    {
        isLocked = false;
        currentPriority = 0;
    }

    public bool IsPlaying(AnimName state)
    {
        return currentState == state;
    }
}