using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton que gestiona el estado general del juego, incluyendo muertes del jugador.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuración de juego")]
    [SerializeField] private string mainMenuSceneName = "Menu"; 
    [SerializeField] private int maxDeathsBeforeReset = 4;

    private int _currentDeathCount = 0;

    private void Awake()
    {
        // Asegurar que solo haya un GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantener el GameManager entre escenas
    }

    /// <summary>
    /// Llama esto cuando el jugador muere.
    /// </summary>
    public void RegisterDeath()
    {
        _currentDeathCount++;
        Debug.Log($"Muertes acumuladas: {_currentDeathCount}");

        if (_currentDeathCount >= maxDeathsBeforeReset)
        {
            ResetToMainMenu();
        }
    }

    /// <summary>
    /// Reinicia el juego cargando el menú principal y reseteando muertes.
    /// </summary>
    private void ResetToMainMenu()
    {
        _currentDeathCount = 0;
        Debug.Log("Límite de muertes alcanzado. Volviendo al menú principal.");
        SceneManager.LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// por las dudas haya que cambiar algo
    /// </summary>
    public void ResetDeathCount()
    {
        _currentDeathCount = 0;
    }

    public int GetDeathCount()
    {
        return _currentDeathCount;
    }
}