using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;   // Tham chi?u ??n Slider
    public Health playerHealth;  // Tham chi?u ??n script Health c?a nhân v?t

    void Start()
    {
        // ??t giá tr? ban ??u
        healthSlider.maxValue = playerHealth.maxHP;
        healthSlider.value = playerHealth.currentHP;

        // C?p nh?t màu s?c ban ??u
        UpdateHealthColor();
    }

    void Update()
    {
        // C?p nh?t giá tr? Slider d?a vào máu c?a nhân v?t
        healthSlider.value = playerHealth.currentHP;

        // C?p nh?t màu s?c c?a thanh máu
        UpdateHealthColor();
    }

    void UpdateHealthColor()
    {
        // Tính ph?n tr?m máu còn l?i
        float healthPercentage = playerHealth.currentHP / playerHealth.maxHP;

        // N?u máu d??i 50%, ??i màu thanh máu thành vàng
        if (healthPercentage < 0.5f)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.yellow; // Màu vàng
        }
        else
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red; // Màu xanh
        }
    }
}
