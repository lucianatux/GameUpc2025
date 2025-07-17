using System.Collections;
using UnityEngine;

public class BossDeathHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private BossHealthUI bossUI;
    [SerializeField] private EnemyHealth enemyHealth;

    [Header("Victoria")]
    [SerializeField] private GameObject winScreen;

    private bool bossActivated = false;

    public void ActivateBoss()
    {
        bossActivated = true;

        if (bossUI != null && enemyHealth != null)
            bossUI.Initialize(enemyHealth.MaxHealth); // Asegurate que MaxLife esté disponible
    }

    private void Update()
    {
        if (!bossActivated || bossUI == null || enemyHealth == null) return;

        bossUI.UpdateHealth(enemyHealth.CurrentHealth);
    }

    public void OnBossDefeated()
    {
        bossUI?.Hide();
        StartCoroutine(ActivateWinScreenWithDelay());
    }

    private IEnumerator ActivateWinScreenWithDelay()
    {
        yield return new WaitForSeconds(3f);
        if (EnvironmentEventsManager.Instance != null)
            EnvironmentEventsManager.Instance.VictoryMusic();

        if (winScreen != null)
        {
            winScreen.SetActive(true);
            Debug.Log("Pantalla de victoria activada.");
        }
        else
        {
            Debug.LogWarning("No se asignó winScreen en el inspector.");
        }
    }
}