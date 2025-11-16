using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP; // Máu t?i ?a
    public float currentHP; // Máu hi?n t?i

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        Debug.Log("Nhân v?t b? t?n công! HP còn l?i: " + currentHP);

        // N?u máu v? 0, nhân v?t ch?t
        if (currentHP <= 0)
        {
            Debug.Log("Nhân v?t ?ã ch?t!");
        }
    }

    private void Start()
    {
        currentHP = maxHP; // ??t máu ??y khi b?t ??u
    }

    // Hàm t?ng máu t?i ?a và h?i ??y máu
    public void IncreaseMaxHP(float amount)
    {
        maxHP += amount; // T?ng máu t?i ?a
        currentHP = maxHP; // H?i ??y máu
        Debug.Log("T?ng máu t?i ?a! HP hi?n t?i: " + currentHP);
    }
}
