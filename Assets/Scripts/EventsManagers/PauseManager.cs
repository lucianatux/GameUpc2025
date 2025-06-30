using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseTextUI; // Texto UI que dice "Juego en pausa"
    [SerializeField] private SoundManager soundManager; // Referencia al SoundManager

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;

            if (pauseTextUI != null)
                pauseTextUI.SetActive(true);

            if (soundManager != null)
                soundManager.PauseMusic();
        }
        else
        {
            Time.timeScale = 1f;

            if (pauseTextUI != null)
                pauseTextUI.SetActive(false);

            if (soundManager != null)
                soundManager.ResumeMusic();
        }
    }
}
