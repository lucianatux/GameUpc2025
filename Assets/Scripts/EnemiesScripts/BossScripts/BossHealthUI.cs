using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject bossBarObject; // El GameObject que contiene el slider (BossHealthBar)
    [SerializeField] private Slider bossSlider;

    private void Start()
    {
        bossBarObject.SetActive(false); // Se oculta al iniciar
    }

    public void Initialize(float maxHealth)
    {
        bossBarObject.SetActive(true);
        bossSlider.maxValue = maxHealth;
        bossSlider.value = maxHealth;
    }

    public void UpdateHealth(float currentHealth)
    {
        bossSlider.value = currentHealth;
    }

    public void Hide()
    {
        bossBarObject.SetActive(false);
    }
}