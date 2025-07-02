using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aji : MonoBehaviour
{
    public AbilityController abilityController;

    private Collider2D _col;
    private Rigidbody2D _rb;

    void Awake()
    {
        _col = GetComponent<Collider2D>();
        if (_col == null)
        {
            Debug.LogError("Collider2D not found in Aji");
        }

        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody2D not found");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision + "aji");

        if (collision.CompareTag("Player"))
        {
            abilityController = collision.GetComponent<AbilityController>();
            Debug.Log("aji touchesPlayer");
            abilityController.UnlockFireBall();
            Destroy(gameObject);
            
            }
    }

}
