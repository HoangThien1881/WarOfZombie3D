using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public float currentExp = 0f; // EXP hiện tại của người chơi
    public float maxExp = 100f; // Mức EXP cần để tăng level
    public int level = 1; // Mức level của người chơi
    public float moveSpeed = 5f; // Tốc độ di chuyển của người chơi

    // Hàm cộng EXP
    public void AddExp(float expAmount)
    {
        currentExp += expAmount;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    // Hàm tăng level và tăng tốc độ di chuyển
    private void LevelUp()
    {
        level++;
        currentExp = 0f; // Reset EXP
        maxExp = maxExp * 1.2f; // Tăng mức EXP cần thiết cho level tiếp theo
        moveSpeed += 0.1f; // Tăng tốc độ di chuyển khi lên level
        Debug.Log("Level Up! Current Level: " + level + ", Speed: " + moveSpeed);
    }
}
