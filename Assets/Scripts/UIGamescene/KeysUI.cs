using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private List<Image> emptyKeys;
    [SerializeField] private List<Image> fullKeys;

    private int collectedKeys = 0;
    private bool reachedEndPlayground = false;

    private void Start()
    {
        if (EnvironmentEventsManager.Instance != null)
        {
            EnvironmentEventsManager.Instance.OnKeyCollected -= OnKeyCollected;
            EnvironmentEventsManager.Instance.OnKeyCollected += OnKeyCollected;
            EnvironmentEventsManager.Instance.OnEndPlayground += OnEndPlaygroundReached;
            EnvironmentEventsManager.Instance.OnEndMiddlePart += OnEndMiddlePartReached;
        }
        if (!reachedEndPlayground)
        {
            ResetUIToStartState();
        }
    }

    private void OnDestroy()
    {
        if (EnvironmentEventsManager.Instance != null)
        {
            EnvironmentEventsManager.Instance.OnKeyCollected -= OnKeyCollected;
            EnvironmentEventsManager.Instance.OnEndPlayground -= OnEndPlaygroundReached;
            EnvironmentEventsManager.Instance.OnEndMiddlePart -= OnEndMiddlePartReached;
        }
    }

    private void ResetUIToStartState()
    {
        collectedKeys = 0;
        for (int i = 0; i < emptyKeys.Count; i++)
        {
            emptyKeys[i].gameObject.SetActive(i == 0); // Solo la primera activa
            fullKeys[i].gameObject.SetActive(false);
        }
    }

    private void OnKeyCollected()
    {
        if (collectedKeys >= fullKeys.Count) return;

        fullKeys[collectedKeys].gameObject.SetActive(true);
        emptyKeys[collectedKeys].gameObject.SetActive(false);
        collectedKeys++;
    }


    private void OnEndPlaygroundReached()
    {
        if (reachedEndPlayground) return;

        reachedEndPlayground = true;

        for (int i = 0; i < emptyKeys.Count; i++)
        {
            emptyKeys[i].gameObject.SetActive(true);
            fullKeys[i].gameObject.SetActive(false);
        }

        collectedKeys = 0;
    }

    private void OnEndMiddlePartReached()
    {
        for (int i = 0; i < emptyKeys.Count; i++)
        {
            emptyKeys[i].gameObject.SetActive(false);
            fullKeys[i].gameObject.SetActive(false);
        }
    }
}
