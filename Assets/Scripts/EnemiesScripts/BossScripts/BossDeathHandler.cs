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
         // Reproduce música de victoria
    if (EnvironmentEventsManager.Instance != null)
        EnvironmentEventsManager.Instance.VictoryMusic();
        StartCoroutine(ActivateWinScreenWithDelay());
    }

    private IEnumerator ActivateWinScreenWithDelay()
    {
        yield return new WaitForSeconds(3f);
        if (winScreen != null)
            winScreen.SetActive(true);
    }
}