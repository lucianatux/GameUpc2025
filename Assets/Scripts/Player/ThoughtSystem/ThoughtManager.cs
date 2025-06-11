using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtManager : MonoBehaviour
{
    public static ThoughtManager Instance;

    public GameObject thoughtBubbleUI;
    public TextMeshProUGUI thoughtText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        thoughtBubbleUI.SetActive(false);
    }

    public void ShowThought(ThoughtSO thought)
    {
        Debug.Log("🧠 Mostrando pensamiento: " + thought.text);
        StopAllCoroutines(); // Por si ya se estaba mostrando uno
        StartCoroutine(DisplayThought(thought));
    }

    private IEnumerator DisplayThought(ThoughtSO thought)
    {
        thoughtText.text = thought.text;
        thoughtBubbleUI.SetActive(true);
        Debug.Log("🗯️ Texto en pantalla: " + thoughtText.text);


        yield return new WaitForSeconds(thought.duration);

        thoughtBubbleUI.SetActive(false);
        Debug.Log("💨 Pensamiento ocultado");
    }
}
