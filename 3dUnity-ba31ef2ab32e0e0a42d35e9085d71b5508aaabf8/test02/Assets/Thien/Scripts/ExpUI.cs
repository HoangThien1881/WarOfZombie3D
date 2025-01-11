using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpUI : MonoBehaviour
{
    public Slider expSlider; // Thanh slider hi?n th? EXP
    public PlayerExp playerExp; // Tham chi?u t?i script PlayerExp
    public TextMeshProUGUI levelText; // Tham chi?u ??n TextMeshPro ?? hi?n th? level

    private void Start()
    {
        expSlider.maxValue = playerExp.maxExp; // ??t giá tr? t?i ?a c?a thanh EXP
        expSlider.value = playerExp.currentExp; // ??t giá tr? ban ??u c?a thanh EXP
        UpdateLevelText(playerExp.level); // C?p nh?t hi?n th? level ban ??u

        // ??ng ký s? ki?n OnLevelUp
        playerExp.OnLevelUp += UpdateLevelText;
    }

    private void Update()
    {
        expSlider.value = playerExp.currentExp; // C?p nh?t giá tr? thanh EXP m?i frame
    }

    private void UpdateLevelText(int newLevel)
    {
        levelText.text = "Level: " + newLevel; // C?p nh?t text level
    }

    private void OnDestroy()
    {
        // H?y ??ng ký s? ki?n khi ??i t??ng b? h?y
        playerExp.OnLevelUp -= UpdateLevelText;
    }
}
