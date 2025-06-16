using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public enum ZoneType { EndPlayground, EndMiddlePart }
    public ZoneType zoneType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (EnvironmentEventsManager.Instance == null) return;

        switch (zoneType)
        {
            case ZoneType.EndPlayground:
                EnvironmentEventsManager.Instance.EndPlayground();
                Debug.Log("Evento EndPlayground disparado");
                break;

            case ZoneType.EndMiddlePart:
                EnvironmentEventsManager.Instance.EndMiddlePart();
                Debug.Log("Evento EndMiddlePart disparado");
                break;
        }
    }
}