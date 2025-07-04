using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    public ThoughtSO thought;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (thought == null)
            {
                Debug.LogWarning($"❌ ThoughtTrigger en {gameObject.name} no tiene asignado un ThoughtSO.");
                return;
            }

            if (ThoughtManager.Instance == null)
            {
                Debug.LogWarning("❌ No hay ThoughtManager activo en la escena.");
                return;
            }

            Debug.Log("🧠 Trigger activado por el jugador. Pensamiento: " + thought.text);
            ThoughtManager.Instance.ShowThought(thought);

            if (thought.destroyAfterShown)
                Destroy(gameObject);
        }
    }
}
