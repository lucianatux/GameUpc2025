using System.Collections; // ✅ NECESARIO para IEnumerator
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private string mainMenuSceneName = "Menu";
    [SerializeField] private int maxDeathsBeforeReset = 4;

    private int _currentDeathCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterDeath()
    {
        _currentDeathCount++;
        Debug.Log($"[GameManager] Muertes acumuladas: {_currentDeathCount}");

        if (_currentDeathCount >= maxDeathsBeforeReset)
        {
            ResetToMainMenu();
        }
    }

    private void ResetToMainMenu()
    {
        StartCoroutine(LoadMenuWithDelay());
    }

    private IEnumerator LoadMenuWithDelay()
    {
        Debug.Log("[GameManager] Esperando antes de cargar el menú...");
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ResetDeathCount()
    {
        _currentDeathCount = 0;
    }

    public int GetDeathCount()
    {
        return _currentDeathCount;
    }
}