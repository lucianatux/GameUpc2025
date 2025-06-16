using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detecta cuando el jugador entra en contacto con el Teratoma y lo cura.
/// </summary>
public class TeratomaTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        LifeSystem playerLife = other.GetComponent<LifeSystem>();
        if (playerLife != null)
        {
            int faltaParaCurarse = playerLife.MaxHealth - playerLife.CurrentHealth;

            if (faltaParaCurarse > 0)
            {
                playerLife.Heal(faltaParaCurarse);
                PlayerEventsManager.Instance?.PlayerHeal();
            }
        }
    }
}

}
