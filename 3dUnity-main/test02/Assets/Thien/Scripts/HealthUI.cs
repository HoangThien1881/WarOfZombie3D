using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;   // Tham chi?u ??n Slider
    public Health playerHealth;  // Tham chi?u ??n script Health c?a nh�n v?t

    void Start()
    {
        // ??t gi� tr? ban ??u
        healthSlider.maxValue = playerHealth.maxHP;
        healthSlider.value = playerHealth.currentHP;

        // C?p nh?t m�u s?c ban ??u
        UpdateHealthColor();
    }

    void Update()
    {
        // C?p nh?t gi� tr? Slider d?a v�o m�u c?a nh�n v?t
        healthSlider.value = playerHealth.currentHP;

        // C?p nh?t m�u s?c c?a thanh m�u
        UpdateHealthColor();
    }

    void UpdateHealthColor()
    {
        // T�nh ph?n tr?m m�u c�n l?i
        float healthPercentage = playerHealth.currentHP / playerHealth.maxHP;

        // N?u m�u d??i 50%, ??i m�u thanh m�u th�nh v�ng
        if (healthPercentage < 0.5f)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.yellow; // M�u v�ng
        }
        else
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red; // M�u xanh
        }
    }
}
