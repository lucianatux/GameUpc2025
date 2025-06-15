using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float seconds;
    void Start()
    {
        Invoke(nameof(DestroyImpactEffect), seconds);
    }

    private void DestroyImpactEffect()
    {
        Debug.Log("se destruye " + this);
        DestroyImmediate(gameObject, true);
    }
}
