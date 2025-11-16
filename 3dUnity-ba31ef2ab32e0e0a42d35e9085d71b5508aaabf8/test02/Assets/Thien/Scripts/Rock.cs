using UnityEngine;

public class Rock : MonoBehaviour
{
    public float damage = 10f; // Sát th??ng c?a c?c ?á

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ki?m tra xem va ch?m v?i ng??i ch?i không
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>(); // L?y component Health t? player
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // G?i ph??ng th?c TakeDamage ?? gi?m máu ng??i ch?i
            }
            Destroy(gameObject); // Xóa c?c ?á sau khi va ch?m
        }
     
    }
}
