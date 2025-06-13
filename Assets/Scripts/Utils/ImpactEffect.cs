using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyImpactEffect), 2f);
    }

    private void DestroyImpactEffect()
    {
        Debug.Log("se destruye " + this);
        DestroyImmediate(gameObject, true);
    }
}
