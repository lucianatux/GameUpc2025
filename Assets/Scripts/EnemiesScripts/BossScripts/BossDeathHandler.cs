using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathHandler : MonoBehaviour
{
     [SerializeField] private GameObject winScreen;

    public void OnBossDefeated()
    {
        StartCoroutine(ActivateWinScreenWithDelay());
    }

    private IEnumerator ActivateWinScreenWithDelay()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            Debug.Log("Pantalla de victoria activada.");

            if (EnvironmentEventsManager.Instance != null)
            {
                EnvironmentEventsManager.Instance.VictoryMusic();
            }
        }
        else
        {
            Debug.LogWarning("No se asignó winScreen en el inspector.");
        }
    }
}
