using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconUpdater : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform playerIcon;  // El icono del jugador en el minimapa
    [SerializeField] private Transform player;          // El transform del jugador en la escena
    [SerializeField] private RectTransform mapRect;     // El rectángulo del mapa en la UI

    [Header("World Bounds")]
    [SerializeField] private Vector2 worldMin;          // Coordenadas mínimas del mundo (X,Y)
    [SerializeField] private Vector2 worldMax;          // Coordenadas máximas del mundo (X,Y)

    private void Update()
    {
        if (player == null || playerIcon == null || mapRect == null)
            return;

        Vector3 playerPosition = player.position;

        // Normalizamos usando X e Y 
        float normalizedX = Mathf.InverseLerp(worldMin.x, worldMax.x, playerPosition.x);
        float normalizedY = Mathf.InverseLerp(worldMin.y, worldMax.y, playerPosition.y);

        // Calcular posición en el minimapa (ajustar porque el ancla suele estar en el centro)
        float iconX = (normalizedX * mapRect.rect.width) - (mapRect.rect.width * 0.5f);
        float iconY = (normalizedY * mapRect.rect.height) - (mapRect.rect.height * 0.5f);

        playerIcon.localPosition = new Vector2(iconX, iconY);
    }
}
