using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapToggle : MonoBehaviour
{
    public GameObject minimapPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && minimapPanel != null)
        {
            bool isActive = !minimapPanel.activeSelf;
            minimapPanel.SetActive(isActive);

            if (isActive)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }
}

