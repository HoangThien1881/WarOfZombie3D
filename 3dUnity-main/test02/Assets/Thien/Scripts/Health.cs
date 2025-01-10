using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    public AudioSource audioSource; // AudioSource để phát âm thanh
    public AudioClip damageSound;   // Âm thanh khi zombie bị tấn công
    public AudioClip deathSound;    // Âm thanh khi zombie chết

    // Hàm nhận sát thương
    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        Debug.Log("Zombie bị bắn! HP còn lại: " + currentHP);

        // Phát âm thanh khi bị tấn công
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound); // Phát âm thanh khi bị tấn công
        }

        // Nếu máu về 0, zombie chết
        if (currentHP <= 0)
        {
            Debug.Log("Zombie đã chết!");

            // Phát âm thanh khi zombie chết
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound); // Phát âm thanh khi chết
            }

            // Bạn có thể thêm logic để thực hiện việc zombie chết (ví dụ: thay đổi trạng thái, tiêu diệt đối tượng, v.v.)
        }
    }

    private void Start()
    {
        currentHP = maxHP;

        // Kiểm tra nếu chưa có AudioSource thì tự tạo mới
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
}
