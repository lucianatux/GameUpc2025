using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObjectLayerSwitcher : MonoBehaviour
{
    [Tooltip("Transform del jugador (arrástralo aquí)")]
    public Transform player; // Asigna el jugador manualmente o déjalo que lo detecte automáticamente
    
    [Tooltip("Ajuste fino de la posición Y para comparación")]
    public float yOffset = 0f; // Útil si el pivote del objeto no está centrado
    
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Si no se asignó manualmente el jugador, intenta encontrarlo por tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Si el objeto está MÁS ARRIBA (Y mayor) que el jugador → Order = -1 (detrás)
            // Si está MÁS ABAJO (Y menor) → Order = 1 (encima)
            spriteRenderer.sortingOrder = (transform.position.y + yOffset > player.position.y) ? -1 : 1;
        }
    }
}