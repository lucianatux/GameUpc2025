using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private GameObject explosionPrefab;
    private GameObject fallPrefab;

    void Start()
    {

    }
    void Awake()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();


        col.enabled = false;
    }


    void OnEnable()
    {
        transform.position = originalPosition;
        //animacion de caida
        StartCoroutine(BombFall(1f));
    }

    private IEnumerator BombFall(float seconds)
    {
        //animacion
        yield return new WaitForSeconds(seconds);
        col.enabled = true;
        Debug.Log("Falls  " + this);

        if (fallPrefab != null)
        {
        Debug.Log("Instatiate fall damage  " + this);

        Instantiate(fallPrefab, originalPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No fall prefab found");
        }
        StartCoroutine(BombExplosion(1f));

    }

    private IEnumerator BombExplosion(float seconds)
    {
        //animacion por explotar
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explodes " + this);

        if (explosionPrefab != null)
        {
            Debug.Log("Instatiate explosion " + this);

            Instantiate(explosionPrefab, originalPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No hay prefab de explosión asignado");
        }

        yield return new WaitForSeconds(.1f);
        Debug.Log("Disable bomb " + this);
        gameObject.SetActive(false); // o Destroy(gameObject) si no la vas a volver a usar

    }
}
