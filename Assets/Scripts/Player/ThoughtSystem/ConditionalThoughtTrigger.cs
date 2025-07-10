using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConditionalThoughtTrigger : MonoBehaviour
{
    public ThoughtSO thought;

    public enum ConditionType
    {
        None,
        HasAllRequiredKeys,
        PlayerHealthFull
    }

    public ConditionType condition = ConditionType.None;

    private Door doorScript;

    private void Awake()
    {
        doorScript = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (thought == null)
        {
            Debug.LogWarning("❌ ConditionalThoughtTrigger: ThoughtSO no asignado.");
            return;
        }

        if (ThoughtManager.Instance == null)
        {
            Debug.LogWarning("❌ ThoughtManager.Instance no inicializado.");
            return;
        }

        // Chequear la condición que corresponde
        if (ConditionIsMet(condition, other.gameObject))
        {
            Debug.Log("⚠️ Se cumplió la condición, no se muestra el pensamiento.");
            return;
        }

        // Mostrar el pensamiento normalmente
        ThoughtManager.Instance.ShowThought(thought);

        if (thought.destroyAfterShown)
            Destroy(gameObject);
    }

    private bool ConditionIsMet(ConditionType cond, GameObject player)
    {
        switch (cond)
        {
            case ConditionType.HasAllRequiredKeys:
                return PlayerHasAllRequiredKeys(player);

            case ConditionType.PlayerHealthFull:
                return PlayerHasFullHealth(player);

            case ConditionType.None:
            default:
                return false; // no hay condición, siempre mostrar
        }
    }

    private bool PlayerHasAllRequiredKeys(GameObject player)
    {
        var inventory = player.GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogWarning("❌ PlayerInventory no encontrado.");
            return false;
        }

        if (doorScript == null)
        {
            Debug.LogWarning("❌ Door no encontrado en el objeto.");
            return false;
        }

        if (!doorScript.RequiresKeys())
            return true; // si no necesita llaves, consideramos que sí las tiene

        // true = tiene todas las llaves; false = le falta alguna
        return !doorScript.RequiredKeys.Any(key => !inventory.HasKey(key.id));

    }

    private bool PlayerHasFullHealth(GameObject player)
    {
        var health = player.GetComponent<PlayerHealth>();
        if (health == null)
        {
            Debug.LogWarning("❌ PlayerHealth no encontrado.");
            return false;
        }

        return health.HealthIsFull();
    }
}
