using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI healthSpeedText; // Text ?? hi?n th? máu và t?c ??
    public PlayerExp playerExp; // Tham chi?u ??n PlayerExp
    public Health playerHealth; // Tham chi?u ??n Health

    private void Update()
    {
        if (playerHealth != null && playerExp != null)
        {
            // C?p nh?t Text hi?n th? máu và t?c ??
            healthSpeedText.text = $"Health: {playerHealth.currentHP}/{playerHealth.maxHP}\nSpeed: {playerExp.moveSpeed:F1}";
        }
    }
}
