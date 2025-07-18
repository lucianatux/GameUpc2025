using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseTextUI; // Texto UI que dice "Juego en pausa"
    [SerializeField] private SoundManager soundManager; // Referencia al SoundManager

    [SerializeField] private GameObject PauseMenu;
    private bool isPaused = false;



    public float pauseTime = 0.1f;  // Ajustalo según necesites

    private IEnumerator Start()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            Pause();
        else
            Resume();
    }


    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        Application.Quit();
        //Debug.Log("Saliste del juego");
    }
    
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        PauseMenu.SetActive(true);

        // if (pauseTextUI != null)
        // pauseTextUI.SetActive(true);

        if (soundManager != null)
            soundManager.PauseMusic();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        PauseMenu.SetActive(false);


        // if (pauseTextUI != null)
        //  pauseTextUI.SetActive(false);

        if (soundManager != null)
            soundManager.ResumeMusic();
    }

}
