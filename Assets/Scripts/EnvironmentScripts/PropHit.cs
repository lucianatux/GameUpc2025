using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHit : MonoBehaviour
{

    [SerializeField] GameObject hitImpact;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            if (hitImpact != null) Instantiate(hitImpact);

            anim.SetTrigger("hit");
            EnvironmentEventsManager.Instance.BurguerHit(); 
        }
    }
}
